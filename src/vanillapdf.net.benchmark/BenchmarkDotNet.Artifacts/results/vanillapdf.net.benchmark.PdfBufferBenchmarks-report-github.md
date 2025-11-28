```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.7171)
Intel Core i7-10700KF CPU 3.80GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK 10.0.100
  [Host]   : .NET 8.0.22 (8.0.2225.52707), X64 RyuJIT AVX2
  ShortRun : .NET 8.0.22 (8.0.2225.52707), X64 RyuJIT AVX2

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method               | Mean        | Error     | StdDev    | Gen0   | Allocated |
|--------------------- |------------:|----------:|----------:|-------:|----------:|
| GetStringData_Small  |    43.66 ns |  31.18 ns |  1.709 ns | 0.0268 |     224 B |
| GetStringData_Medium | 1,015.80 ns | 952.06 ns | 52.186 ns | 2.3861 |   20024 B |
