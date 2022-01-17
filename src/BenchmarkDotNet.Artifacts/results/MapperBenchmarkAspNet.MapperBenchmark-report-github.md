``` ini

BenchmarkDotNet=v0.13.1, OS=macOS Monterey 12.0.1 (21A559) [Darwin 21.1.0]
Intel Core i5-1030NG7 CPU 1.10GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100
  [Host]     : .NET 5.0.3 (5.0.321.7212), X64 RyuJIT
  Job-TXQCFC : .NET 5.0.3 (5.0.321.7212), X64 RyuJIT
  Job-IMSJKE : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT

OutlierMode=DontRemove  

```
|        Method |        Job |  Runtime | Categories |      Mean |    Error |   StdDev | Rank |
|-------------- |----------- |--------- |----------- |----------:|---------:|---------:|-----:|
|     PrjRecord | Job-TXQCFC | .NET 5.0 |        PRJ |  57.25 ns | 0.922 ns | 0.863 ns |    * |
|     PrjRecord | Job-IMSJKE | .NET 6.0 |        PRJ |  62.98 ns | 1.341 ns | 1.790 ns |   ** |
| PrjAutoMapper | Job-TXQCFC | .NET 5.0 |        PRJ | 146.04 ns | 0.429 ns | 0.402 ns |  *** |
| PrjAutoMapper | Job-IMSJKE | .NET 6.0 |        PRJ | 146.19 ns | 1.053 ns | 0.985 ns |  *** |
|               |            |          |            |           |          |          |      |
|     MpdRecord | Job-TXQCFC | .NET 5.0 |        MPD |  11.53 ns | 0.191 ns | 0.179 ns |    * |
|     MpdRecord | Job-IMSJKE | .NET 6.0 |        MPD |  11.70 ns | 0.069 ns | 0.064 ns |   ** |
| MpdAutoMapper | Job-TXQCFC | .NET 5.0 |        MPD | 128.56 ns | 0.415 ns | 0.388 ns |  *** |
| MpdAutoMapper | Job-IMSJKE | .NET 6.0 |        MPD | 134.00 ns | 1.038 ns | 0.971 ns | **** |
