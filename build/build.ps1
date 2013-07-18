$ErrorActionPreference = 'Stop'

function CheckLastExitCode() {
  if ($? -eq $false) {
    throw 'build failed'
  }
}

powershell .\build\clean.ps1

C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe .\build\SupaCharge.Core.proj /p:FrameworkVersion=v3.5
CheckLastExitCode
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe .\build\SupaCharge.Testing.proj /p:FrameworkVersion=v3.5
CheckLastExitCode
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe .\build\SupaCharge.UnitTests.proj /p:FrameworkVersion=v3.5
CheckLastExitCode

C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe .\build\SupaCharge.Core.proj /p:FrameworkVersion=v4.0
CheckLastExitCode
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe .\build\SupaCharge.Testing.proj /p:FrameworkVersion=v4.0
CheckLastExitCode
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe .\build\SupaCharge.UnitTests.proj /p:FrameworkVersion=v4.0
CheckLastExitCode