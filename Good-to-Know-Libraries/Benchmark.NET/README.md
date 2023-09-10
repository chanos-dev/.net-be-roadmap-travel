### [.NET Benchmark](https://benchmarkdotnet.org/articles/guides/getting-started.html)

```csharp
# 환경 지정
[SimpleJob(RuntimeMoniker.Net60)]
[SimpleJob(RuntimeMoniker.Net70)]
public class Boo
{
	...
}
```

```csharp
# GC 및 메모리 정보 출력
//[MemoryDiagnoser(false)] // GC 정보를 출력하지 않음
[MemoryDiagnoser]
public class Boo
{
	...
}
```

```csharp
# 메서드 Benchmark 지정
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
# Benchmark Attribute가 속한 클래스 실행
BenchmarkRunner.Run<Foo>();
BenchmarkRunner.Run(typeof(Foo));
```

```csharp
# 실행
dotnet run -c release
```

```csharp
# Good Practices
- Debug 모드가 아닌 Release 모드에서 실행되어야 한다.
	- Debug 모드의 메서드는 10~100배 느릴 수 있다.	
- 다양한 환경(.NET)에서 테스트를 진행해야 한다.	
- 데드 코드를 피해야 한다.
	void Foo()
	{
		Math.Exp(1);
	}
	- JIT에 의해 코드가 제거 될 수 있다.
	double Foo()
	{
		return Math.Exp(1);
	}
```