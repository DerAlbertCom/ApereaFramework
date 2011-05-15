# Helper script for those who want to run
# psake without importing the module.
import-module .\tools\psake\psake.psm1
import-module .\tools\psake-version.psm1
import-module .\tools\psake-nuget.psm1

invoke-psake  -framework '4.0' @args 

remove-module psake-nuget
remove-module psake-version
remove-module psake