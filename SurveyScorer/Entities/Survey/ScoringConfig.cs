using System.Xml.Serialization;

namespace SurveyScorer.Entities.Survey
{
    public class ScoringConfig
    {
        public int AggregateGreenPercentage { get; set; }
        public int AggregateYellowPercentage { get; set; }
        public Question[] Questions { get; set; }
        [XmlElement("Metadata")]
        public Metadata[] Metadata { get; set; }
    }
}
