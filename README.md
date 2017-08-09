# SurveyScorer

Generate score card and reports from survey (or MCQ exam, or something similar) responses.

Basically I created this tool to generate aggregate resilts/scores from different surveys we used to do on [Google forms](https://www.google.com/forms/about/). But this doesn't have a dependency on Google forms as such, and it doesn't even interact with that. All it needs are:

    1. The survey/exam responses in form of an Excel file (.xlsx)
    2. The scoring criteria in XML format
    3. An HTML template for individual results/marksheets
    
This is written on C# using targetting .NETCore1.0  
You can build it to be a standalone executable or portable application (the client machine will need .Net framework)

---

To use the application

    1. Build it (targetting whichever platrom you need)
    2. Create files [2] and [3] as said above
    3. Get your result set (responses of the survey/exam in .xlsx format)
    4. Update the 'appsettings.json' file and set valid values for all fields
    5. Run the application
    6. Your desired results will be in 'output.directory' as specified in [4]
