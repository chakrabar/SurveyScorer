using OfficeOpenXml;
using SurveyScorer.Application.Helpers;
using SurveyScorer.Entities.Response;
using SurveyScorer.Entities.Survey;
using System;
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

                PopulateMetaColumnNumbers(scoringConfig, worksheet, totalColumns);
                PopulateColumnNumbers(scoringConfig, worksheet, totalColumns);

                for (int rowNum = 2; rowNum <= totalRows; rowNum++)
                {
                    var scorecard = GetScorecardWithMetadata(worksheet, scoringConfig, rowNum);
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
                    scorecard.Aggregate = (int)Math.Round(scorecard.ScoreItems.Sum(si => si.Score), 2);
                    scorecard.ResultColor = scorecard.Aggregate < 70 ? 
                                                Entities.Enums.ResultColor.Red : 
                                                (scorecard.Aggregate >= 90 ? Entities.Enums.ResultColor.Green : Entities.Enums.ResultColor.Yellow);
                    allScores.Add(scorecard);
                }
            }
            return allScores;
        }

        private static ScoreCard GetScorecardWithMetadata(ExcelWorksheet worksheet, ScoringConfig scoringConfig, int rowNum)
        {
            var scorecard = new ScoreCard()
            {
                ScoreItems = new List<CategoryScore>(),
                Metadata = new List<MetaResponse>()
            };
            foreach (var rule in scoringConfig.Metadata)
            {
                scorecard.Metadata.Add(new MetaResponse
                {
                    Query = rule.Query,
                    ReportTag = rule.ReportTag,
                    Response = worksheet.GetCellValue(rowNum, rule.ColumnNumber.Value)
                });
            }
            return scorecard;
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

        private static void PopulateMetaColumnNumbers(ScoringConfig ruleConfig, ExcelWorksheet worksheet, int totalColumns)
        {
            for (int col = 1; col <= totalColumns; col++)
            {
                var colHeader = worksheet.GetCellValue(1, col);
                var metaToUpdate = ruleConfig.Metadata
                    .FirstOrDefault(m => m.Query.Trim().ToLower() == colHeader.Trim().ToLower());

                if (metaToUpdate != null)
                    metaToUpdate.ColumnNumber = col;
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
