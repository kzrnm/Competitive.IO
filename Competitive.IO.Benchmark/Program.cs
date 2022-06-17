#pragma warning disable IDE0005 // Using ディレクティブは必要ありません。
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.CsProj;
using ConsoleReader = Kzrnm.Competitive.IO.ConsoleReader;
using NewConsoleReader = Kzrnm.Competitive.IO.Benchmark.ConsoleReader;
#pragma warning restore IDE0005 // Using ディレクティブは必要ありません。

#pragma warning disable CS0436
#if DEBUG
BenchmarkSwitcher.FromAssembly(typeof(BenchmarkConfig).Assembly).Run(args, new DebugInProcessConfig());
#else
_ = BenchmarkRunner.Run(typeof(Benchmark));
//_ = BenchmarkRunner.Run(typeof(Benchmark).Assembly);
#endif

public class BenchmarkConfig : ManualConfig
{
    public BenchmarkConfig()
    {
        //AddDiagnoser(MemoryDiagnoser.Default);
        AddExporter(BenchmarkDotNet.Exporters.MarkdownExporter.GitHub);
        //AddJob(Job.ShortRun.WithToolchain(CsProjCoreToolchain.NetCoreApp60));
        AddJob(Job.ShortRun.WithToolchain(CsProjCoreToolchain.NetCoreApp31));
        //AddJob(Job.ShortRun.WithToolchain(CsProjClassicNetToolchain.Net472));
    }
}

[Config(typeof(BenchmarkConfig))]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
public class Benchmark
{
    readonly Assembly assembly = Assembly.GetExecutingAssembly();

    private Stream AsciiStream() => assembly.GetManifestResourceStream("Competitive.IO.Benchmark.Ascii.txt");
    private Stream IntStream() => assembly.GetManifestResourceStream("Competitive.IO.Benchmark.Int.txt");
    private Stream DoubleStream() => assembly.GetManifestResourceStream("Competitive.IO.Benchmark.Double.txt");


    [Benchmark]
    [BenchmarkCategory("Current")]
    public long ReadInt()
    {
        var cr = new ConsoleReader(IntStream(), new UTF8Encoding(false));
        long sum = 0;
        int N = cr.Int();
        for (int i = 0; i < N; i++)
        {
            sum += cr.Int();
            sum += cr.Int();
            sum += cr.Int();
        }
        return sum;
    }
    [Benchmark]
    [BenchmarkCategory("Current")]
    public double ReadDouble()
    {
        var cr = new ConsoleReader(DoubleStream(), new UTF8Encoding(false));
        double sum = 0;
        int N = cr.Int();
        for (int i = 0; i < N; i++)
        {
            sum += cr.Double();
            sum += cr.Double();
            sum += cr.Double();
        }
        return sum;
    }
    [Benchmark]
    [BenchmarkCategory("Current")]
    public double ReadAscii()
    {
        var cr = new ConsoleReader(AsciiStream(), new UTF8Encoding(false));
        int hash = 0;
        int N = cr.Int();
        for (int i = 0; i < N; i++)
        {
            hash ^= cr.Ascii().GetHashCode();
            hash ^= cr.Ascii().GetHashCode();
            hash ^= cr.Ascii().GetHashCode();
        }
        return hash;
    }


    [Benchmark]
    [BenchmarkCategory("New")]
    public long NewReadInt()
    {
        var cr = new NewConsoleReader(IntStream(), new UTF8Encoding(false));
        long sum = 0;
        int N = cr.Int();
        for (int i = 0; i < N; i++)
        {
            sum += cr.Int();
            sum += cr.Int();
            sum += cr.Int();
        }
        return sum;
    }
    [Benchmark]
    [BenchmarkCategory("New")]
    public double NewReadDouble()
    {
        var cr = new NewConsoleReader(DoubleStream(), new UTF8Encoding(false));
        double sum = 0;
        int N = cr.Int();
        for (int i = 0; i < N; i++)
        {
            sum += cr.Double();
            sum += cr.Double();
            sum += cr.Double();
        }
        return sum;
    }
    [Benchmark]
    [BenchmarkCategory("New")]
    public double NewReadAscii()
    {
        var cr = new NewConsoleReader(AsciiStream(), new UTF8Encoding(false));
        int hash = 0;
        int N = cr.Int();
        for (int i = 0; i < N; i++)
        {
            hash ^= cr.Ascii().GetHashCode();
            hash ^= cr.Ascii().GetHashCode();
            hash ^= cr.Ascii().GetHashCode();
        }
        return hash;
    }
}



//[Config(typeof(BenchmarkConfig))]
//[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
//public class BenchmarkRepeat
//{
//    readonly Assembly assembly = Assembly.GetExecutingAssembly();

//    private Stream AsciiStream() => assembly.GetManifestResourceStream("Competitive.IO.Benchmark.Ascii.txt");
//    private Stream IntStream() => assembly.GetManifestResourceStream("Competitive.IO.Benchmark.Int.txt");
//    private Stream DoubleStream() => assembly.GetManifestResourceStream("Competitive.IO.Benchmark.Double.txt");

//    [Benchmark]
//    [BenchmarkCategory("Current")]
//    public long RepeatInt()
//    {
//        var cr = new ConsoleReader(IntStream(), new UTF8Encoding(false));
//        long sum = 0;
//        int N = cr.Int();
//        foreach (var s in cr.Repeat(3 * N).Int())
//        {
//            sum += sum;
//        }
//        return sum;
//    }
//    [Benchmark]
//    [BenchmarkCategory("Current")]
//    public double RepeatDouble()
//    {
//        var cr = new ConsoleReader(DoubleStream(), new UTF8Encoding(false));
//        double sum = 0;
//        int N = cr.Int();
//        foreach (var s in cr.Repeat(3 * N).Double())
//        {
//            sum += sum;
//        }
//        return sum;
//    }
//    [Benchmark]
//    [BenchmarkCategory("Current")]
//    public int RepeatAscii()
//    {
//        var cr = new ConsoleReader(AsciiStream(), new UTF8Encoding(false));
//        int hash = 0;
//        int N = cr.Int();
//        foreach (var s in cr.Repeat(3 * N).Ascii())
//        {
//            hash ^= s.GetHashCode();
//        }
//        return hash;
//    }
//}
