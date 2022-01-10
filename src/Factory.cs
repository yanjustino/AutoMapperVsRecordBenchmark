namespace MapperBenchmarkAspNet;

public static class Factory
{
    public static OrderEvent NewEventOrder() => new ()
    {
        Date = new DateTime(2008, 12, 15, 20, 30, 0),
        Code = Guid.NewGuid().ToString(),
        Ticker = "TICKER",
        Amount = new Random().Next(100, 10000),
        Fee = new Random().Next(10, 1000)
    };

    public static OrderEventInput NewOrderEventInput() => new ()
    {
        EventDate = new DateTime(2008, 12, 15, 0, 0, 0),
        EventHour = 20,
        EventMinute = 30,
        OrderInfo = new OrderEventInput.OrderInputInfo
        {
            Code = Guid.NewGuid().ToString(),
            Ticker = "TICKER",
            Amount = new Random().Next(100, 10000),
            Fee = new Random().Next(10, 1000)           
        }
    };
}