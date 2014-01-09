function Configure() {
  $root = '.'
  $src = Join-Path $root 'src'
  $thirdparty = Join-Path $root 'thirdparty'
  $debug = Join-Path $root 'debug'

  return @{
    thirdparty_dir = $thirdparty
    src_dir = $src
    packages_dir = Join-Path $thirdparty 'packages'
    debug_dir = $debug
  }
}

$config = Configure

function CheckLastExitCode() {
  if ($? -eq $false) {
    throw 'Command Failed'
  }
}

function TryDelete($path) {
  if (Test-Path($path)) {
    Remove-Item $path -Verbose -Recurse
  }
}

function Bootstrap() {
  .\thirdparty\nuget\nuget.exe install .\src\SupaCharge.Nuget.Packages\common\packages.config -OutputDirectory .\thirdparty\packages\common -ExcludeVersion | Write-Host
  .\thirdparty\nuget\nuget.exe install .\src\SupaCharge.Nuget.Packages\net-3.5\packages.config -OutputDirectory .\thirdparty\packages\net-3.5 -ExcludeVersion | Write-Host
  .\thirdparty\nuget\nuget.exe install .\src\SupaCharge.Nuget.Packages\net-4.0\packages.config -OutputDirectory .\thirdparty\packages\net-4.0 -ExcludeVersion | Write-Host
  .\thirdparty\nuget\nuget.exe install .\src\SupaCharge.Nuget.Packages\net-4.5\packages.config -OutputDirectory .\thirdparty\packages\net-4.5 -ExcludeVersion | Write-Host
}

function Clean() {
  TryDelete($config.debug_dir)
}

function CleanAll() {
  TryDelete($config.packages_dir)
}

function RunUnitTestsVS() {
  Write-Host -ForegroundColor Cyan '----------VS Unit Tests (3.5)-----------'
  .\thirdparty\packages\common\nunit.runners\tools\nunit-console.exe .\src\SupaCharge.UnitTests\bin\debug\SupaCharge.UnitTests.dll /nologo /framework:net-3.5 | Write-Host
  CheckLastExitCode
  Write-Host -ForegroundColor Cyan '----------------------------------'
}

function RunUnitTests() {
  Write-Host -ForegroundColor Cyan '-------Debug Unit Tests (3.5)-----------'
  .\thirdparty\packages\common\nunit.runners\tools\nunit-console.exe .\debug\net-3.5\SupaCharge.UnitTests\SupaCharge.UnitTests.dll /nologo /framework:net-3.5 | Write-Host
  CheckLastExitCode
  Write-Host -ForegroundColor Cyan '----------------------------------'

  Write-Host -ForegroundColor Cyan '-------Debug Unit Tests (4.0)-----------'
  .\thirdparty\packages\common\nunit.runners\tools\nunit-console.exe .\debug\net-4.0\SupaCharge.UnitTests\SupaCharge.UnitTests.dll /nologo /framework:net-4.0 | Write-Host
  CheckLastExitCode
  Write-Host -ForegroundColor Cyan '----------------------------------'

  Write-Host -ForegroundColor Cyan '-------Debug Unit Tests (4.5)-----------'
  .\thirdparty\packages\common\nunit.runners\tools\nunit-console.exe .\debug\net-4.5\SupaCharge.UnitTests\SupaCharge.UnitTests.dll /nologo /framework:net-4.5 | Write-Host
  CheckLastExitCode
  Write-Host -ForegroundColor Cyan '----------------------------------'
}

function RunAllTests() {
  RunUnitTests
  RunUnitTestsVS
}

function Build() {
  C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe .\src\SupaCharge.Build\SupaCharge.proj /ds /maxcpucount:6 | Write-Host
  CheckLastExitCode
}

function SetEnv() {
  $env:PATH += ";.\thirdparty\packages\common\NUnit.Runners\tools"
  Write-Host -ForegroundColor Green 'Path information set'
}

function Cycle() {
  Build
  RunUnitTests
}

