using SurveyScorer.Entities.Response;
using System.Collections.Generic;
using System.IO;

namespace SurveyScorer.Application
{
    public class SurveyReportGenerator
    {
        public string Create(ScoreCard scoreCard, string templatePath)
        {
            var templateContent = File.ReadAllText(templatePath);

            //TODO: populate metadata
            templateContent = PopulateScoreItems(scoreCard.ScoreItems, templateContent);

            return templateContent;
        }

        private static string PopulateScoreItems(IEnumerable<CategoryScore> scoreItems, string templateContent)
        {
            foreach (var scoreItem in scoreItems)
            {
                templateContent = templateContent.Replace(scoreItem.ReportTag, scoreItem.Response);
                templateContent = templateContent.Replace($"{scoreItem.ReportTag}-class", scoreItem.StateColor.ToString().ToLower());
            }

            return templateContent;
        }
    }
}
