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
    private CalendarEvent? Event { get; set; }
    private CalendarEventFormInput? EventFormInput { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        Mapper = CalendarAutoMapper.CreateMapper();
        Event = Factory.NewCalendarEvent();
        EventFormInput = Factory.NewCalendarEventFormInput();
    }    

    [BenchmarkCategory("PRJ"), Benchmark]
    public ICalendarEventForm PrjAutoMapper() => Mapper!.Map<CalendarEventOutput>(Event);

    [BenchmarkCategory("PRJ"), Benchmark]
    public ICalendarEventForm PrjRecord() => Event!.Parse();
    
    [BenchmarkCategory("MPD"), Benchmark]
    public ICalendarEventForm MpdAutoMapper() => Mapper!.Map<CalendarEventOutput>(EventFormInput);

    [BenchmarkCategory("MPD"), Benchmark]
    public ICalendarEventForm MpdRecord() => EventFormInput!.Parse();    
    
    [GlobalCleanup]
    public void GlobalCleanup()
    {
        Mapper = null;
        Event = null;
        EventFormInput = null;
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