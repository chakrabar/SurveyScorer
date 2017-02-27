using System.Xml.Serialization;

namespace SurveyScorer.Entities.Survey
{
    public class ScoringConfig
    {
        [XmlElement("Question")]
        public Question[] Questions { get; set; }
        [XmlElement("Metadata")]
        public Metadata[] Metadata { get; set; }
    }
}
