# UTC to NZ Local Converter

This is a .Net Console Application.

In summary it is a console application which receives a timestamp as a string
and converts it to a timestamp which represents the equivalent timestamp in NZ.

I chose to create this application to better understand working with 
datetimes and unit testing.

I had heard from a mentor that datetimes can be notoriously tricky and that
developers can be easily caught out by factors such as the location of the
host machine.

As such I decided this was a suitable topic to learn and was a natural fit
for implementing unit tests.

## Requirements
A console application which takes a string input and returns a string
- Three types of strings expected:
	- "2000-01-01T00:00:00.000" - no timezone specified, so assume no conversion
	required
	- "2000-01-01T00:00:00.000Z" - Z for Zulu time offset
	- "2000-01-01T00:00:00.000+05:00 - Indicating UTC+5 offset
- The returned string will match "New Zealand Standard Time" 
- Should account for Daylight Savings Time
- The application should function correctly even if the host machine is not set
  to the New Zealand timezone
  
## Key Concepts Demonstrated
- Working with DateTimes in C#
	- Format Validation
	- Timezone Conversions
- xUnit Testing
	- Test driven development principles
	- Data driven tests
- Continuous Integration
	- Setting up a Continuous Integration (CI) pipeline which builds and 
	  runs the tests on commits to main branch.
	- Setting up rulesets to protect the main branch.
