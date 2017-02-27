using System.Xml.Serialization;

namespace SurveyScorer.Entities.Survey
{
    public class Metadata
    {
        [XmlAttribute]
        public string Query { get; set; }
        public string ReportTag { get; set; }
        public int? ColumnNumber { get; set; }
    }
}
