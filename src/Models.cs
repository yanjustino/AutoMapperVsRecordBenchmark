namespace MapperBenchmarkAspNet;

public record CalendarEvent
{
    public DateTime Date { get; set; }
    public string? Title { get; set; }
}

public interface ICalendarEventForm
{
    public DateTime EventDate { get; set; }
    public int EventHour { get; set; }
    public int EventMinute { get; set; }
    public string? Title { get; set; }
}

public record CalendarEventFormInput : ICalendarEventForm
{
    public DateTime EventDate { get; set; }
    public int EventHour { get; set; }
    public int EventMinute { get; set; }
    public string? Title { get; set; }
}

public record CalendarEventOutput : ICalendarEventForm
{
    public DateTime EventDate { get; set; }
    public int EventHour { get; set; }
    public int EventMinute { get; set; }
    public string? Title { get; set; }
}