function BuildNugetPackages() {
  tryDelete('nugetworking')
  
  New-Item .\nugetworking\core\lib\net35 -ItemType directory -Verbose
  New-Item .\nugetworking\core\lib\net40 -ItemType directory -Verbose
  New-Item .\nugetworking\core\lib\net45 -ItemType directory -Verbose
  
  New-Item .\nugetworking\testing\lib\net35 -ItemType directory -Verbose
  New-Item .\nugetworking\testing\lib\net40 -ItemType directory -Verbose
  New-Item .\nugetworking\testing\lib\net45 -ItemType directory -Verbose
  
  Copy-Item .\debug\net-3.5\supacharge.core\supacharge.core.dll .\nugetworking\core\lib\net35 -Verbose
  Copy-Item .\debug\net-4.0\supacharge.core\supacharge.core.dll .\nugetworking\core\lib\net40 -Verbose
  Copy-Item .\debug\net-4.5\supacharge.core\supacharge.core.dll .\nugetworking\core\lib\net45 -Verbose
  Copy-Item .\src\supacharge.nuget.specs\supacharge.core.dll.nuspec .\nugetworking\core -Verbose
  
  Copy-Item .\debug\net-3.5\supacharge.testing\supacharge.testing.dll .\nugetworking\testing\lib\net35 -Verbose
  Copy-Item .\debug\net-4.0\supacharge.testing\supacharge.testing.dll .\nugetworking\testing\lib\net40 -Verbose
  Copy-Item .\debug\net-4.5\supacharge.testing\supacharge.testing.dll .\nugetworking\testing\lib\net45 -Verbose
  Copy-Item .\src\supacharge.nuget.specs\supacharge.testing.dll.nuspec .\nugetworking\testing -Verbose
  
  thirdparty\nuget\nuget.exe pack .\nugetworking\core\supacharge.core.dll.nuspec -OutputDirectory .\nugetworking\core | Write-Host
  CheckLastExitCode
  
  thirdparty\nuget\nuget.exe pack .\nugetworking\testing\supacharge.testing.dll.nuspec -OutputDirectory .\nugetworking\testing | Write-Host
  CheckLastExitCode
}

function PushNugetPackages() {
  Write-Host -ForegroundColor Yellow '--------------------!!!!!!!------------------------'
  Write-Host -ForegroundColor Yellow 'Push Nuget Packages'
  Write-Host -ForegroundColor Yellow 'Are You Sure?  Enter YES to Continue'
  $response = Read-Host

  if ($response -eq 'YES') {
    Write-Host -ForegroundColor Yellow 'Pushing'

    thirdparty\nuget\nuget.exe push .\nugetworking\core\SupaCharge.Core.1.0.0.16.nupkg | Write-Host
    CheckLastExitCode

    thirdparty\nuget\nuget.exe push .\nugetworking\testing\SupaCharge.Testing.1.0.0.16.nupkg | Write-Host
    CheckLastExitCode
  } else {
    Write-Host -ForegroundColor Yellow 'Cancelled - Nothing Pushed'
  }
}

function Minion {
  param([string[]] $commands)

  $ErrorActionPreference = 'Stop'

  $commands | ForEach {
    $command = $_
    $times = Measure-Command {
      
      Write-Host -ForegroundColor Green "command: $command"
      Write-Host ''
      
      switch ($command) {
        'help' { Help }
        'bootstrap' { Bootstrap }
        'set.env' { SetEnv }
        'clean.all' { 
          Clean
          CleanAll 
        }
        'clean' { Clean }
        'run.unit.tests.vs' { RunUnitTestsVS }
        'run.unit.tests' { RunUnitTests }
        'run.all.tests' { RunAllTests }
        'build.all' { Build }
        'build.nuget.packages' { BuildNugetPackages }
        'cycle' { Cycle }
        'push.nuget.packages' { PushNugetPackages }
        default { Write-Host -ForegroundColor Red "command not known: $command" }
      }
    }
    
    Write-Host ''
    Write-Host -ForegroundColor Green "Command completed in $($times.TotalSeconds) seconds ($($times.TotalMilliseconds) millis)"
  }
}

export-modulemember -function Minion