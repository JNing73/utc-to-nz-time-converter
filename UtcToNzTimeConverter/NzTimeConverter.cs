using System.Globalization;

namespace UtcToNzTimeConverter;
public class NzTimeConverter
{
    public static string Convert(string timestamp)
    {
        string format = "yyyy-MM-ddTHH:mm:ss.fff";

        bool validTimeStamp = DateTime.TryParseExact(
                timestamp,
                format,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime _
                );

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
