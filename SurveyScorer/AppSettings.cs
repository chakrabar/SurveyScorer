using Microsoft.Extensions.Configuration;
using System.IO;

namespace SurveyScorer
{
    static class AppSettings
    {
        private static IConfigurationRoot Configuration { get; set; }

        internal static string SurveyResultFilePath { get; set; }
        internal static string ReportTemplatePath { get; private set; }
        internal static string ScoringRulesConfigPath { get; private set; }
        internal static string OutputDirectory { get; private set; }
        internal static string OutputFilePrefix { get; private set; }
        internal static string ReportPrefix { get; private set; }

        internal static void Initialize()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            SurveyResultFilePath = Configuration["surveyExcelPath"];
            ReportTemplatePath = Configuration["reportTemplatePath"];
            ScoringRulesConfigPath = Configuration["scoringRules"];
            OutputDirectory = Configuration["output:directory"];
            OutputFilePrefix = Configuration["output:scoreSheetPrefix"];
            ReportPrefix = Configuration["output:reportPrefix"];
        }
    }
}
