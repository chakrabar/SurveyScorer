using SurveyScorer.Application;
using SurveyScorer.Application.Helpers;
using SurveyScorer.Entities.Survey;
using SurveyScorer;
using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        AppSettings.Initialize();

        Console.WriteLine($"Excel path = {AppSettings.SurveyResultFilePath}");
        Console.WriteLine($"Template path = {AppSettings.ReportTemplatePath}");
        Console.WriteLine($"Scoring config = {AppSettings.ScoringRulesConfigPath}");
        Console.WriteLine($"Output directory = {AppSettings.OutputDirectory}");
        //Verify.TestSerialization();

        //test
        string scoringXml = File.ReadAllText(AppSettings.ScoringRulesConfigPath);
        ScoringConfig config = XmlHelper.DeserializeData<ScoringConfig>(scoringXml);

        //here is the actual application logic
        var scores = new SurveyExcelReader().ReadFromExcelFile(AppSettings.SurveyResultFilePath, config);
        //this section needs to be cleaned later

        Console.WriteLine("Scores calculated & reports generated.");
        Console.WriteLine("Press `Enter` to exit...");
    }
}