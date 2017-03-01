using OfficeOpenXml;
using OfficeOpenXml.Style;
using SurveyScorer.Entities.Enums;
using SurveyScorer.Entities.Response;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SurveyScorer.Application
{
    public class SurveySummaryGenerator
    {
        static Color _fadedGray = ColorTranslator.FromHtml("#F7FAFA");
        static Color _darkGray = ColorTranslator.FromHtml("#848686");
        static Color _red = ColorTranslator.FromHtml("#E3A098");
        static Color _yellow = ColorTranslator.FromHtml("#F2E963");
        static Color _green = ColorTranslator.FromHtml("#94d98c");

        public static byte[] CreateExcel(IEnumerable<ScoreCard> scoreCards)
        {
            using (ExcelPackage xlPackage = new ExcelPackage())
            {
                var ws = xlPackage.Workbook.Worksheets.Add("SurveyResults");

                var totalColumns = SetExcelHeaders(scoreCards, ws); //aggregate score 1 column
                var totalRows = scoreCards.Count() + 1;
                FormatExcel(ws, totalRows, totalColumns);

                PopulateResultDetails(scoreCards, ws);

                return xlPackage.GetAsByteArray();
            }
        }

        private static void PopulateResultDetails(IEnumerable<ScoreCard> scoreCards, ExcelWorksheet ws)
        {
            var row = 2;
            foreach (var scorecard in scoreCards)
            {
                var column = 1;
                foreach (var meta in scorecard.Metadata)
                {
                    ws.Cells[row, column].Value = meta.Response;
                    column++;
                }
                ws.Cells[row, column].Value = scorecard.Aggregate;
                ws.Cells[row, column].Style.Fill.BackgroundColor.SetColor(GetCellColor(scorecard.ResultColor));
                ws.Cells[row, column].Style.Font.Bold = true;
                column++;
                foreach (var scoreItem in scorecard.ScoreItems)
                {
                    ws.Cells[row, column].Value = $"[{scoreItem.Score}] {scoreItem.Response}";
                    ws.Cells[row, column].Style.Fill.BackgroundColor.SetColor(GetCellColor(scoreItem.StateColor));
                    column++;
                }                
                row++;
            }
        }

        private static Color GetCellColor(ResultColor stateColor)
        {
            switch (stateColor)
            {
                case ResultColor.Red:
                    return _red;
                case ResultColor.Yellow:
                    return _yellow;
                case ResultColor.Green:
                    return _green;
                default:
                    break;
            }
            return _fadedGray;
        }

        //Assumption 01 - all scorecards have equal number of meta & questions
        //Assumption 02 - all meta & questions are order in same way
        private static int SetExcelHeaders(IEnumerable<ScoreCard> scoreCards, ExcelWorksheet ws)
        {
            int column = 1;
            foreach (var meta in scoreCards.First().Metadata)
            {
                ws.Cells[1, column].Value = meta.Query;
                column++;
            }
            ws.Cells[1, column++].Value = "Total";
            foreach (var scoreItem in scoreCards.First().ScoreItems)
            {
                ws.Cells[1, column].Value = scoreItem.Query;
                column++;
            }            

            return --column;
        }

        static void FormatExcel(ExcelWorksheet ws, int totalRows, int totalColumns)
        {
            if (totalRows == 0)
                return;

            var allCells = ws.Cells[1, 1, totalRows, totalColumns]; //with header

            var border = allCells.Style.Border.Top.Style
                        = allCells.Style.Border.Left.Style
                        = allCells.Style.Border.Right.Style
                        = allCells.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            allCells.AutoFilter = true; //auto enable filters in all columns

            allCells.Style.Fill.PatternType = ExcelFillStyle.Solid;

            allCells.Style.Fill.BackgroundColor.SetColor(_fadedGray);

            for (int i = 1; i <= totalColumns; i++)
                ws.Column(i).AutoFit(); //autofit all columns

            ws.Cells[1, 1, 1, totalColumns].Style.Fill.BackgroundColor.SetColor(Color.LightSeaGreen);
        }
    }
}
