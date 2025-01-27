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
        public void Convert_ValidStringNoTimezoneOffset_ReturnsInput(String dateTimeString)
        {
            Assert.Equal(dateTimeString, NzTimeConverter.Convert(dateTimeString));
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
        public void Convert_InvalidStringFormat_ThrowsException(String dateTimeString)
        {
            var exception = Assert.Throws<ArgumentException>(() => NzTimeConverter.Convert(dateTimeString));
            Assert.Equal(
                $"Please check the input: \"{dateTimeString}\" " +
                $"for formatting errors or invalid dates and/or times"
                , exception.Message
                );
        }
    }
}
