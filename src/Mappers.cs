using AutoMapper;

namespace MapperBenchmarkAspNet;

/// <summary>
/// Profile Instance
/// <see href="https://docs.automapper.org/en/stable/Configuration.html#profile-instances"/>
/// </summary>
public class CalendarMapper : Profile
{
    /// <summary>
    /// Mapping based on Automapper projection sample
    /// <see href="https://docs.automapper.org/en/stable/Projection.html"/>
    /// </summary>
    public CalendarMapper()
    {
        // Mapping Directly
        CreateMap<CalendarEventFormInput, CalendarEventOutput>();

        // Projection
        //
        CreateMap<CalendarEvent, CalendarEventOutput>()
            .ForMember(dest => dest.EventDate, opt => opt.MapFrom(src => src.Date.Date))
            .ForMember(dest => dest.EventHour, opt => opt.MapFrom(src => src.Date.Hour))
            .ForMember(dest => dest.EventMinute, opt => opt.MapFrom(src => src.Date.Minute));
    }

    /// <summary>
    /// Create a MapperConfiguration instance and initialize configuration via the constructor
    /// <see href="https://docs.automapper.org/en/stable/Configuration.html"/>
    /// </summary>
    /// <returns></returns>
    public static IMapper CreateMapper() => new MapperConfiguration(m => m.AddProfile<CalendarMapper>()).CreateMapper();
}

/// <summary>
/// Parser using C# record
/// <see href="https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/records"/>
/// </summary>
public static class CalendarParser
{
    /// <summary>
    /// Projection
    /// </summary>
    /// <param name="src"></param>
    /// <returns></returns>
    public static CalendarEventOutput Parse(this CalendarEvent src) => new()
    {
        EventDate = src.Date.Date,
        EventHour = src.Date.Hour,
        EventMinute = src.Date.Minute,
        Title = src.Title
    };

    /// <summary>
    /// Mapping directly
    /// </summary>
    /// <param name="input"></param>
    /// <returns>ICalendarEventForm</returns>
    public static ICalendarEventForm Parse(this CalendarEventFormInput input) => input with { };
}