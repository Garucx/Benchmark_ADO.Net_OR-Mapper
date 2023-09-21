``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 11 (10.0.22621.1555/22H2/2022Update/SunValley2)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=7.0.203
  [Host]     : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2


```
|            Method | Mean | Error | Rank |
|------------------ |-----:|------:|-----:|
| AdoConnectionOpen |   NA |    NA |    ? |

Benchmarks with issues:
  OpenConnection.AdoConnectionOpen: DefaultJob
