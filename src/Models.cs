namespace MapperBenchmarkAspNet;

public record OrderEvent
{
    public DateTime Date { get; init; } = DateTime.Now;
    public string Code { get; init; } = string.Empty;
    public string Ticker { get; init; } = string.Empty;
    public decimal Amount { get; init; } = 0;
    public decimal Fee { get; init; } = 0;
}

public interface IOrderEvent
{
    public DateTime EventDate { get; init; }
    public int EventHour { get; init; }
    public int EventMinute { get; init; }
    public IOrderInfo? OrderInfo { get; init; }
}

public interface IOrderInfo
{
    public string Code { get; init; }
    public string Ticker { get; init; }
    public decimal Amount { get; init; }
    public decimal Fee { get; init; }
    
}

public record OrderEventInput: IOrderEvent
{
    public DateTime EventDate { get; init; }
    public int EventHour { get; init; }
    public int EventMinute { get; init; }
    public IOrderInfo? OrderInfo { get; init; } = new OrderInputInfo();

    public record OrderInputInfo : IOrderInfo
    {
        public string Code { get; init; } = string.Empty;
        public string Ticker { get; init; } = string.Empty;
        public decimal Amount { get; init; }
        public decimal Fee { get; init; }        
    }
}

public record OrderEventOutput : IOrderEvent
{
    public DateTime EventDate { get; init; }
    public int EventHour { get; init; }
    public int EventMinute { get; init; }
    public IOrderInfo? OrderInfo { get; init; } = new OrderOutputInfo();

    public record OrderOutputInfo : IOrderInfo
    {
        public string Code { get; init; } = string.Empty;
        public string Ticker { get; init; } = string.Empty;
        public decimal Amount { get; init; }
        public decimal Fee { get; init; }        
    }
}