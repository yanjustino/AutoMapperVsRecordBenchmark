using AutoMapper;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using Perfolizer.Mathematics.OutlierDetection;

namespace MapperBenchmarkAspNet;

/// <summary>
/// Using benchmarkdotnet
/// <see href="https://benchmarkdotnet.org/articles/overview.html"/>
/// </summary>
[CategoriesColumn]
[Config(typeof(Config))]
[Outliers(OutlierMode.DontRemove)]
[SimpleJob(RuntimeMoniker.Net50, id:".Net 50")]
[SimpleJob(RuntimeMoniker.Net60, id:".Net 60")]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[MarkdownExporter, HtmlExporter, RPlotExporter]
public class MapperBenchmark
{
    private IMapper? Mapper { get; set; }
    private OrderEvent? Order { get; set; }
    private OrderEventInput? Input { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        Mapper = CalendarAutoMapper.CreateMapper();
        Order = Factory.NewEventOrder();
        Input = Factory.NewOrderEventInput();
    }    

    [BenchmarkCategory("PRJ"), Benchmark]
    public IOrderEvent PrjAutoMapper() => Mapper!.Map<OrderEventOutput>(Order);

    [BenchmarkCategory("PRJ"), Benchmark]
    public IOrderEvent PrjRecord() => Order!.Parse();
    
    [BenchmarkCategory("MPD"), Benchmark]
    public IOrderEvent MpdAutoMapper() => Mapper!.Map<OrderEventOutput>(Input);

    [BenchmarkCategory("MPD"), Benchmark]
    public IOrderEvent MpdRecord() => Input!.Parse();    
    
    [GlobalCleanup]
    public void GlobalCleanup()
    {
        Mapper = null;
        Order = null;
        Input = null;
    }    
}

public class Config : ManualConfig
{
    public Config()
    {
        AddColumn(
            StatisticColumn.P0,
            StatisticColumn.P25,
            StatisticColumn.P50,
            StatisticColumn.P67,
            StatisticColumn.P80,
            StatisticColumn.P85,
            StatisticColumn.P90,
            StatisticColumn.P95,
            StatisticColumn.P100);
    }
}