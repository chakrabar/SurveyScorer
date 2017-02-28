using SurveyScorer.Entities.Enums;
using System.Collections.Generic;

namespace SurveyScorer.Entities.Response
{
    public class ScoreCard
    {
        public decimal Aggregate { get; set; }
        public ResultColor ResultColor { get; set; }
        public IList<MetaResponse> Metadata { get; set; }
        public IList<CategoryScore> ScoreItems { get; set; }
    }
}
