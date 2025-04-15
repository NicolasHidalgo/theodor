using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapp.Util
{
    public static class Formula
    {
        public static double[] RegresionLogaritmica(double[] xData, double[] yData)
        {
            // Ajustar parámetros
            FitExtendedNelsonSiegelParameters(xData, yData,
                out double beta0, out double beta1, out double beta2,
                out double beta3, out double lambda1, out double lambda2);

            // Retornar los parámetros como arreglo
            return new double[] { beta0, beta1, beta2, beta3, lambda1, lambda2 };
        }

        static void FitExtendedNelsonSiegelParameters(double[] xData, double[] yData, out double beta0, out double beta1, out double beta2, out double beta3, out double lambda1, out double lambda2)
        {
            // Valores iniciales para los parámetros
            double[] initialParams = { 0.1, 0.1, 0.1, 0.1, 1, 1 }; // beta0, beta1, beta2, beta3, lambda1, lambda2

            // Configuración del descenso de gradiente
            double learningRate = 0.000005; // Tasa de aprendizaje reducida para mayor precisión
            int maxIterations = 50000; // Aumentar el número máximo de iteraciones
            double tolerance = 1e-15; // Tolerancia más baja para mayor precisión


            // Función del modelo de Nelson-Siegel extendido
            Func<double[], double[], double[]> modelFunction = (paramsVector, x) =>
            {
                double paramBeta0 = paramsVector[0];
                double paramBeta1 = paramsVector[1];
                double paramBeta2 = paramsVector[2];
                double paramBeta3 = paramsVector[3];
                double paramLambda1 = paramsVector[4];
                double paramLambda2 = paramsVector[5];

                var yPred = new double[x.Length];
                for (int i = 0; i < x.Length; i++)
                {
                    double t = x[i];
                    double term1 = (1 - Math.Exp(-t / paramLambda1)) / (t / paramLambda1);
                    double term2 = term1 - Math.Exp(-t / paramLambda1);
                    double term3 = (1 - Math.Exp(-t / paramLambda2)) / (t / paramLambda2) - Math.Exp(-t / paramLambda2);

                    yPred[i] = paramBeta0 + paramBeta1 * term1 + paramBeta2 * term2 + paramBeta3 * term3;
                }

                return yPred;
            };

            // Función de error
            Func<double[], double> objectiveFunction = paramsVector =>
            {
                var yPred = modelFunction(paramsVector, xData);
                double error = 0;
                for (int i = 0; i < yData.Length; i++)
                {
                    error += Math.Pow(yPred[i] - yData[i], 2);
                }
                return Math.Sqrt(error);
            };

            // Función para calcular el gradiente numéricamente
            Func<double[], double[]> gradientFunction = paramsVector =>
            {
                var epsilon = 1e-8;
                var gradient = new double[paramsVector.Length];
                for (int i = 0; i < paramsVector.Length; i++)
                {
                    var perturbedParams = (double[])paramsVector.Clone();
                    perturbedParams[i] += epsilon;
                    var objectiveValuePlus = objectiveFunction(perturbedParams);
                    perturbedParams[i] -= 2 * epsilon;
                    var objectiveValueMinus = objectiveFunction(perturbedParams);
                    gradient[i] = (objectiveValuePlus - objectiveValueMinus) / (2 * epsilon);
                }
                return gradient;
            };

            // Descenso de gradiente
            for (int iteration = 0; iteration < maxIterations; iteration++)
            {
                double[] gradient = gradientFunction(initialParams);

                // Actualizar parámetros
                for (int i = 0; i < initialParams.Length; i++)
                {
                    initialParams[i] -= learningRate * gradient[i];
                }

                // Verificar convergencia
                double error = objectiveFunction(initialParams);
                if (error < tolerance)
                {
                    break;
                }

                // Opción para imprimir el progreso
                if (iteration % 100 == 0)
                {
                    Console.WriteLine($"Iteración {iteration}: Error = {error}");
                }
            }

            // Asignar los valores ajustados a los parámetros de salida
            beta0 = initialParams[0];
            beta1 = initialParams[1];
            beta2 = initialParams[2];
            beta3 = initialParams[3];
            lambda1 = initialParams[4];
            lambda2 = initialParams[5];
        }


    }
}