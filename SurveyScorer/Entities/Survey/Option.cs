using System.Xml.Serialization;

namespace SurveyScorer.Entities.Survey
{
    public class Option
    {
        [XmlText]
        public string Value { get; set; }
        [XmlAttribute]
        public decimal Score { get; set; }
    }
}
