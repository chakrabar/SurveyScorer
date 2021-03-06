﻿using SurveyScorer.Entities.Enums;
using SurveyScorer.Entities.Response;
using SurveyScorer.Entities.Survey;
using System;
using System.Linq;

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
                {
                    result.Score = selectedOption.Score;
                }
                else //response didn't match any option
                {
                    //bespoke logic for "Other"
                    var otherOption = question.Options.FirstOrDefault(op => op.Value.Trim().ToLower() == "other");
                    if (otherOption != null)
                        result.Score = otherOption.Score;
                }                    
            }
            else
            {
                var selectedOptions = question.Options
                    .Where(o => response.ToLower().Contains(o.Value.Trim().ToLower()));
                if (selectedOptions.Any())
                    result.Score = selectedOptions.Sum(op => op.Score);
                //bespoke logic for "Other"                
                var otherOption = question.Options.FirstOrDefault(op => op.Value.Trim().ToLower() == "other");
                if (otherOption != null)
                {
                    var check = new string(response.ToArray()); //bwahahhaahaha 
                    foreach (var option in question.Options)
                    {
                        check = check.Replace(option.Value, "");
                    }
                    check = check.Replace(",", "").Trim(); //removing comma separators
                    if (!string.IsNullOrWhiteSpace(check))
                        result.Score += otherOption.Score;
                }
            }

            if (question.ScoringRule.GreenScore.HasValue && result.Score >= question.ScoringRule.GreenScore)
                result.StateColor = ResultColor.Green;
            else if (question.ScoringRule.RedScore.HasValue && result.Score < question.ScoringRule.RedScore)
                result.StateColor = ResultColor.Red;
            else
                result.StateColor = ResultColor.Yellow;

            return result;
        }

        //TODO: overall RED-GREEN rules are hard coded as 70% and 85% - should be configurable
        internal static (decimal RedScore, decimal GreenScore) GetAggregateRules(ScoringConfig scoringConfig)
        {
            var maxPossibleScore = scoringConfig.Questions
                .Aggregate(0.0m, (current, next) =>
                    current + (next.SingleAnswerOnly ?
                                next.Options.Max(op => op.Score) :
                                next.Options.Sum(op => op.Score)));
            var redScore = maxPossibleScore * (scoringConfig.AggregateYellowPercentage / 100m);
            var greenScore = maxPossibleScore * (scoringConfig.AggregateGreenPercentage / 100m);
            return (redScore, greenScore);
        }

        internal static (decimal Sum, ResultColor Color) GetAggregate(ScoreCard scoreCard, decimal redScore, decimal greenScore)
        {
            
            var aggregate = Math.Round(scoreCard.ScoreItems.Sum(si => si.Score), 2);
            
            var resultColor = aggregate < redScore ?
                                        ResultColor.Red :
                                        (aggregate >= greenScore ? ResultColor.Green : ResultColor.Yellow);
            return (aggregate, resultColor);
        }
    }
}
