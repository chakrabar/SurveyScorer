using OfficeOpenXml;
using SurveyScorer.Entities.Response;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SurveyScorer.Application
{
    public class SurveyResultGenerator
    {
        public static void CreateExcel(IEnumerable<ScoreCard> scoreCards)
        {
            
        }

        Color _fadedGray = System.Drawing.ColorTranslator.FromHtml("#F7FAFA");
        Color _darkGray = System.Drawing.ColorTranslator.FromHtml("#848686");
        //public byte[] Download(string jobId, string version)
        //{
        //    using (ExcelPackage xlPackage = new ExcelPackage())
        //    {
        //        var ws = xlPackage.Workbook.Worksheets.Add("SurveyResults");
        //        SetExcelHeader(ws);
        //        var productLinkingJob = ProductLinkingDataService.GetProductLinkingJobDetails(jobId);
        //        var packagedeviceDetails = ProductLinkingDataService.GetPackageAndLinkedDeviceDetails(
        //            productLinkingJob.SourceProducts.Select(x => x.ProductCode).ToList(), productLinkingJob.DestinationProducts.Select(d => d.ProductCode).ToList());
        //        var inflightPackageDeviceDetails = ProductLinkingDataService.GetInFlightPackageAndLinkedDeviceDetails(productLinkingJob);
        //        var rows = SetExcelDataAndGetNumberOfRows(ws, packagedeviceDetails.Item1, packagedeviceDetails.Item2, inflightPackageDeviceDetails);
        //        FormatExcel(ws, rows, 14);
        //        ProtectExcelSheet(ws);
        //        ProtectExcelWorkBook(xlPackage);
        //        SetEditableCells(ws, rows);
        //        return xlPackage.GetAsByteArray();
        //    }
        //}

        //private void SetExcelHeader(ExcelWorksheet ws)
        //{

        //    ws.Cells[1, 1].Value = Constants.ExcelHeader.MANUFACTURER;
        //    ws.Cells[1, 1].Value = Constants.ExcelHeader.MANUFACTURER;
        //    ws.Cells[1, 2].Value = Constants.ExcelHeader.MODEL;
        //    ws.Cells[1, 3].Value = Constants.ExcelHeader.COLOUR;
        //    ws.Cells[1, 4].Value = Constants.ExcelHeader.MEMORY;
        //    ws.Cells[1, 5].Value = Constants.ExcelHeader.PRODUCT_NAME;
        //    ws.Cells[1, 6].Value = Constants.ExcelHeader.PRODUCT_CODE;
        //    ws.Cells[1, 7].Value = Constants.ExcelHeader.PACKAGE;
        //    ws.Cells[1, 8].Value = Constants.ExcelHeader.PACKAGE_PRODUCT_CODE;
        //    ws.Cells[1, 9].Value = Constants.ExcelHeader.TIER;
        //    ws.Cells[1, 10].Value = Constants.ExcelHeader.PACKAGE_RENTAL;
        //    ws.Cells[1, 11].Value = Constants.ExcelHeader.TERM;
        //    ws.Cells[1, 12].Value = Constants.ExcelHeader.LINKED;
        //    ws.Cells[1, 13].Value = Constants.ExcelHeader.PRICE;
        //    ws.Cells[1, 14].Value = Constants.ExcelHeader.REVENUE_CODE;
        //}
        //private void FormatExcel(ExcelWorksheet ws, int rowCount, int columnCount)
        //{
        //    if (rowCount == 0)
        //        return;

        //    var allCells = ws.Cells[1, 1, rowCount + 1, columnCount]; //with header

        //    var border = allCells.Style.Border.Top.Style
        //                = allCells.Style.Border.Left.Style
        //                = allCells.Style.Border.Right.Style
        //                = allCells.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

        //    allCells.AutoFilter = true; //auto enable filters in all columns

        //    allCells.Style.Fill.PatternType = ExcelFillStyle.Solid;

        //    allCells.Style.Fill.BackgroundColor.SetColor(_fadedGray);

        //    for (int i = 1; i <= columnCount; i++)
        //        ws.Column(i).AutoFit(); //autofit all columns

        //    ws.Column(12).Width = 13.71;
        //    ws.Column(14).Width = 16;//Set the Linked column width manually

        //    ws.Cells[1, 1, 1, 14].Style.Fill.PatternType = ExcelFillStyle.Solid; //header
        //    ws.Cells[1, 7, 1, 11].Style.Fill.BackgroundColor.SetColor(Color.LightSeaGreen);

        //    ws.Cells[1, 12, 1, 14].Style.Fill.BackgroundColor.SetColor(Color.Bisque); //Header editable background  
        //}
    }
}
