
$solutionFolder  = ".\src"
$packagesFolder = Join-Path $solutionFolder "packages"
$configFilter = Join-Path $solutionFolder "**\packages.config"

$packages = Get-ChildItem $configFilter
foreach ($package in $packages) {
   .\tools\nuget install $package.Fullname  /OutputDirectory $packagesFolder
}