using OfficeOpenXml;
using System.Linq;

namespace SurveyScorer.Application.Helpers
{
    static class ExcelHelper
    {
        public static string GetCellValue(this ExcelWorksheet worksheet, int row, int column)
        {
            if (worksheet == null)
                return null;
            var cells = worksheet.Cells[row, column];
            return cells.Any() ? (cells.First().Value ?? string.Empty).ToString() : null;
        }
    }
}
