using System.Globalization;
using System.Text.RegularExpressions;

namespace UtcToNzTimeConverter;
public class NzTimeConverter
{
    private static readonly string _standardFormat = "yyyy-MM-ddTHH:mm:ss.fff";
    private static readonly TimeZoneInfo _nztimeZone = TimeZoneInfo.FindSystemTimeZoneById("New Zealand Standard Time");

    public static string Convert(string timestamp)
    {
        bool mayHaveSpecifier = (timestamp.Length > _standardFormat.Length);

        // when "K" is added to the format validation string, the parser will accept
        // timestamps with a Z or UTC (e.g. +05:00) indicator, and will still accept timestamps 
        // without a timezone specifier
        string validationFormat = _standardFormat + "K";
        bool validTimeStamp = DateTime.TryParseExact(
                timestamp,
                validationFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.AdjustToUniversal,
                out DateTime dt
                );

        if (validTimeStamp && (mayHaveSpecifier))
        {
            dt = TimeZoneInfo.ConvertTimeFromUtc(dt, _nztimeZone);
            return dt.ToString(_standardFormat);
        }
        if (validTimeStamp)
        {
            // If no specified offset then as no conversion is necessary
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
