
import-module .\tools\psake-nuget.psm1

Install-Packages ".\src" ".\src\packages"

remove-module psake-nuget