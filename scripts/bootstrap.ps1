thirdparty\nuget\NuGet.exe install src\SupaCharge.Nuget.Packages\common\packages.config -OutputDirectory packages\common -ExcludeVersion
thirdparty\nuget\NuGet.exe install src\SupaCharge.Nuget.Packages\net-3.5\packages.config -OutputDirectory packages\net-3.5 -ExcludeVersion
thirdparty\nuget\NuGet.exe install src\SupaCharge.Nuget.Packages\net-4.0\packages.config -OutputDirectory packages\net-4.0 -ExcludeVersion

$env:PATH += ";.\packages\common\NAnt.Portable\;.\packages\common\NUnit.Runners\tools"

nant Clean Build.And.Test.All.Versions