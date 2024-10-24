using System.Text.RegularExpressions;

namespace NotificationChannelParser;

class Program
{

    #region "Fields"

    private static readonly List<string> ValidChannels = new() { "BE", "FE", "QA", "Urgent" };

    private const string AppTitle = "NOTIFICATION CHANNELS PARSER";
    private const string Pattern = @"\[(.*?)\]";
    private const bool ClearConsole = true; // Used to the console screen.

    #endregion

    static void Main(string[] args)
    {
        do
        {
            TitlePrinter();

            var input = Console.ReadLine();

            if (IsInValid(input)) continue;

            ParseNotificationChannelsLinq(input!, out var result);

            PrintStartedIndex($"Output : {result}", false);

            ClearScreen();
        } while (true);
    }

    #region "Channels Parser Operator Methods"

    /// <summary>
    /// Parse notification channels from input string using a traditional method with a loop
    /// Indentifies and recognizes valid channels defined in <see cref="ValidChannels"/>
    /// </summary>
    /// <param name="input">The input string</param>
    /// <param name="result">The output string containing the recognized channels or message if none channel was found.</param>
    private static void ParseNotificationChannels(string input, out string result)
    {
        var matches = Regex.Matches(input, Pattern);
        var recognizedChannels = new List<string>();
        foreach (Match match in matches)
        {
            var tag = match.Groups[1].Value;
            if (ValidChannels.Contains(tag) && !recognizedChannels.Contains(tag))
            {
                recognizedChannels.Add(tag);
            }
        }
        result = recognizedChannels.Any()
            ? $"Receive Channels : {string.Join(",", recognizedChannels)}"
            : "No valid notification channels found.";
    }

    /// <summary>
    /// Parse notification channels from input string using LINQ method
    /// Indentifies and recognizes valid channels defined in <see cref="ValidChannels"/>
    /// </summary>
    /// <param name="input">The input string</param>
    /// <param name="result">The output string containing the recognized channels or message if none channel was found.</param>
    private static void ParseNotificationChannelsLinq(string input, out string result)
    {
        var matches = Regex.Matches(input, Pattern);
        var recognizedChannels = matches.Select(x => x.Groups[1].Value).Where(tag => ValidChannels.Contains(tag)).Distinct().ToList();
        result = recognizedChannels.Count > 0
            ? $"Receive Channels : {string.Join(",", recognizedChannels)}"
            : "No valid notification channels found.";
    }

    #endregion

    #region "Validation"

    /// <summary>
    /// Checks whether inputted string is null or whitespace
    /// </summary>
    /// <param name="input">The input string to validate</param>
    /// <returns>
    /// Return <c>true</c> if the input is null or consists only whitespace; otherwise, return <c>false</c>.
    /// </returns>
    private static bool IsInValid(string? input)
    {
        if (!string.IsNullOrWhiteSpace(input)) return false;
        PrintStartedIndex("Output : Entered text was empty ! Please try again...");
        Console.ReadKey();
        if (ClearConsole)
            Console.Clear();
        return true;
    }

    #endregion

    #region "UI"

    /// <summary>
    /// <b>Prints Title</b>
    /// </summary>
    private static void TitlePrinter()
    {
        PrintCentered(AppTitle);
        PrintCentered(new string('-', AppTitle.Length));
        PrintStartedIndex("Input : ");
    }

    /// <summary>
    /// <b>Clears</b> the console screen if <see cref="ClearConsole"/> field is set to <c>true</c>.
    /// </summary>
    private static void ClearScreen()
    {
        if (!ClearConsole) return;
        PrintStartedIndex(@$"Press any key to continue.....", false);
        Console.ReadKey();
        Console.Clear();
    }

    #region "Printing Methods"

    /// <summary>
    /// Print at the <b>center</b> of the console windows size
    /// </summary>
    /// <param name="text">The text to be <b>printed</b></param>
    static void PrintCentered(string text)
    {
        var windowWidth = Console.WindowWidth;
        var padding = (windowWidth - text.Length) / 2;
        if (padding < 0) padding = 0;
        Console.WriteLine($"{new string(' ', padding)}{text}");
    }

    /// <summary>
    /// Prints at the started index of the "<see cref="AppTitle"/>", with optional indentation.
    /// </summary>
    /// <param name="text">The <b>Text</b> to be <b>printed</b></param>
    /// <param name="input">If <c>true</c>, prints without indentation; if <c>false</c>, prints with indentation</param>
    static void PrintStartedIndex(string text, bool input = true)
    {
        var windowWidth = Console.WindowWidth;
        var padding = ((windowWidth - AppTitle.Length) / 2) - (AppTitle.Length / 2);
        var result = $"{new string(' ', padding)}{text}";
        if (input)
            Console.Write(result);
        else
            Console.WriteLine(result);
    }

    #endregion

    #endregion

}