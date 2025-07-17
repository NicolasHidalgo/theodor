using System;
using MathNet.Numerics;
using MathNet.Numerics.Optimization;
using MathNet.Numerics.LinearAlgebra;

namespace webapp.Util
{
    public static class Formula
    {
        public static double[] RegresionLogaritmica(double[] xData, double[] yData)
        {
            // Parámetros iniciales razonables
            var initialParams = Vector<double>.Build.DenseOfArray(new double[]
            {
                0.01,   // beta0
                0.01,   // beta1
                0.01,   // beta2
                0.01,   // beta3
                1.0,    // lambda1
                1.0     // lambda2
            });

            // Convertir tus datos a Vector<double>
            // Convertir datos a Vector
            var xVector = Vector<double>.Build.DenseOfArray(xData);
            var yVector = Vector<double>.Build.DenseOfArray(yData);

            // Función de modelo
            Func<Vector<double>, Vector<double>, Vector<double>> modelFunction = (parameters, maturities) =>
            {
                double beta0 = parameters[0];
                double beta1 = parameters[1];
                double beta2 = parameters[2];
                double beta3 = parameters[3];
                double lambda1 = Math.Max(parameters[4], 0.001);
                double lambda2 = Math.Max(parameters[5], 0.001);

                var yPred = Vector<double>.Build.Dense(maturities.Count);

                for (int i = 0; i < maturities.Count; i++)
                {
                    double t = maturities[i];

                    double term1 = (1 - Math.Exp(-t / lambda1)) / (t / lambda1);
                    double term2 = term1 - Math.Exp(-t / lambda1);
                    double term3 = (1 - Math.Exp(-t / lambda2)) / (t / lambda2) - Math.Exp(-t / lambda2);

                    yPred[i] = beta0 + beta1 * term1 + beta2 * term2 + beta3 * term3;
                }

                return yPred;
            };

            // ✅ El orden correcto de argumentos
            var objective = ObjectiveFunction.NonlinearModel(
                modelFunction,
                xVector,
                yVector
            );

            // Crear el optimizador
            var minimizer = new LevenbergMarquardtMinimizer();

            // Ejecutar ajuste
            var result = minimizer.FindMinimum(objective, initialParams);

            // Mostrar resumen opcional
            //Console.WriteLine("Error residual total: " + result.FunctionInfo.Value);

            // Retornar los parámetros como array
            return result.MinimizingPoint.ToArray();
        }
    }
}

