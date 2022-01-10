using AutoMapper;

namespace MapperBenchmarkAspNet;

/// <summary>
/// Profile Instance
/// <see href="https://docs.automapper.org/en/stable/Configuration.html#profile-instances"/>
/// </summary>
public class CalendarAutoMapper : Profile
{
    /// <summary>
    /// Mapping based on Automapper projection sample
    /// <see href="https://docs.automapper.org/en/stable/Projection.html"/>
    /// </summary>
    public CalendarAutoMapper()
    {
        // Mapping Directly
        CreateMap<OrderEventInput, OrderEventOutput>();
        CreateMap<OrderEventInput.OrderInputInfo, OrderEventOutput.OrderOutputInfo>();

        // Projection
        CreateMap<OrderEvent, OrderEventOutput>()
            .ForMember(dest => dest.EventDate, opt => opt.MapFrom(src => src.Date.Date))
            .ForMember(dest => dest.EventHour, opt => opt.MapFrom(src => src.Date.Hour))
            .ForMember(dest => dest.EventMinute, opt => opt.MapFrom(src => src.Date.Minute));
        
        CreateMap<OrderEvent, OrderEventOutput.OrderOutputInfo>()        
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
            .ForMember(dest => dest.Fee, opt => opt.MapFrom(src => src.Fee))
            .ForMember(dest => dest.Ticker, opt => opt.MapFrom(src => src.Ticker));
    }

    /// <summary>
    /// Create a MapperConfiguration instance and initialize configuration via the constructor
    /// <see href="https://docs.automapper.org/en/stable/Configuration.html"/>
    /// </summary>
    /// <returns></returns>
    public static IMapper CreateMapper() => new MapperConfiguration(m => m.AddProfile<CalendarAutoMapper>()).CreateMapper();
}

/// <summary>
/// Parser using C# record
/// <see href="https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/records"/>
/// </summary>
public static class CalendarMapper
{
    /// <summary>
    /// Projection
    /// </summary>
    /// <param name="src"></param>
    /// <returns></returns>
    public static OrderEventOutput Parse(this OrderEvent src) => new()
    {
        EventDate = src.Date.Date,
        EventHour = src.Date.Hour,
        EventMinute = src.Date.Minute,
        OrderInfo = new OrderEventOutput.OrderOutputInfo
        {
            Code = src.Code,
            Ticker = src.Ticker,
            Amount = src.Amount,
            Fee = src.Fee
        }
    };

    /// <summary>
    /// Mapping directly
    /// </summary>
    /// <param name="input"></param>
    /// <returns>ICalendarEventForm</returns>
    public static IOrderEvent Parse(this OrderEventInput input) => input with { };
}