# Avaliação de performance: uso da biblioteca AutoMapper versus abordagem _Adhoc_ para mapeamento objeto-objeto no tipo record da linguagem C#

## Introdução 
O mapeamento objeto-objeto é uma técnica muito comum na programação orientada a objetos (POO). Sua adoção está associada à necessidade de intercambiar estruturas entre objetos. 
A literatura de engenharia de software já documentou diversos padrões que dão suporte à diferentes tipos de mecanismos de mapeamento [[1]](https://martinfowler.com/eaaCatalog/index.html) [[2]](https://refactoring.guru/design-patterns/prototype).
Contudo, uma forma de se obter o mapeamento objeto-objeto, de forma a abstrair a necessidade de implementar tais padrões, é adotar ferramentas atendam tal propósito. 

- Apresentar o Automapper
- O a adoção de ferramentas devem estar alinhadas com os propósitos de qualidade do projeto de software
- 

``` ini
BenchmarkDotNet=v0.13.1, OS=macOS Monterey 12.0.1 (21A559) [Darwin 21.1.0]
Intel Core i5-1030NG7 CPU 1.10GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100
  [Host]     : .NET 5.0.3 (5.0.321.7212), X64 RyuJIT
  Job-GWFPFL : .NET 5.0.3 (5.0.321.7212), X64 RyuJIT
  Job-TXFNNH : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT

OutlierMode=DontRemove  

```

| Method          | Categories | Descrição             |
|-----------------|----------- |-----------------------|
| `PrjAutoMapper` |        PRJ | Projeção Automapper   |
| `PrjRecord`     |        PRJ | Projeção _Adhoc_      |
| `MpdAutoMapper` |        MPD | Mapeamento Automapper |
| `MpdRecord`     |        MPD | Mapeamento _Adhoc_    |

### Visão geral

| Method                |  Runtime |      Mean |    Error |   StdDev |    Median |
|-----------------------|--------- |----------:|---------:|---------:|----------:|
| Projeção Automapper   | .NET 5.0 | 144.78 ns | 2.835 ns | 5.040 ns | 144.30 ns |
| Projeção _Adhoc_      | .NET 5.0 |  21.56 ns | 0.516 ns | 0.614 ns |  21.30 ns |
| Projeção Automapper   | .NET 6.0 | 147.08 ns | 2.877 ns | 3.939 ns | 147.35 ns |
| Projeção _Adhoc_      | .NET 6.0 |  17.08 ns | 0.439 ns | 0.488 ns |  17.09 ns |
|                       |          |           |          |          |           |
| Mapeamento Automapper | .NET 5.0 | 129.34 ns | 2.683 ns | 4.839 ns | 128.40 ns |
| Mapeamento _Adhoc_    | .NET 5.0 |  14.20 ns | 0.841 ns | 2.479 ns |  13.50 ns |
| Mapeamento Automapper | .NET 6.0 | 135.10 ns | 2.695 ns | 3.865 ns | 134.55 ns |
| Mapeamento _Adhoc_    | .NET 6.0 |  12.76 ns | 0.350 ns | 0.576 ns |  12.77 ns |

### Análise Percentil

| Method                |  Runtime |        P0 |       P25 |       P50 |       P67 |       P80 |       P85 |       P90 |       P95 |      P100 |
|-----------------------|--------- |----------:|----------:|----------:|----------:|----------:|----------:|----------:|----------:|----------:|
| Projeção Automapper   | .NET 5.0 | 136.37 ns | 141.86 ns | 144.30 ns | 144.97 ns | 147.06 ns | 149.22 ns | 150.31 ns | 156.85 ns | 158.77 ns |
| Projeção _Adhoc_      | .NET 5.0 |  20.88 ns |  21.15 ns |  21.30 ns |  21.65 ns |  21.85 ns |  22.21 ns |  22.30 ns |  22.37 ns |  23.42 ns |
| Projeção Automapper   | .NET 6.0 | 139.06 ns | 144.46 ns | 147.35 ns | 148.67 ns | 149.57 ns | 150.24 ns | 151.78 ns | 153.59 ns | 155.62 ns |
| Projeção _Adhoc_      | .NET 6.0 |  16.19 ns |  16.68 ns |  17.09 ns |  17.31 ns |  17.43 ns |  17.48 ns |  17.58 ns |  17.69 ns |  18.17 ns |
|                       |          |           |           |           |           |           |           |           |           |           |
| Mapeamento Automapper | .NET 5.0 | 123.05 ns | 126.28 ns | 128.40 ns | 130.70 ns | 132.46 ns | 133.41 ns | 134.80 ns | 135.91 ns | 149.67 ns |
| Mapeamento _Adhoc_    | .NET 5.0 |  12.39 ns |  13.21 ns |  13.50 ns |  13.94 ns |  14.50 ns |  15.10 ns |  16.02 ns |  17.52 ns |  34.60 ns |
| Mapeamento Automapper | .NET 6.0 | 129.17 ns | 132.01 ns | 134.55 ns | 136.34 ns | 138.18 ns | 139.03 ns | 139.57 ns | 142.08 ns | 144.23 ns |
| Mapeamento _Adhoc_    | .NET 6.0 |  11.66 ns |  12.42 ns |  12.77 ns |  13.01 ns |  13.13 ns |  13.15 ns |  13.35 ns |  13.49 ns |  14.61 ns |

```shell
// * Detailed results *
MapperBenchmark.PrjAutoMapper: Job-GWFPFL(OutlierMode=DontRemove, Runtime=.NET 5.0)
Runtime = .NET 5.0.3 (5.0.321.7212), X64 RyuJIT; GC = Concurrent Server
Mean = 144.780 ns, StdErr = 0.797 ns (0.55%), N = 40, StdDev = 5.040 ns
Min = 136.373 ns, Q1 = 141.859 ns, Median = 144.296 ns, Q3 = 146.080 ns, Max = 158.770 ns
IQR = 4.221 ns, LowerFence = 135.528 ns, UpperFence = 152.412 ns
ConfidenceInterval = [141.945 ns; 147.615 ns] (CI 99.9%), Margin = 2.835 ns (1.96% of Mean)
Skewness = 1.07, Kurtosis = 3.98, MValue = 2
-------------------- Histogram --------------------
[134.439 ns ; 137.745 ns) | @
[137.745 ns ; 141.919 ns) | @@@@@@@@@
[141.919 ns ; 145.787 ns) | @@@@@@@@@@@@@@@@@@@
[145.787 ns ; 149.558 ns) | @@@@@@
[149.558 ns ; 153.668 ns) | @@
[153.668 ns ; 159.736 ns) | @@@
---------------------------------------------------

MapperBenchmark.PrjRecord: Job-GWFPFL(OutlierMode=DontRemove, Runtime=.NET 5.0)
Runtime = .NET 5.0.3 (5.0.321.7212), X64 RyuJIT; GC = Concurrent Server
Mean = 21.557 ns, StdErr = 0.134 ns (0.62%), N = 21, StdDev = 0.614 ns
Min = 20.884 ns, Q1 = 21.152 ns, Median = 21.300 ns, Q3 = 21.783 ns, Max = 23.422 ns
IQR = 0.632 ns, LowerFence = 20.204 ns, UpperFence = 22.731 ns
ConfidenceInterval = [21.041 ns; 22.073 ns] (CI 99.9%), Margin = 0.516 ns (2.39% of Mean)
Skewness = 1.36, Kurtosis = 4.59, MValue = 2
-------------------- Histogram --------------------
[20.853 ns ; 21.437 ns) | @@@@@@@@@@@@
[21.437 ns ; 22.021 ns) | @@@@@
[22.021 ns ; 22.583 ns) | @@@
[22.583 ns ; 23.130 ns) | 
[23.130 ns ; 23.714 ns) | @
---------------------------------------------------

MapperBenchmark.PrjAutoMapper: Job-TXFNNH(OutlierMode=DontRemove, Runtime=.NET 6.0)
Runtime = .NET 6.0.0 (6.0.21.52210), X64 RyuJIT; GC = Concurrent Server
Mean = 147.079 ns, StdErr = 0.772 ns (0.53%), N = 26, StdDev = 3.939 ns
Min = 139.061 ns, Q1 = 144.465 ns, Median = 147.354 ns, Q3 = 149.019 ns, Max = 155.618 ns
IQR = 4.555 ns, LowerFence = 137.632 ns, UpperFence = 155.852 ns
ConfidenceInterval = [144.201 ns; 149.956 ns] (CI 99.9%), Margin = 2.877 ns (1.96% of Mean)
Skewness = 0.01, Kurtosis = 2.79, MValue = 2
-------------------- Histogram --------------------
[137.316 ns ; 141.911 ns) | @@
[141.911 ns ; 145.562 ns) | @@@@@@
[145.562 ns ; 149.052 ns) | @@@@@@@@@@@@
[149.052 ns ; 152.635 ns) | @@@@
[152.635 ns ; 156.582 ns) | @@
---------------------------------------------------

MapperBenchmark.PrjRecord: Job-TXFNNH(OutlierMode=DontRemove, Runtime=.NET 6.0)
Runtime = .NET 6.0.0 (6.0.21.52210), X64 RyuJIT; GC = Concurrent Server
Mean = 17.079 ns, StdErr = 0.112 ns (0.65%), N = 19, StdDev = 0.488 ns
Min = 16.192 ns, Q1 = 16.684 ns, Median = 17.090 ns, Q3 = 17.406 ns, Max = 18.174 ns
IQR = 0.722 ns, LowerFence = 15.602 ns, UpperFence = 18.488 ns
ConfidenceInterval = [16.640 ns; 17.517 ns] (CI 99.9%), Margin = 0.439 ns (2.57% of Mean)
Skewness = 0.17, Kurtosis = 2.43, MValue = 2
-------------------- Histogram --------------------
[15.952 ns ; 16.398 ns) | @
[16.398 ns ; 16.877 ns) | @@@@@@
[16.877 ns ; 17.493 ns) | @@@@@@@@@
[17.493 ns ; 17.934 ns) | @@
[17.934 ns ; 18.414 ns) | @
---------------------------------------------------

MapperBenchmark.MpdAutoMapper: Job-GWFPFL(OutlierMode=DontRemove, Runtime=.NET 5.0)
Runtime = .NET 5.0.3 (5.0.321.7212), X64 RyuJIT; GC = Concurrent Server
Mean = 129.341 ns, StdErr = 0.756 ns (0.58%), N = 41, StdDev = 4.839 ns
Min = 123.053 ns, Q1 = 126.282 ns, Median = 128.399 ns, Q3 = 131.726 ns, Max = 149.675 ns
IQR = 5.444 ns, LowerFence = 118.116 ns, UpperFence = 139.892 ns
ConfidenceInterval = [126.658 ns; 132.025 ns] (CI 99.9%), Margin = 2.683 ns (2.07% of Mean)
Skewness = 1.77, Kurtosis = 8.21, MValue = 2
-------------------- Histogram --------------------
[121.212 ns ; 125.576 ns) | @@@@@@@
[125.576 ns ; 129.260 ns) | @@@@@@@@@@@@@@@@@
[129.260 ns ; 133.666 ns) | @@@@@@@@@@@@
[133.666 ns ; 137.207 ns) | @@@@
[137.207 ns ; 140.891 ns) | 
[140.891 ns ; 144.574 ns) | 
[144.574 ns ; 147.833 ns) | 
[147.833 ns ; 151.517 ns) | @
---------------------------------------------------

MapperBenchmark.MpdRecord: Job-GWFPFL(OutlierMode=DontRemove, Runtime=.NET 5.0)
Runtime = .NET 5.0.3 (5.0.321.7212), X64 RyuJIT; GC = Concurrent Server
Mean = 14.205 ns, StdErr = 0.248 ns (1.75%), N = 100, StdDev = 2.479 ns
Min = 12.391 ns, Q1 = 13.209 ns, Median = 13.495 ns, Q3 = 14.254 ns, Max = 34.603 ns
IQR = 1.045 ns, LowerFence = 11.642 ns, UpperFence = 15.822 ns
ConfidenceInterval = [13.364 ns; 15.046 ns] (CI 99.9%), Margin = 0.841 ns (5.92% of Mean)
Skewness = 5.81, Kurtosis = 46.33, MValue = 2.13
-------------------- Histogram --------------------
[11.690 ns ; 12.666 ns) | @@@@@
[12.666 ns ; 14.068 ns) | @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
[14.068 ns ; 15.559 ns) | @@@@@@@@@@@@@@
[15.559 ns ; 16.961 ns) | @@@@@@@
[16.961 ns ; 18.572 ns) | @@@@@
[18.572 ns ; 20.044 ns) | @
[20.044 ns ; 21.446 ns) | 
[21.446 ns ; 22.849 ns) | 
[22.849 ns ; 24.251 ns) | 
[24.251 ns ; 25.653 ns) | 
[25.653 ns ; 27.055 ns) | 
[27.055 ns ; 28.457 ns) | 
[28.457 ns ; 29.859 ns) | 
[29.859 ns ; 31.261 ns) | 
[31.261 ns ; 32.663 ns) | 
[32.663 ns ; 33.902 ns) | 
[33.902 ns ; 35.305 ns) | @
---------------------------------------------------

MapperBenchmark.MpdAutoMapper: Job-TXFNNH(OutlierMode=DontRemove, Runtime=.NET 6.0)
Runtime = .NET 6.0.0 (6.0.21.52210), X64 RyuJIT; GC = Concurrent Server
Mean = 135.102 ns, StdErr = 0.730 ns (0.54%), N = 28, StdDev = 3.865 ns
Min = 129.171 ns, Q1 = 132.010 ns, Median = 134.549 ns, Q3 = 137.912 ns, Max = 144.231 ns
IQR = 5.902 ns, LowerFence = 123.157 ns, UpperFence = 146.765 ns
ConfidenceInterval = [132.408 ns; 137.797 ns] (CI 99.9%), Margin = 2.695 ns (1.99% of Mean)
Skewness = 0.57, Kurtosis = 2.62, MValue = 2
-------------------- Histogram --------------------
[127.500 ns ; 131.334 ns) | @@@@
[131.334 ns ; 134.675 ns) | @@@@@@@@@@@
[134.675 ns ; 138.680 ns) | @@@@@@@@
[138.680 ns ; 142.125 ns) | @@@
[142.125 ns ; 145.902 ns) | @@
---------------------------------------------------

MapperBenchmark.MpdRecord: Job-TXFNNH(OutlierMode=DontRemove, Runtime=.NET 6.0)
Runtime = .NET 6.0.0 (6.0.21.52210), X64 RyuJIT; GC = Concurrent Server
Mean = 12.760 ns, StdErr = 0.097 ns (0.76%), N = 35, StdDev = 0.576 ns
Min = 11.662 ns, Q1 = 12.419 ns, Median = 12.772 ns, Q3 = 13.063 ns, Max = 14.608 ns
IQR = 0.644 ns, LowerFence = 11.453 ns, UpperFence = 14.029 ns
ConfidenceInterval = [12.410 ns; 13.111 ns] (CI 99.9%), Margin = 0.350 ns (2.75% of Mean)
Skewness = 0.55, Kurtosis = 4.28, MValue = 2
-------------------- Histogram --------------------
[11.516 ns ; 12.046 ns) | @@@
[12.046 ns ; 12.508 ns) | @@@@@@@@@
[12.508 ns ; 13.156 ns) | @@@@@@@@@@@@@@@@@@
[13.156 ns ; 13.627 ns) | @@@@
[13.627 ns ; 14.089 ns) | 
[14.089 ns ; 14.377 ns) | 
[14.377 ns ; 14.839 ns) | @
---------------------------------------------------

// * Hints *
Outliers
  MapperBenchmark.PrjAutoMapper: OutlierMode=DontRemove, Runtime=.NET 5.0 -> 4 outliers were detected (153.51 ns..158.77 ns)
  MapperBenchmark.PrjRecord: OutlierMode=DontRemove, Runtime=.NET 5.0     -> 1 outlier  was  detected (28.13 ns)
  MapperBenchmark.MpdAutoMapper: OutlierMode=DontRemove, Runtime=.NET 5.0 -> 1 outlier  was  detected (156.94 ns)
  MapperBenchmark.MpdRecord: OutlierMode=DontRemove, Runtime=.NET 5.0     -> 12 outliers were detected (20.78 ns..39.55 ns)
  MapperBenchmark.MpdRecord: OutlierMode=DontRemove, Runtime=.NET 6.0     -> 1 outlier  was  detected (19.59 ns)


// * Legends *
  Categories : All categories of the corresponded method, class, and assembly
  Mean       : Arithmetic mean of all measurements
  Error      : Half of 99.9% confidence interval
  StdDev     : Standard deviation of all measurements
  Median     : Value separating the higher half of all measurements (50th percentile)
  P0         : Percentile 0
  P25        : Percentile 25
  P50        : Percentile 50
  P67        : Percentile 67
  P80        : Percentile 80
  P85        : Percentile 85
  P90        : Percentile 90
  P95        : Percentile 95
  P100       : Percentile 100
  1 ns       : 1 Nanosecond (0.000000001 sec)

// ***** BenchmarkRunner: End *****
// ** Remained 0 benchmark(s) to run **
Run time: 00:05:03 (303.8 sec), executed benchmarks: 8

Global total time: 00:05:14 (314.56 sec), executed benchmarks: 8
// * Artifacts cleanup *

```
