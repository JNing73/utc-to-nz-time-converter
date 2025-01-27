using System.Globalization;

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
        if (mayBeZulu)
        {
            format += "Z";
        }

        bool validTimeStamp = DateTime.TryParseExact(
                timestamp,
                format,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime dt
                );

        if (validTimeStamp && mayBeZulu)
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
