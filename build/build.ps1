$ErrorActionPreference = 'Stop'

function CheckLastExitCode() {
  if ($? -eq $false) {
    throw 'build failed'
  }
}

powershell .\build\clean.ps1

C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe .\build\SupaCharge.proj /ds
CheckLastExitCode