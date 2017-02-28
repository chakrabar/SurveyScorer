using SurveyScorer;
using SurveyScorer.Application;
using SurveyScorer.Application.Helpers;
using SurveyScorer.Entities.Survey;
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
        Console.WriteLine("Press `Enter` to continue...");
        Console.ReadLine();

        string scoringXml = File.ReadAllText(AppSettings.ScoringRulesConfigPath);
        ScoringConfig config = XmlHelper.DeserializeData<ScoringConfig>(scoringXml);
        var outputDir = FileHelper.CreateDirectoryWithTimestamp(AppSettings.OutputDirectory, null);

        //here is the actual application logic
        var scores = new SurveyExcelReader().ReadFromExcelFile(AppSettings.SurveyResultFilePath, config);

        var excelSummary = SurveyResultGenerator.CreateExcel(scores);
        FileHelper.CreateFileWithTimestampAndWrite(excelSummary, outputDir, AppSettings.OutputFilePrefix, "xlsx");

        //var html = SurveyReportGenerator.Create(scores[0], AppSettings.ReportTemplatePath);
        SurveyReportGenerator.Process(scores, AppSettings.ReportTemplatePath, outputDir);

        Console.WriteLine("Scores calculated & reports generated. See output directory.");
        Console.WriteLine("Press `Enter` to exit...");
        Console.ReadLine();
    }
}