using System.Security.Cryptography;
using Xunit;

namespace UtcToNzTimeConverter
{
    public class NzTimeConverterTests
    {
        [Theory]
        [InlineData("2000-01-01T00:00:00.000")]
        [InlineData("2008-09-15T14:23:11.789")]
        [InlineData("2016-12-05T17:00:01.789")] 
        [InlineData("1997-11-17T23:45:59.987")]
        [InlineData("2020-02-29T16:37:11.876")] // Leap Year
        [InlineData("2024-02-29T08:15:47.123")] // Leap Year
        public void Convert_ValidStringNoTimezoneOffset_ReturnsInput(String timestamp)
        {
            Assert.Equal(timestamp, NzTimeConverter.Convert(timestamp));
        }

        [Theory]
        [InlineData("")] // Empty String
        [InlineData("adfhkladjh")] // Incoherent String

        [InlineData("19B0-01-01T00:00:00.000")] // Letter instead of an integer
        [InlineData("1900.01.01T00:00:00.000")] // Incorrect date separator

        [InlineData("2001-13-22T00:00:00.000")] // Non-existent month
        [InlineData("2006-04-31T00:00:00.000")] // Non-existent day
        [InlineData("2015-02-29T00:00:00.000")] // Non-existent day (non-leap year)

        [InlineData("2016-05-15T25:25:00.000")] // Invalid hours
        [InlineData("2016-05-15T25:00:61.000")] // Invalid minutes
        [InlineData("2016-05-15T25:30:99.000")] // Invalid time and minutes
        [InlineData("2016-15-23T25:30:99.000")] // Invalid day, time

        [InlineData("2016-12-05T17:00:01.7890")] // An additional value at the end
        [InlineData("2016-12-05T17:00:01.78")] // Missing last millisecond indicator

        [InlineData("2008-07-15T07:00:00.000+15:00")] // Invalid UTC modifier
        [InlineData("2008-07-15T07:00:00.000+03:5")] // Invalid UTC modifier
        [InlineData("2008-07-15T07:00:00.000A")] // Invalid UTC modifier
        [InlineData("2008-07-15T07:00:00+02:00")] // Valid modifier but missing millisecond value
        public void Convert_InvalidStringFormat_ThrowsException(String timestamp)
        {
            var exception = Assert.Throws<ArgumentException>(() => NzTimeConverter.Convert(timestamp));
            Assert.Equal(
                $"Please check the input: \"{timestamp}\" " +
                $"for formatting errors or invalid dates and/or times"
                , exception.Message
                );
        }

        [Theory]
        [InlineData("2008-07-15T19:00:00.000", "2008-07-15T07:00:00.000Z")] // Non DST day (UTC + 12)
        [InlineData("2008-07-15T15:56:23.632", "2008-07-15T03:56:23.632Z")] // Non DST day (UTC + 12)
        [InlineData("2012-08-28T22:56:23.632", "2012-08-28T10:56:23.632Z")] // Non DST day (UTC + 12)
        [InlineData("2012-08-29T00:56:23.632", "2012-08-28T12:56:23.632Z")] // Non DST day - Rollover to next day
        [InlineData("2015-06-23T02:10:33.142", "2015-06-22T14:10:33.142Z")] // Non DST day - Rollover to next day
        [InlineData("2024-04-06T13:00:00.000", "2024-04-06T00:00:00.000Z")] // Last day of DST 2024 (UTC + 13)
        [InlineData("2024-04-07T12:00:00.000", "2024-04-07T00:00:00.000Z")] // End of DST 2024 (UTC + 12)
        [InlineData("2024-09-29T13:23:08.111", "2024-09-29T00:23:08.111Z")] // First day of DST 2024 (UTC + 13)
        [InlineData("2025-04-05T13:00:00.000", "2025-04-05T00:00:00.000Z")] // Last day of DST 2025 (UTC + 13)
        [InlineData("2025-04-06T12:00:00.000", "2025-04-06T00:00:00.000Z")] // End of DST 2025 (UTC + 12)
        public void Covert_ZuluTimeOffset_ReturnsConvertedValue(string expected, string input)
        {
            Assert.Equal(expected, NzTimeConverter.Convert(input));
        }

        [Theory]
        [InlineData("2008-07-15T08:00:00.000", "2008-07-15T07:00:00.000+11:00")] // UTC+11 -> UTC+12
        [InlineData("2008-07-15T14:00:00.000", "2008-07-15T07:00:00.000+05:00")] // UTC+5 -> UTC+12
        [InlineData("2008-07-16T07:00:00.000", "2008-07-15T07:00:00.000-12:00")] // Negative offset with date rollover
        [InlineData("2008-07-15T15:56:23.632", "2008-07-15T03:56:23.632+00:00")] // Zero offset (positive)
        [InlineData("2008-07-15T15:56:23.632", "2008-07-15T03:56:23.632-00:00")] // Zero offset (negative)
        [InlineData("2008-07-15T07:00:00.000", "2008-07-15T07:00:00.000+12:00")] // UTC+12 -> UTC+12
        [InlineData("2024-04-06T01:00:00.000", "2024-04-06T00:00:00.000+12:00")] // UTC+12 -> UTC+13 (DST)
        [InlineData("2015-06-22T18:10:33.142", "2015-06-22T14:10:33.142+08:00")] // UTC+8 -> UTC+12 (Not DST)
        [InlineData("2015-06-22T18:10:33.142", "2015-06-22T05:10:33.142-01:00")] // UTC-11 -> UTC+12 (Not DST)
        public void Convert_UtcTimeOffset_ReturnsConvertedValue(string expected, string input)
        {
            Assert.Equal(expected, NzTimeConverter.Convert(input));
        }
    }
}
