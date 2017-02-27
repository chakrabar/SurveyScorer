using SurveyScorer.Application.Helpers;
using SurveyScorer.Entities.Survey;

namespace SurveyScorer
{
    class Verify
    {
        internal static void TestSerialization()
        {
            //test
            var test = new ScoringConfig
            {
                Questions = new Question[]
                {
                    new Question
                    {
                        Options = new Option[]
                        {
                            new Option
                            {
                                Score = 10,
                                Value = "First option"
                            },
                            new Option
                            {
                                Score = 20,
                                Value = "Second option"
                            }
                        },
                        Query = "Select an option",
                        ReportTag = "opt1",
                        ScoringRule = new ScoringRule
                        {
                            GreenScore = 10,
                            RedScore = null
                        },
                        SingleAnswerOnly = true
                    },
                    new Question
                    {
                        Options = new Option[]
                        {
                            new Option
                            {
                                Score = 100,
                                Value = "Good option"
                            },
                            new Option
                            {
                                Score = 20,
                                Value = "Bad option"
                            }
                        },
                        Query = "Select another option",
                        ReportTag = "opt2",
                        ScoringRule = new ScoringRule
                        {
                            GreenScore = 100,
                            RedScore = 20
                        },
                        SingleAnswerOnly = true
                    }
                },
                Metadata = new Metadata[]
                {
                    new Metadata
                    {
                        Query = "Project name",
                        ReportTag = "project"
                    },
                    new Metadata
                    {
                        Query = "Project BU",
                        ReportTag = "bu"
                    }
                }
            };
            var xml = XmlHelper.SerializeData(test);
        }
    }
}
