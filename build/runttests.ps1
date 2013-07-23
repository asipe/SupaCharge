$ErrorActionPreference = 'Stop'

function CheckLastExitCode() {
  if ($? -eq $false) {
    throw 'build failed'
  }
}


Write-Host ''
Write-Host '------------net-3.5------------' -ForegroundColor DarkYellow
.\packages\common\NUnit.Runners\tools\nunit-console.exe debug\net-3.5\supacharge.unittests\supacharge.unittests.dll /nologo | Write-Host
CheckLastExitCode
Write-Host '-------------------------------' -ForegroundColor DarkYellow
Write-Host ''

Write-Host ''
Write-Host '------------net-4.0------------' -ForegroundColor DarkYellow
.\packages\common\NUnit.Runners\tools\nunit-console.exe debug\net-4.0\supacharge.unittests\supacharge.unittests.dll /nologo | Write-Host
CheckLastExitCode
Write-Host '-------------------------------' -ForegroundColor DarkYellow
Write-Host ''

Write-Host ''
Write-Host '------------net-4.5------------' -ForegroundColor DarkYellow
.\packages\common\NUnit.Runners\tools\nunit-console.exe debug\net-4.5\supacharge.unittests\supacharge.unittests.dll /nologo | Write-Host
CheckLastExitCode
Write-Host '-------------------------------' -ForegroundColor DarkYellow
Write-Host ''
