﻿

function Get-AssemblyInfoVersion {
    param(
        [Parameter(Position=0, Mandatory=1)]
        [string]$file = @()
    )
    Process {
        $line = Get-Content $file | where-object { $_.Contains("AssemblyVersion") }
        New-Object Version($line.Split('"')[1])
    }
}


function Set-AssemblyInfoVersion {
    param(
        [Parameter(Position=0, Mandatory=1)]
        [string]$file = @(),
        [Parameter(Position=1, Mandatory=1)]
        [string]$version = "0.0.0.1",
        [Parameter(Position=2, Mandatory=0)]
        [string]$fileVersion = "0.0.0.0"
    )
    Process {
        if ($fileVersion -eq "0.0.0.0") {
            $fileVersion = $version       
        }
        $lines= get-content $file
        $output = New-Item -type file "$file" -force
        foreach ($line in $lines)
        {
           if ($line.Contains("AssemblyFileVersion")) {
            Add-Content $output "[assembly: AssemblyFileVersion(""$fileVersion"")]"
           }
           elseif ($line.Contains("AssemblyVersion")) 
           {
            Add-Content $output "[assembly: AssemblyVersion(""$version"")]"
           } else 
           {
            Add-Content $output $line
           }           
        }
    }
}

function Get-PackageVersion {
    param(
        [Parameter(Position=0, Mandatory=1)]
        [string]$nuspecFile = @()
    )
    
    Process 
    {
        [XML]$specs = get-content $nuspecFile
        $specs.package.metadata.version
    }
}

function Set-PackageVersion {
  param(
    [Parameter(Position=0,Mandatory=1)]
    [string]$nuspecFile = @(),
    [Parameter(Position=1,Mandatory=1)]
    [string]$version = @(),
    [Parameter(Position=2,Mandatory=0)]
    [System.Collections.Hashtable]$dependencyList = @{}
   )
   
   Process
   {
        Write-Host "Set-PackageVersion on $nuspecFile" -ForegroundColor Yellow
        [XML]$specs = get-content $nuspecFile
        $specs.package.metadata.version = $version
        foreach ($key in $dependencyList.Keys) 
        {
            $depVersion = $dependencyList[$key]
            $dependency = $specs.package.metadata.dependencies.dependency | where-object { $_.id -eq $key }            
            $dependency.version = $depVersion
        }
        $specs.Save($nuspecFile)
   }
}

function BumpRevision {
    param(
        [Parameter(Position=0, Mandatory=1)]
        [string]$file = @()
    )    
    Process {
        $version = Get-AssemblyInfoVersion $file
        $version = New-Object Version($version.Major,  $version.Minor,  $version.Build, ($version.Revision+1))
        Set-AssemblyInfoVersion $file $version
    }
}

function BumpBuildVersion {
    param(
        [Parameter(Position=0, Mandatory=1)]
        [string]$file = @()
    )    
    Process {
        $version = Get-AssemblyInfoVersion $file
        $version = New-Object Version($version.Major,  $version.Minor,  ($version.Build+1), $version.Revision)
        Set-AssemblyInfoVersion $file $version
    }
}

function BumpMinorVersion {
    param(
        [Parameter(Position=0, Mandatory=1)]
        [string]$file = @()
    )    
    Process {
        $version = Get-AssemblyInfoVersion $file
        $version = New-Object Version($version.Major,  ($version.Minor+1),  0, $version.Revision)
        Set-AssemblyInfoVersion $file $version
    }
}

function BumpMajorVersion {
    param(
        [Parameter(Position=0, Mandatory=1)]
        [string]$file = @()
    )    
    Process {
        $version = Get-AssemblyInfoVersion $file
        $version = New-Object Version(($version.Major+1),  0,  0, $version.Revision)
        Set-AssemblyInfoVersion $file $version
    }
}

Export-ModuleMember "Set-AssemblyInfoVersion", "Get-AssemblyInfoVersion", "Get-PackageVersion", "Set-PackageVersion", "BumpRevision", "BumpBuildVersion", "BumpMinorVersion", "BumpMajorVersion"
