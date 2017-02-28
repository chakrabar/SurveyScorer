using SurveyScorer.Application.Helpers;
using SurveyScorer.Entities.Response;
using System;
using System.Collections.Generic;
using System.IO;

namespace SurveyScorer.Application
{
    public class SurveyReportGenerator
    {
        //TODO: this method should move out of this class
        public static void Process(IEnumerable<ScoreCard> scoreCards, string templatePath, string outputDirectory)
        {
            var reportCount = 0;
            foreach (var card in scoreCards)
            {
                var reportString = Create(card, templatePath);
                var fileNamePrefix = $"{AppSettings.ReportPrefix}_{(++reportCount).ToString("000")}";
                FileHelper.CreateFileWithTimestampAndWrite(reportString, outputDirectory, fileNamePrefix, "html");
            }
        }

        public static string Create(ScoreCard scoreCard, string templatePath)
        {
            var templateContent = File.ReadAllText(templatePath);

            templateContent = PopulateMetadata(scoreCard.Metadata, templateContent);
            templateContent = PopulateScoreItems(scoreCard.ScoreItems, templateContent);
            templateContent = PopulateTopLevelDetails(scoreCard, templateContent);

            return templateContent;
        }

        //TODO: need to generalize more
        private static string PopulateTopLevelDetails(ScoreCard scoreCard, string templateContent)
        {
            templateContent = templateContent.Replace("@@year", DateTime.Now.Year.ToString());
            templateContent = templateContent.Replace("@@score-class", scoreCard.ResultColor.ToString().ToLower());
            templateContent = templateContent.Replace("@@score", scoreCard.Aggregate.ToString());
            return templateContent;
        }

        private static string PopulateMetadata(IEnumerable<MetaResponse> metadata, string templateContent)
        {
            foreach (var meta in metadata)
            {
                templateContent = templateContent.Replace($"@@{meta.ReportTag}", meta.Response);
            }
            return templateContent;
        }

        private static string PopulateScoreItems(IEnumerable<CategoryScore> scoreItems, string templateContent)
        {
            foreach (var scoreItem in scoreItems)
            {
                templateContent = templateContent.Replace($"@@{scoreItem.ReportTag}-class", scoreItem.StateColor.ToString().ToLower());
                templateContent = templateContent.Replace($"@@{scoreItem.ReportTag}", scoreItem.Response);                
            }

            return templateContent;
        }
    }
}
