using SurveyScorer.Entities.Enums;
using System;
using System.Collections.Generic;

namespace SurveyScorer.Entities.Response
{
    public class ScoreCard
    {
        public string Project { get; set; }
        public string BU { get; set; }
        public string Client { get; set; }
        public string Summary { get; set; }
        public string TechLead { get; set; }
        public string BUHead { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Aggregate { get; set; }
        public ResultColor StateColor { get; set; }
        public IList<CategoryScore> ScoreItems { get; set; }
    }
}
