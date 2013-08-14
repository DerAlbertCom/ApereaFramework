Properties {
    $project_dir = Split-Path $psake.build_script_file
    $src_dir = "$project_dir\src"
    $out_dir = "$project_dir\out"
    
    $nuget_path = "$src_dir\.nuget"
    $nuspec_dir = "$project_dir\nuspecs"
    $nupgk_dir = "$project_dir\nupkg"
    $version_file = "$src_dir\FrameworkVersion.cs"
    $old_path = ""
}

Task Default -Depends Build

Task ExtendPath {
    $old_path = get-item -path "ENV:PATH"
    if (-not $old_path.value.Contains($nuget_path))
    {
        $path = "$nuget_path;" + $old_path.value;
        set-item -force -path "ENV:PATH"  -value "$path"         
    }
}
Task Clean -Depends ExtendPath {
    Write-Host "Clean all Builds" -ForegroundColor Green
    Exec { msbuild "$src_dir\Aperea.Bootstrap.sln" /t:Clean /v:quiet /p:Configuration=Release } 
    Exec { msbuild "$src_dir\Aperea.Bootstrap.sln" /t:Clean /v:quiet /p:Configuration=Debug } 
}


Task Build -Depends  Clean {
    Write-Host "Building ApereaFramework.sln" -ForegroundColor Green	
	Install-Packages $src_dir "$src_dir\packages"
    Exec { msbuild "$src_dir\Aperea.Bootstrap.sln" /t:Build /v:quiet /p:Configuration=Release /p:OutDir="$out_dir\" } 
}

Task BumpRevision {
    BumpRevision $version_file
}


Task BumpBuildVersion {
    BumpBuildVersion $version_file
}

Task BumpMinorVersion {
    BumpMinorVersion $version_file
}

Task BumpMajorVersion {
    BumpMajorVersion $version_file
}

Task SetPackageVersion {
    $version = Get-AssemblyInfoVersion $version_file
	
    Write-Host "Updating NuGet-Packages Version to $version" -ForegroundColor Green

    $depVersion = "[$version]"

    Set-PackageVersion "$nuspec_dir\Aperea.Bootstrap.nuspec" $version
    Set-PackageVersion "$nuspec_dir\Aperea.Bootstrap.Mvc.nuspec" $version @{"Aperea.Bootstrap"=$depVersion}
    Set-PackageVersion "$nuspec_dir\Aperea.Bootstrap.WebApi.nuspec" $version @{"Aperea.Bootstrap"=$depVersion}
    Set-PackageVersion "$nuspec_dir\Aperea.Identity.nuspec" $version @{"Aperea.Bootstrap"=$depVersion}
    Set-PackageVersion "$nuspec_dir\Aperea.Common.nuspec" $version @{"Aperea.Bootstrap"=$depVersion}
    Set-PackageVersion "$nuspec_dir\Aperea.Data.EntityFramework.nuspec" $version @{"Aperea.Bootstrap"=$depVersion}
}

Task CreateNuGet -Depends Build, SetPackageVersion  {
    Write-Host "Creating NuGet-Packages" -ForegroundColor Green   
	md $nupgk_dir -force   

    Exec { nuget.exe pack "$nuspec_dir\Aperea.Bootstrap.nuspec" /OutputDirectory "$nupgk_dir\" }    
    Exec { nuget.exe pack "$nuspec_dir\Aperea.Bootstrap.Mvc.nuspec" /OutputDirectory "$nupgk_dir\" }    
    Exec { nuget.exe pack "$nuspec_dir\Aperea.Bootstrap.WebApi.nuspec" /OutputDirectory "$nupgk_dir\" }    
    Exec { nuget.exe pack "$nuspec_dir\Aperea.Identity.nuspec" /OutputDirectory "$nupgk_dir\" }    
    Exec { nuget.exe pack "$nuspec_dir\Aperea.Common.nuspec" /OutputDirectory "$nupgk_dir\" }    
    Exec { nuget.exe pack "$nuspec_dir\Aperea.Data.EntityFramework.nuspec" /OutputDirectory "$nupgk_dir\" }    
}


Task Release -Depends CreateNuGet  {
}

Task PushIt -Depends Release  {
    $version = Get-AssemblyInfoVersion $version_file

    Write-Host "Pushing NuGet-Packages" -ForegroundColor Green   
    Exec { nuget.exe push "$nupgk_dir\Aperea.Bootstrap.$version.nupkg" }    
    Exec { nuget.exe push "$nupgk_dir\Aperea.Bootstrap.Mvc.$version.nupkg" }    
    Exec { nuget.exe push "$nupgk_dir\Aperea.Bootstrap.WebApi.$version.nupkg" }    
    Exec { nuget.exe push "$nupgk_dir\Aperea.Common.$version.nupkg" }  
    Exec { nuget.exe push "$nupgk_dir\Aperea.Data.EntityFramework.$version.nupkg" }  
}

Task NuGetPush -Depends  PushIt, BumpRevision  {
    $version = Get-AssemblyInfoVersion $version_file    
}


