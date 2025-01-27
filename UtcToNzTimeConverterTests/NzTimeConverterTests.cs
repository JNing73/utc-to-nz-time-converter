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

    }
}
