using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<Foo>();

[MemoryDiagnoser(false)]
[SimpleJob(RuntimeMoniker.Net60)]
[SimpleJob(RuntimeMoniker.Net70)]
public class Foo
{
    [Benchmark]
    public void InitSortedSetUsingConstroctor()
    {
        SortedSet<int> set = new(Enumerable.Range(0, 10000));
    }

    [Benchmark]
    public void InitSortedSetUsingAddMethod()
    {
        SortedSet<int> set = new();

        foreach(int i in Enumerable.Range(0, 10000))
            set.Add(i);
    }
}
 