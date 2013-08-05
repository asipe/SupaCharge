get-module -all | where {$_.Name -eq 'minion'} | foreach {
  write-host -ForegroundColor Yellow "removing module $_"
  remove-module $_
}

import-module '.\scripts\minion.psm1'
write-host -ForegroundColor Green "minion ready"