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

#    <delete dir="${nuget.working.dir}" verbose="${verbose}" />
#    <copy todir="${nuget.working.dir}\core\lib\net35" file="${debug.dir}\net-3.5\SupaCharge.Core.dll" verbose="${verbose}"/>
#    <copy todir="${nuget.working.dir}\core\lib\net40" file="${debug.dir}\net-4.0\SupaCharge.Core.dll" verbose="${verbose}"/>
#    <copy todir="${nuget.working.dir}\core" file="${nuget.spec.dir}\SupaCharge.Core.dll.nuspec" verbose="${verbose}"/>
#
#    <exec program="thirdparty\nuget\nuget.exe"
#          verbose="${verbose}"
#          commandline="pack ${nuget.working.dir}\core\SupaCharge.Core.dll.nuspec -OutputDirectory ${nuget.working.dir}\core"
#          workingdir="${root.dir}"
#          failonerror="true"/>
#
#    <copy todir="${nuget.working.dir}\testing\lib\net35" file="${debug.dir}\net-3.5\SupaCharge.Testing.dll" verbose="${verbose}"/>
#    <copy todir="${nuget.working.dir}\testing\lib\net40" file="${debug.dir}\net-4.0\SupaCharge.Testing.dll" verbose="${verbose}"/>
#    <copy todir="${nuget.working.dir}\testing" file="${nuget.spec.dir}\SupaCharge.Testing.dll.nuspec" verbose="${verbose}"/>
#
#    <exec program="thirdparty\nuget\nuget.exe"
#          verbose="${verbose}"
#          commandline="pack ${nuget.working.dir}\testing\SupaCharge.Testing.dll.nuspec -OutputDirectory ${nuget.working.dir}\testing"
#          workingdir="${root.dir}"
#          failonerror="true"/>