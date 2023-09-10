### [.NET Benchmark](https://benchmarkdotnet.org/articles/guides/getting-started.html)

```csharp
# ȯ�� ����
[SimpleJob(RuntimeMoniker.Net60)]
[SimpleJob(RuntimeMoniker.Net70)]
public class Boo
{
	...
}
```

```csharp
# GC �� �޸� ���� ���
//[MemoryDiagnoser(false)] // GC ������ ������� ����
[MemoryDiagnoser]
public class Boo
{
	...
}
```

```csharp
# �޼��� Benchmark ����
public class Boo
{
	[Benchmark]
    public void InitSortedSetUsingConstroctor()
    {
        SortedSet<int> set = new(Enumerable.Range(0, 10000));
    }
}
```

```csharp
# Benchmark Attribute�� ���� Ŭ���� ����
BenchmarkRunner.Run<Foo>();
BenchmarkRunner.Run(typeof(Foo));
```

```csharp
# ����
dotnet run -c release
```

```csharp
# Good Practices
- Debug ��尡 �ƴ� Release ��忡�� ����Ǿ�� �Ѵ�.
	- Debug ����� �޼���� 10~100�� ���� �� �ִ�.	
- �پ��� ȯ��(.NET)���� �׽�Ʈ�� �����ؾ� �Ѵ�.	
- ���� �ڵ带 ���ؾ� �Ѵ�.
	void Foo()
	{
		Math.Exp(1);
	}
	- JIT�� ���� �ڵ尡 ���� �� �� �ִ�.
	double Foo()
	{
		return Math.Exp(1);
	}
```