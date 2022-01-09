
BenchmarkDotNet=v0.13.1, OS=macOS Monterey 12.0.1 (21A559) [Darwin 21.1.0]
Intel Core i5-1030NG7 CPU 1.10GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100
  [Host]     : .NET 5.0.3 (5.0.321.7212), X64 RyuJIT
  Job-GWFPFL : .NET 5.0.3 (5.0.321.7212), X64 RyuJIT
  Job-TXFNNH : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT

OutlierMode=DontRemove  

        Method |        Job |  Runtime | Categories |      Mean |    Error |   StdDev |    Median |        P0 |       P25 |       P50 |       P67 |       P80 |       P85 |       P90 |       P95 |      P100 |
-------------- |----------- |--------- |----------- |----------:|---------:|---------:|----------:|----------:|----------:|----------:|----------:|----------:|----------:|----------:|----------:|----------:|
 PrjAutoMapper | Job-GWFPFL | .NET 5.0 |        PRJ | 144.78 ns | 2.835 ns | 5.040 ns | 144.30 ns | 136.37 ns | 141.86 ns | 144.30 ns | 144.97 ns | 147.06 ns | 149.22 ns | 150.31 ns | 156.85 ns | 158.77 ns |
     PrjRecord | Job-GWFPFL | .NET 5.0 |        PRJ |  21.56 ns | 0.516 ns | 0.614 ns |  21.30 ns |  20.88 ns |  21.15 ns |  21.30 ns |  21.65 ns |  21.85 ns |  22.21 ns |  22.30 ns |  22.37 ns |  23.42 ns |
 PrjAutoMapper | Job-TXFNNH | .NET 6.0 |        PRJ | 147.08 ns | 2.877 ns | 3.939 ns | 147.35 ns | 139.06 ns | 144.46 ns | 147.35 ns | 148.67 ns | 149.57 ns | 150.24 ns | 151.78 ns | 153.59 ns | 155.62 ns |
     PrjRecord | Job-TXFNNH | .NET 6.0 |        PRJ |  17.08 ns | 0.439 ns | 0.488 ns |  17.09 ns |  16.19 ns |  16.68 ns |  17.09 ns |  17.31 ns |  17.43 ns |  17.48 ns |  17.58 ns |  17.69 ns |  18.17 ns |
               |            |          |            |           |          |          |           |           |           |           |           |           |           |           |           |           |
 MpdAutoMapper | Job-GWFPFL | .NET 5.0 |        MPD | 129.34 ns | 2.683 ns | 4.839 ns | 128.40 ns | 123.05 ns | 126.28 ns | 128.40 ns | 130.70 ns | 132.46 ns | 133.41 ns | 134.80 ns | 135.91 ns | 149.67 ns |
     MpdRecord | Job-GWFPFL | .NET 5.0 |        MPD |  14.20 ns | 0.841 ns | 2.479 ns |  13.50 ns |  12.39 ns |  13.21 ns |  13.50 ns |  13.94 ns |  14.50 ns |  15.10 ns |  16.02 ns |  17.52 ns |  34.60 ns |
 MpdAutoMapper | Job-TXFNNH | .NET 6.0 |        MPD | 135.10 ns | 2.695 ns | 3.865 ns | 134.55 ns | 129.17 ns | 132.01 ns | 134.55 ns | 136.34 ns | 138.18 ns | 139.03 ns | 139.57 ns | 142.08 ns | 144.23 ns |
     MpdRecord | Job-TXFNNH | .NET 6.0 |        MPD |  12.76 ns | 0.350 ns | 0.576 ns |  12.77 ns |  11.66 ns |  12.42 ns |  12.77 ns |  13.01 ns |  13.13 ns |  13.15 ns |  13.35 ns |  13.49 ns |  14.61 ns |
