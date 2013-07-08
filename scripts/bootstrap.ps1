thirdparty\nuget\NuGet.exe install src\SupaCharge.Nuget.Packages\common\packages.config -OutputDirectory packages\common
thirdparty\nuget\NuGet.exe install src\SupaCharge.Nuget.Packages\net-3.5\packages.config -OutputDirectory packages\net-3.5

$env:PATH += ";.\packages\common\NAnt.Portable.0.92\;.\packages\common\NUnit.Runners.2.6.1\tools"

nant Clean Build.All Run.Tests