using System.Globalization;
using System.Text.RegularExpressions;

namespace UtcToNzTimeConverter;
public class NzTimeConverter
{
    private static readonly string _standardFormat = "yyyy-MM-ddTHH:mm:ss.fff";

    public static string Convert(string timestamp)
    {
        string format = _standardFormat;

        bool mayHaveSpecifier = false;
        if (timestamp.Length > _standardFormat.Length)
        {
            mayHaveSpecifier = true;
            format += "K"; // 'K' denotes both Zulu time and UTC offsets in the format patterb
        }

        bool validTimeStamp = DateTime.TryParseExact(
                timestamp,
                format,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime dt
                );

        if (validTimeStamp && (mayHaveSpecifier))
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
}
