$ErrorActionPreference = 'Stop'

if (Test-Path('debug')) {
  remove-item 'debug' -Recurse -Verbose
}
