using BEANS;
using BL;
using ClosedXML.Excel;
using log4net;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using webapp.Util;
using webapp.ViewModels;

namespace webapp.Controllers
{
    [Authorize]
    public class XLSController : BaseController
    {

        XLS_BL neg = new XLS_BL();
        //static string xlsPathDES = ConfigurationManager.AppSettings["xlsPathDES"];
        //static string xlsPathPRO = ConfigurationManager.AppSettings["xlsPathPRO"];
        static string xlsPath = ConfigurationManager.AppSettings["xlsPath"];

        //[DllImport("user32.dll")]
        //static public extern int GetWindowThreadProcessId(int hWnd, out int lpdwProcessId);
        //Process GetExcelProcess(Microsoft.Office.Interop.Excel.Application excelApp)
        //{
        //    int id;
        //    GetWindowThreadProcessId(excelApp.Hwnd, out id);
        //    return Process.GetProcessById(id);
        //}

        public ActionResult Permisos()
        {
            return View();
        }

        public ActionResult Dashboard(int CodReporte)
        {
            var parametro = "?CodReporte=" + CodReporte;
            var replymenu = neg.fn_xls_get_menu(parametro);
            var menu = ((List<SEG_OPCION_BE>)replymenu.DATA).FirstOrDefault();
            ViewBag.NomMenu = menu.NOM_MENU;

            var replydash = neg.fn_xls_get_dashboard(CodReporte);
            var dashboard = ((List<PBI_DASHBOARD_BE>)replydash.DATA).FirstOrDefault();
            ViewBag.UrlPrivado = dashboard.URL_PRIVADO;
            ViewBag.UrlPublico = dashboard.URL_PUBLICO;

            return View();
        }

        public ActionResult CargaExcel(int CodCarga)
        {
            var model = new GEN_REPLY_BE();
            model.ACCION = "GET_PROCESO";
            var Proceso = new XLS_PROCESO_BE();
            Proceso.COD_CARGA = CodCarga;
            model.DATA = Proceso;
            var modProceso = neg.fn_xls_sel_proceso(model);
            Proceso = ((List<XLS_PROCESO_BE>)modProceso.DATA).FirstOrDefault();

            ViewBag.CodCarga = Proceso.COD_CARGA;
            ViewBag.NomCarga = Proceso.NOM_CARGA;
            ViewBag.NomHoja = Proceso.NOM_HOJA;

            return View("CargaExcel");
        }

