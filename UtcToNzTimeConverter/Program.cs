using UtcToNzTimeConverter;

string expectedFormatExamples =
    $"""
    Expected format:yyy-MM-ddTHH:mm:ss.fff (optional: "Z" or UTC offset e.g. +05:00)

    Examples:
    2008-09-15T14:23:11.789
    2008-09-15T14:23:11.789Z
    2008-09-15T14:23:11.789+05:00{"\n"}
    """;

while (true)
{
    try
    {
        Console.Clear();
        Console.WriteLine(expectedFormatExamples);
        Console.WriteLine("Please provide the timestamp to be converted");

        string timestamp = Console.ReadLine()!;
        string convertedTimestamp = NzTimeConverter.Convert(timestamp);

        Console.WriteLine($"\nConverted to New Zealand Standard Time: {convertedTimestamp}");
        return;
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"\n{ex.Message}\n");
        Console.WriteLine("Press 'Enter to retry...");
        Console.ReadLine();
    }
}