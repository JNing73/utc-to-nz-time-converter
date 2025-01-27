using System.Globalization;
using System.Text.RegularExpressions;

namespace UtcToNzTimeConverter;
public class NzTimeConverter
{
    private static readonly string _standardFormat = "yyyy-MM-ddTHH:mm:ss.fff";

    public static string Convert(string timestamp)
    {
        string format = _standardFormat;

        bool mayBeZulu = (
            timestamp.Length > 0 &&
            timestamp.Substring(timestamp.Length - 1) == "Z"
            );

        bool mayBeOffset = IsUtcOffset(timestamp);

        if (mayBeZulu)
        {
            format += "Z";
        }
        else if (mayBeOffset)
        {
            format += "K";
        }

        bool validTimeStamp = DateTime.TryParseExact(
                timestamp,
                format,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime dt
                );

        if (validTimeStamp && (mayBeZulu || mayBeOffset))
        {
            return dt.ToString(_standardFormat);
        }
        if (validTimeStamp)
        {
            return timestamp;
        }
        else
        {
            throw new ArgumentException(
                $"Please check the input: \"{timestamp}\" " +
                $"for formatting errors or invalid dates and/or times"
                );
        }
    }

    public static bool IsUtcOffset(string timestamp)
    {
        string pattern = @"[+-]\d{2}:\d{2}$";
        return Regex.IsMatch(timestamp, pattern);
    }
}
