### publish nuget package 

#### github

1. Access Token 발급
  - token -> `ghp_umhUfyFmnOEd4lmjQZaLJkAomthEezwWQNcJ`
![token](./assets/token.png)

2. nuget source 추가
- `dotnet nuget add source --username {user_name} --password {token} --name {source_name} "https://nuget.pkg.github.com/{user_name}/index.json"`
  - e.g) `dotnet nuget add source --username chanos-dev --password ghp_umhUfyFmnOEd4lmjQZaLJkAomthEezwWQNcJ --name github "https://nuget.pkg.github.com/chanos-dev/index.json"`

3. csproj 내용 수정
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>netstandard2.0</TargetFramework>
	  <PackageId>CMath</PackageId> 
	  <Version>1.0.0</Version>
	  <Authors>chanos.dev</Authors> 	  
	  <RepositoryUrl>https://github.com/chanos-dev/.net-record</RepositoryUrl>
	  <PackageDescription>test github nuget package</PackageDescription> 
	  <PackageTags>math</PackageTags> 
	  <PackageIcon>icon.png</PackageIcon> 
	  <PackageIconUrl>https://cdn-icons-png.flaticon.com/128/897/897281.png</PackageIconUrl> 		
	  <PackageProjectUrl>https://github.com/chanos-dev/.net-record/tree/main/nuget/chanos.Math/chanos.Math</PackageProjectUrl> 
  </PropertyGroup>
  
  <ItemGroup> 
    <None Include="icon.png" Pack="true" PackagePath="\" /> 
  </ItemGroup>
</Project>

```

4. nuget package 생성
- 프로젝트 또는 솔루션 위치에서 
  - `dotnet pack -c Release`
- 경로 지정
  - `dotnet pakc .\chanos.Math\chanos.Math.csproj -c Release`
![package](./assets/package.png)

5. nuget push
- `dotnet nuget push {nupkg_path} --source {source_name}`
  - e.g) `dotnet nuget push .\bin\Release\CMath.1.0.0.nupkg --source github`

6. github package 탭 확인
![push](./assets/push.png)

---