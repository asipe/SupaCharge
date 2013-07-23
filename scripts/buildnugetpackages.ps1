$ErrorActionPreference = 'Stop'

function CheckLastExitCode() {
  if ($? -eq $false) {
    throw 'build failed'
  }
}

if (Test-Path('nugetworking')) {
  remove-item 'nugetworking' -Recurse -Verbose
}

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