

function GetProjectFiles($projectDir) {
	get-childitem $projectDir -Recurse -include *.resx,*.cshtml,*.cs,*.transform,*.txt -exclude AssemblyInfo.cs,Global.asax.cs,*.Designer.cs
}

function GetRootNamespace($projectFile) {
	[XML]$xml = Get-Content $projectFile
	$xml.Project.PropertyGroup[0].RootNamespace
}

function DontChangeFileName($name)
{
	$name.EndsWith(".transform")
}

function Install-Packages {
	param (
		[Parameter(Position=0,Mandatory=1)]
		[string]$solutionFolder,
		[Parameter(Position=1,Mandatory=1)]
		[string]$packagesFolder
	)

	$packagesFolder = Join-Path $solutionFolder "packages"
	$configFilter = Join-Path $solutionFolder "**\packages.config"

	$packages = Get-ChildItem $configFilter
	foreach ($package in $packages) {
	   .\tools\nuget install $package.Fullname  /OutputDirectory $packagesFolder
	}
}

function ConvertMvcProject {
	param (
	[Parameter(Position=0,Mandatory=1)]
    [string]$projectFile,
    [Parameter(Position=1,Mandatory=1)]
    [string]$outDir,
    [Parameter(Position=2,Mandatory=0)]
    [string[]]$excludeFiles = @()
	)
	
	Process {
	    Write-Host "Converting $projectFile" -ForegroundColor Green

		$projectDir =  Split-path $projectFile
		$rootNamespace = GetRootNamespace($projectFile)
		if (Test-Path $outDir)
		{
			Remove-Item $outDir -Recurse -Force
		}
		$sourceFiles = GetProjectFiles $projectDir
		. mkdir $outDir -Force
		foreach ($file in $sourceFiles)
		{
			$name = $file.FullName.SubString($outDir.Length+2)
			$origDestName = Join-Path $outDir $name
			$destName = "$origDestName.pp"
			$folder = Split-path $destName
			if ((Test-Path $folder) -eq $False)
			{
				mkdir $folder -Force
			}
			if (DontChangeFileName $file.Name) {
				(Get-Content $file.Fullname) -replace "$rootNamespace","`$rootnamespace`$" | Out-File $origDestName
			} else {
				(Get-Content $file.Fullname) -replace "$rootNamespace","`$rootnamespace`$" | Out-File $destName
			}
		}
		$objDir = Join-Path $outDir "obj"
		if (Test-Path $objDir)
		{
			Remove-Item $objDir -Recurse -Force
		}
		
		
	}
}

Export-ModuleMember "ConvertMvcProject", "Install-Packages"