        public JsonResult JSON_GetProceso(GEN_REPLY_BE model)
        {
            model = neg.fn_xls_sel_proceso(model);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult JSON_GetPermisos(GEN_REPLY_BE model)
        {
            var _obj = (string[])model.DATA;
            var _user = JsonConvert.DeserializeObject<XLS_USUARIO_PROCESO_BE>(_obj[0]);
            model.DATA = _user;
            model = neg.fn_xls_sel_permisos(model);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Permisos(GEN_REPLY_BE model)
        {
            if (ModelState.IsValid)
            {
                var _obj = (string[])model.DATA;
                var _aux = _obj[0];
                var _per = JsonConvert.DeserializeObject<XLS_USUARIO_PROCESO_BE>(_aux);
                model.DATA = _per;

                var reply = neg.fn_xls_upd_permisos(model);
                var res = new Response();
                res.Message = reply.MENSAJE;

                res.Status = HttpStatusCode.BadRequest;

                if (reply.MENSAJE.Contains("SUCCESS"))
                    res.Status = HttpStatusCode.OK;

                return Json(res);
            }

            return Json(
                     new Response
                     {
                         Status = HttpStatusCode.BadRequest,
                         Message = "No se puede continuar por errores en el modelo",
                         Errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)
                     });
        }

        public JsonResult JSON_GetEstructura(GEN_REPLY_BE model)
        {
            var _obj = (string[])model.DATA;
            var _mod = JsonConvert.DeserializeObject<XLS_PROCESO_ESTRUCTURA_BE>(_obj[0]);
            model.DATA = _mod;
            model = neg.fn_xls_sel_estructura(model);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult JSON_GetParametros(GEN_REPLY_BE model)
        {
            var _obj = (string[])model.DATA;
            var _mod = JsonConvert.DeserializeObject<XLS_PROCESO_PARAMETRO_BE>(_obj[0]);
            model.DATA = _mod;
            model = neg.fn_xls_sel_parametro(model);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JSON_GetEstructuraExcel(GEN_REPLY_BE model)
        {
            var _obj = (string[])model.DATA;
            var _mod = JsonConvert.DeserializeObject<XLS_PROCESO_ESTRUCTURA_BE>(_obj[0]);
            model.DATA = _mod;

            try
            {
                model = neg.fn_xls_sel_estructura(model);
                var lista = (List<XLS_PROCESO_ESTRUCTURA_BE>) model.DATA;
                

                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add(_mod.NOM_HOJA);
                    var currentRow = 0;

                    //worksheet.Cell(currentRow, 1).Value = "NUM_NIF_RECP";
                    //worksheet.Cell(currentRow, 2).Value = "NOM_RZN_SOC_RECP";
                    //worksheet.Cell(currentRow, 3).Value = "COD_TIP_CPE";

                    foreach (var item in lista)
                    {
                        currentRow++;
                        worksheet.Cell(1, currentRow).Value = item.NOM_CAMPO;
                    }

                    worksheet.Columns(1, 25).AdjustToContents();
                    //var range = worksheet.Range("A1:Y" + lista.Count + 1);
                    //var table = range.CreateTable();
                    //table.Column(1).Style.NumberFormat.Format = "#,##0.00";

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();

                        model.Base64 = Convert.ToBase64String(content);
                        //return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "data.xlsx");
                    }
                }

            }
            catch (Exception ex)
            {
                model.MENSAJE = "ERROR: " + ex.Message;
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JSON_GetVistaCargaExcel(GEN_REPLY_BE model)
        {
            var _obj = (string[])model.DATA;
            var _mod = JsonConvert.DeserializeObject<XLS_CARGA_BE>(_obj[0]);
            model.DATA = _mod;

            try
            {
                var datatable = neg.fn_xls_pro_vistaCarga(model);
                
                using (var workbook = new XLWorkbook())
                {

                    datatable.TableName = _mod.NOM_HOJA;
                    workbook.Worksheets.Add(datatable);
                    workbook.Worksheet(_mod.NOM_HOJA).Columns(1, 200).AdjustToContents();

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        model.Base64 = Convert.ToBase64String(content);
                    }
                }
                

            }
            catch (Exception ex)
            {
                model.MENSAJE = "ERROR: " + ex.Message;
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //epplus
        [HttpPost]
        public ActionResult CargaXls()
        {
            Response res = new Response();
            res.Status = HttpStatusCode.BadRequest;
            res.Message = string.Empty;

            try
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var model = new GEN_REPLY_BE(); //(GEN_REPLY_BE)Request["model"];
                    var _obj = (string[])model.DATA;
                    var _Accion = Request["Accion"];
                    var _CodUsuario = Request["CodUsuario"];
                    var _CodCarga = Request["CodCarga"];

                    var _Control01 = Request["CONTROL_01"];
                    var _Control02 = Request["CONTROL_02"];

                    model.ACCION = _Accion;
                    model.COD_USUARIO = _CodUsuario;
                    XLS_CARGA_BE Carga = new XLS_CARGA_BE();
                    Carga.COD_CARGA = int.Parse(_CodCarga);

                    model.ACCION = "GET_PROCESO";
                    var Proceso = new XLS_PROCESO_BE();
                    Proceso.COD_CARGA = Carga.COD_CARGA;
                    model.DATA = Proceso;
                    var modProceso = neg.fn_xls_sel_proceso(model);
                    Proceso = ((List<XLS_PROCESO_BE>)modProceso.DATA).FirstOrDefault();
                    var _NomHoja = Proceso.NOM_HOJA;

                    model.ACCION = "SELECT_ESTRUCTURA";
                    var Estructura = new XLS_PROCESO_ESTRUCTURA_BE();
                    Estructura.COD_CARGA = Carga.COD_CARGA;
                    model.DATA = Estructura;
                    var modEstructura = neg.fn_xls_sel_estructura(model);
                    var Estructuras = (List<XLS_PROCESO_ESTRUCTURA_BE>)modEstructura.DATA;

                    var fileContent = Request.Files[i];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        string fileCliente = Path.GetFileName(fileContent.FileName);
                        string fileExtension = Path.GetExtension(fileContent.FileName).ToLower();
                        string fileDestino = string.Empty;
                        string fileDestinoDBS = string.Empty;

                        if (fileExtension != ".xlsx" && fileExtension != ".xls" && fileExtension != ".xlsm")
                        {
                            res.Status = HttpStatusCode.BadRequest;
                            res.Message = "ERROR: El archivo debe ser tipo Excel (.xlsx,.xls)";
                            return Json(res);
                        }

                        //var stream = fileContent.InputStream;
                        var nomenclatura = Carga.COD_CARGA + "_" + DateTime.Now.ToString("yyMMddhhmmss") + ".xlsx";
                        if (HttpContext.IsDebuggingEnabled)
                        {
                            fileDestino = Path.Combine(Server.MapPath("~/XlsD"), nomenclatura);
                            fileDestinoDBS = Path.Combine(Server.MapPath("~/XlsD"), nomenclatura);
                        }
                        else
                        {
                            fileDestino = Path.Combine(Server.MapPath("~/XlsD"), nomenclatura);
                            fileDestinoDBS = Path.Combine(Server.MapPath("~/XlsD"), nomenclatura);
                            if (xlsPath != string.Empty)
                            {
                                fileDestinoDBS = fileDestino.Replace(Server.MapPath("~/XlsD"), xlsPath);
                            }
                        }

                        var mensajeError = string.Empty;
                        fileContent.SaveAs(fileDestinoDBS);
                        var HojaPrincipal = string.Empty;

                        try
                        {
                            Log.Debug("Inicializando ExcelPackage");

                            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                            FileInfo existingFile = new FileInfo(fileDestinoDBS);
                            using (ExcelPackage package = new ExcelPackage(existingFile))
                            {
                                //get the first worksheet in the workbook

                                _NomHoja = _NomHoja.Replace("%", "").Trim();
                                int pos = 0;
                                foreach (var sheet in package.Workbook.Worksheets)
                                {
                                    var SheetName = sheet.Name;
                                    if (SheetName.ToUpper().Contains(_NomHoja.ToUpper()) || _NomHoja.ToUpper().Contains(SheetName.ToUpper()))
                                    {
                                        HojaPrincipal = SheetName;
                                        break;
                                    }
                                    pos++;
                                }

                                if (HojaPrincipal == string.Empty)
                                {
                                    res.Status = HttpStatusCode.BadRequest;
                                    res.Message += "ERROR: No existe la Hoja '" + _NomHoja + "' en el excel.";
                                }
                                else
                                {
                                    ExcelWorksheet worksheet = package.Workbook.Worksheets[pos];
                                    Log.Debug("Hoja principal => " + HojaPrincipal);

                                    var FilaEncabezado = 0;

                                    var iFilaEncabezado = 0;
                                    var FilaEncabezadoLast = 0;
                                    var cRowLast = 0;

                                    for (int r = 1; r <= 12; r++)
                                    {
                                        var iRow = 0;
                                        var cRow1 = 0;
                                        var cRow2 = 0;

                                        var Ar1 = worksheet.Cells[r, 1].Text.ToString();
                                        var Br1 = worksheet.Cells[r, 2].Text.ToString();
                                        var Cr1 = worksheet.Cells[r, 3].Text.ToString();
                                        var Dr1 = worksheet.Cells[r, 4].Text.ToString();
                                        var Er1 = worksheet.Cells[r, 5].Text.ToString();

                                        var Ar2 = worksheet.Cells[r + 1, 1].Text.ToString();
                                        var Br2 = worksheet.Cells[r + 1, 2].Text.ToString();
                                        var Cr2 = worksheet.Cells[r + 1, 3].Text.ToString();
                                        var Dr2 = worksheet.Cells[r + 1, 4].Text.ToString();
                                        var Er2 = worksheet.Cells[r + 1, 5].Text.ToString();

                                        if (Ar1 != string.Empty)
                                            cRow1++;
                                        if (Br1 != string.Empty)
                                            cRow1++;
                                        if (Cr1 != string.Empty)
                                            cRow1++;
                                        if (Dr1 != string.Empty)
                                            cRow1++;
                                        if (Er1 != string.Empty)
                                            cRow1++;

                                        if (Ar2 != string.Empty)
                                            cRow2++;
                                        if (Br2 != string.Empty)
                                            cRow2++;
                                        if (Cr2 != string.Empty)
                                            cRow2++;
                                        if (Dr2 != string.Empty)
                                            cRow2++;
                                        if (Er2 != string.Empty)
                                            cRow2++;

                                        if (cRow1 > 0 && cRow2 == cRow1)
                                        {
                                            iRow = cRow1;
                                            iFilaEncabezado = r;
                                        }                                           
                                        if (cRow1 >= cRow2)
                                        {
                                            iRow = cRow1;
                                            iFilaEncabezado = r;
                                        }
                                        if (cRow2 > cRow1)
                                        {
                                            iRow = cRow2;
                                            iFilaEncabezado = r + 1;
                                        }

                                        if (FilaEncabezadoLast == 0)
                                        {
                                            FilaEncabezadoLast = iFilaEncabezado;
                                            cRowLast = iRow;
                                        }
                                        else
                                        {
                                            if (cRow1 > cRowLast || cRow2 > cRowLast)
                                            {
                                                FilaEncabezadoLast = iFilaEncabezado;
                                                cRowLast = iRow; 
                                            }
                                            
                                        }
                                        
                                    }

                                    FilaEncabezado = FilaEncabezadoLast;

                                    /* metodo antiguo
                                    var A1 = worksheet.Cells[1, 1].Text.ToString();
                                    var B1 = worksheet.Cells[1, 2].Text.ToString();
                                    var C1 = worksheet.Cells[1, 3].Text.ToString();
                                    var D1 = worksheet.Cells[1, 4].Text.ToString();
                                    var E1 = worksheet.Cells[1, 5].Text.ToString();

                                    var A2 = worksheet.Cells[2, 1].Text.ToString();
                                    var B2 = worksheet.Cells[2, 2].Text.ToString();
                                    var C2 = worksheet.Cells[2, 3].Text.ToString();
                                    var D2 = worksheet.Cells[2, 4].Text.ToString();
                                    var E2 = worksheet.Cells[2, 5].Text.ToString();

                                    var A3 = worksheet.Cells[3, 1].Text.ToString();
                                    var B3 = worksheet.Cells[3, 2].Text.ToString();
                                    var C3 = worksheet.Cells[3, 3].Text.ToString();
                                    var D3 = worksheet.Cells[3, 4].Text.ToString();
                                    var E3 = worksheet.Cells[3, 5].Text.ToString();

                                    var A4 = worksheet.Cells[4, 1].Text.ToString();
                                    var B4 = worksheet.Cells[4, 2].Text.ToString();
                                    var C4 = worksheet.Cells[4, 3].Text.ToString();
                                    var D4 = worksheet.Cells[4, 4].Text.ToString();
                                    var E4 = worksheet.Cells[4, 5].Text.ToString();

                                    var A5 = worksheet.Cells[5, 1].Text.ToString();
                                    var B5 = worksheet.Cells[5, 2].Text.ToString();
                                    var C5 = worksheet.Cells[5, 3].Text.ToString();
                                    var D5 = worksheet.Cells[5, 4].Text.ToString();
                                    var E5 = worksheet.Cells[5, 5].Text.ToString();

                                    var cRow1 = 0;
                                    var cRow2 = 0;
                                    var cRow3 = 0;
                                    var cRow4 = 0;
                                    var cRow5 = 0;

                                    if (A1 != string.Empty)
                                        cRow1++;
                                    if (B1 != string.Empty)
                                        cRow1++;
                                    if (C1 != string.Empty)
                                        cRow1++;
                                    if (D1 != string.Empty)
                                        cRow1++;
                                    if (E1 != string.Empty)
                                        cRow1++;

                                    if (A2 != string.Empty)
                                        cRow2++;
                                    if (B2 != string.Empty)
                                        cRow2++;
                                    if (C2 != string.Empty)
                                        cRow2++;
                                    if (D2 != string.Empty)
                                        cRow2++;
                                    if (E2 != string.Empty)
                                        cRow2++;

                                    if (A3 != string.Empty)
                                        cRow3++;
                                    if (B3 != string.Empty)
                                        cRow3++;
                                    if (C3 != string.Empty)
                                        cRow3++;
                                    if (D3 != string.Empty)
                                        cRow3++;
                                    if (E3 != string.Empty)
                                        cRow3++;

                                    if (A4 != string.Empty)
                                        cRow4++;
                                    if (B4 != string.Empty)
                                        cRow4++;
                                    if (C4 != string.Empty)
                                        cRow4++;
                                    if (D4 != string.Empty)
                                        cRow4++;
                                    if (E4 != string.Empty)
                                        cRow4++;

                                    if (A5 != string.Empty)
                                        cRow5++;
                                    if (B5 != string.Empty)
                                        cRow5++;
                                    if (C5 != string.Empty)
                                        cRow5++;
                                    if (D5 != string.Empty)
                                        cRow5++;
                                    if (E5 != string.Empty)
                                        cRow5++;

                                    FilaEncabezado = 5;
                                    if (cRow4 > 0 && cRow5 == cRow4)
                                        FilaEncabezado = 4;
                                    if (cRow3 > 0 && cRow4 == cRow3)
                                        FilaEncabezado = 3;
                                    if (cRow2 > 0 && cRow3 == cRow2)
                                        FilaEncabezado = 2;
                                    if (cRow1 > 0 && cRow2 == cRow1)
                                        FilaEncabezado = 1;

                                    if ((cRow1 >= cRow2) && (cRow1 >= cRow3) && (cRow1 >= cRow4) && (cRow1 >= cRow5))
                                        FilaEncabezado = 1;
                                    if ((cRow2 > cRow1) && (cRow2 >= cRow3) && (cRow2 >= cRow4) && (cRow2 >= cRow5))
                                        FilaEncabezado = 2;
                                    if ((cRow3 > cRow1) && (cRow3 > cRow2) && (cRow3 >= cRow4) && (cRow3 >= cRow5))
                                        FilaEncabezado = 3;
                                    if ((cRow4 > cRow1) && (cRow4 > cRow2) && (cRow4 > cRow3) && (cRow4 >= cRow5))
                                        FilaEncabezado = 4;
                                    if ((cRow5 > cRow1) && (cRow5 > cRow2) && (cRow5 > cRow3) && (cRow5 > cRow4))
                                        FilaEncabezado = 5;

                                    */

                                    Carga.FILA_ENCABEZADO = FilaEncabezado;

                                    var ColumnaEncabezado = 0;
                                    for (int j = 1; j <= 5; j++)
                                    {
                                        
                                        var valorColumna = worksheet.Cells[FilaEncabezado, j].Text.ToString();
                                        if (valorColumna != string.Empty)
                                        {
                                            ColumnaEncabezado = j;
                                            break;
                                        }
                                    }

                                    Carga.COLUMNA_ENCABEZADO = ColumnaEncabezado;

                                    // validacion estructura
                                    var c = 0;
                                    var isOK = true;
                                    foreach (var item in Estructuras.OrderBy(x => x.ORDEN))
                                    {
                                        var col = ColumnaEncabezado + c;

                                        var valor = worksheet.Cells[FilaEncabezado, col].Text.ToString();
                                        valor = valor.ToUpper();
                                        var campo = item.NOM_CAMPO.ToUpper();
                                        var equivalencia = item.EQUIVALENCIA;

                                        if (!campo.Equals(valor))
                                        {
                                            res.Status = HttpStatusCode.BadRequest;
                                            //res.Message += "ERROR: Error de estructura. Fila [" + FilaEncabezado + "] Columna [" + col + "] : Dice [" + valor + "] debe decir [" + campo + "]";
                                            var colName = GetExcelColumnName(col);
                                            res.Message += "ERROR: Error de estructura. La celda '"+ colName + FilaEncabezado.ToString() +  "' : Dice [" + valor + "] debe decir [" + campo + "]";
                                            isOK = false;
                                            break;
                                        }
                                        c++;
                                    }

                                    c = 0;
                                    if (isOK)
                                    {
                                        foreach (var item in Estructuras.OrderBy(x => x.ORDEN))
                                        {
                                            var col = ColumnaEncabezado + c;
                                            var row = FilaEncabezado + 1;

                                            //var valor = worksheet.Cells[row, col].Text.ToString();
                                            var campo = item.NOM_CAMPO;
                                            var tipo = item.TIPO.ToUpper();
                                            var _NumberFormat = string.Empty; //"@";

                                            if (tipo.Equals("FECHA"))
                                            {
                                                _NumberFormat = "yyyy-mm-dd hh:mm:ss";
                                            }
                                            //if (tipo.Equals("TEXTO"))
                                            //{
                                            //    _NumberFormat = "@";
                                            //}
                                            if (tipo.Equals("NUMERO"))
                                            {
                                                _NumberFormat = "0.0000000000000000";
                                            }

                                            if (_NumberFormat != string.Empty)
                                            {
                                                var colName = GetExcelColumnName(col);
                                                var rango = colName + row.ToString() + ":" + colName + "150000";
                                                worksheet.Cells[rango].Style.Numberformat.Format = _NumberFormat;
                                            }
                                            
                                            c++;
                                        }

                                    }

                                    package.Save();

                                    worksheet.Dispose();
                                    package.Dispose();

                                    GC.Collect();
                                }

                            }
                            GC.Collect();
                            var archivoDestino = fileDestinoDBS.Replace(@"\", "/");

                            Carga.ARCHIVO_FISICO = fileCliente;
                            Carga.ARCHIVO_CARGA = fileDestinoDBS;
                            Carga.NOM_HOJA = HojaPrincipal;
                            Carga.CONTROL_01 = _Control01;
                            Carga.CONTROL_02 = _Control02;
                            model.ACCION = "INSERT_CARGA";
                            model.DATA = Carga;

                            if (!res.Message.Contains("ERROR"))
                            {
                                var execute = neg.fn_xls_upd_carga(model, Proceso);
                                res.Message = execute.MENSAJE;
                                if (execute.MENSAJE.Contains("SUCCESS"))
                                    res.Status = HttpStatusCode.OK;
                            }

                        }
                        catch (Exception ex)
                        {
                            Log.Debug("Error, " + ex.Message);
                            System.Threading.Thread.Sleep(1000);  //Espera un segundo para intentarlo de nuevo
                            res.Message += " Librería Fail: " + ex.Message;
                        }

                    }

                }


            }
            catch (Exception ex)
            {
                Log.Debug("Error, " + ex.Message);
                res.Status = HttpStatusCode.BadRequest;
                res.Message += "ERROR: " + ex.Message;
            }

            return Json(res);
        }     
        private string GetExcelColumnName(int columnNumber)
        {
            string columnName = "";

            while (columnNumber > 0)
            {
                int modulo = (columnNumber - 1) % 26;
                columnName = Convert.ToChar('A' + modulo) + columnName;
                columnNumber = (columnNumber - modulo) / 26;
            }

            return columnName;
        }

    }
}