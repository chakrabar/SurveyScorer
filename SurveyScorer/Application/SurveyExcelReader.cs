using SurveyScorer.Application.Helpers;
using SurveyScorer.Entities.Response;
using SurveyScorer.Entities.Survey;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SurveyScorer.Application
{
    public class SurveyExcelReader
    {
        public List<ScoreCard> ReadFromExcelFile(string excelFilePath, ScoringConfig scoringConfig)
        {
            CheckFileExists(excelFilePath);

            var allScores = new List<ScoreCard>();

            var excel = new FileInfo(excelFilePath);
            using (ExcelPackage xlPackage = new ExcelPackage(excel))
            {
                CheckExcelValid(xlPackage);

                var worksheet = xlPackage.Workbook.Worksheets.First();
                var totalRows = worksheet.Dimension.End.Row;
                var totalColumns = worksheet.Dimension.End.Column;

                PopulateColumnNumbers(scoringConfig, worksheet, totalColumns);

                for (int rowNum = 2; rowNum <= totalRows; rowNum++)
                {
                    var scorecard = GetScoreMetadata(worksheet, rowNum);
                    foreach (var rule in scoringConfig.Questions)
                    {
                        var response = worksheet.GetCellValue(rowNum, rule.ColumnNumber.Value);
                        var result = ScoreCalculator.GetScore(rule, response);
                        scorecard.ScoreItems.Add(new CategoryScore
                        {
                            Query = rule.Query,
                            ReportTag = rule.ReportTag,
                            Response = response,
                            Score = result.Score,
                            StateColor = result.StateColor
                        });
                    }
                    allScores.Add(scorecard);
                }
            }
            return allScores;
        }

        private static ScoreCard GetScoreMetadata(ExcelWorksheet worksheet, int rowNum)
        {
            return new ScoreCard()
            {
                Project = worksheet.GetCellValue(rowNum, 3),
                BU = worksheet.GetCellValue(rowNum, 4),
                Client = worksheet.GetCellValue(rowNum, 5),
                TechLead = worksheet.GetCellValue(rowNum, 6),
                ScoreItems = new List<CategoryScore>()
            };
        }

        private static void PopulateColumnNumbers(ScoringConfig ruleConfig, ExcelWorksheet worksheet, int totalColumns)
        {
            for (int col = 1; col <= totalColumns; col++)
            {
                var colHeader = worksheet.GetCellValue(1, col);
                var ruleToUpdate = ruleConfig.Questions
                    .FirstOrDefault(q => q.Query.Trim().ToLower() == colHeader.Trim().ToLower());

                if (ruleToUpdate != null)
                    ruleToUpdate.ColumnNumber = col;
            }
        }

        private static void CheckFileExists(string excelFilePath)
        {
            if (!File.Exists(excelFilePath))
                throw new FileNotFoundException("File invalid " + excelFilePath);
        }

        private static void CheckExcelValid(ExcelPackage xlPackage)
        {
            if (xlPackage.Workbook == null)
                throw new InvalidDataException("Excel is corrupt! Check the file and upload again."); //+ excelFilePath
        }
    }
}
