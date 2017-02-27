using SurveyScorer.Entities.Response;
using SurveyScorer.Entities.Survey;
using System.Linq;
using SurveyScorer.Entities.Enums;

namespace SurveyScorer.Application
{
    class ScoreCalculator
    {
        internal static UnitResult GetScore(Question question, string response)
        {
            var result = new UnitResult();

            if (question.SingleAnswerOnly)
            {
                var selectedOption = question.Options
                    .FirstOrDefault(o => o.Value.Trim().ToLower() == response.Trim().ToLower());
                if (selectedOption != null)
                    result.Score = selectedOption.Score;
            }
            else
            {
                var selectedOptions = question.Options
                    .Where(o => response.ToLower().Contains(o.Value.Trim().ToLower()));
                if (selectedOptions.Any())
                    result.Score = selectedOptions.Sum(op => op.Score);
            }

            if (question.ScoringRule.GreenScore.HasValue && result.Score >= question.ScoringRule.GreenScore)
                result.StateColor = ResultColor.Green;
            else if (question.ScoringRule.RedScore.HasValue && result.Score < question.ScoringRule.RedScore)
                result.StateColor = ResultColor.Red;
            else
                result.StateColor = ResultColor.Yellow;

            return result;
        }
    }
}
