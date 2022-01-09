namespace MapperBenchmarkAspNet;

public static class Factory
{
    public static CalendarEvent NewCalendarEvent() => new ()
    {
        Date = new DateTime(2008, 12, 15, 20, 30, 0),
        Title = "Company Holiday Party"
    };

    public static CalendarEventFormInput NewCalendarEventFormInput() => new ()
    {
        EventDate = new DateTime(2008, 12, 15, 0, 0, 0),
        EventHour = 20,
        EventMinute = 30,
        Title = "Company Holiday Party"
    };
}