using System.Xml.Serialization;

namespace SurveyScorer.Entities.Survey
{
    public class Question
    {
        [XmlAttribute]
        public string Query { get; set; }
        public bool SingleAnswerOnly { get; set; }
        public Option[] Options { get; set; }
        public ScoringRule ScoringRule { get; set; }
        public string ReportTag { get; set; }
        public int? ColumnNumber { get; set; }
    }
}
