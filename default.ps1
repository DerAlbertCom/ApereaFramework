Properties {
    $project_dir = Split-Path $psake.build_script_file
    $src_dir = "$project_dir\src"
    $out_dir = "$project_dir\out"
    
    $nuget_path = "$project_dir\src\.nuget"
    $nuspec_dir = "$project_dir\nuspecs"
    $nupgk_dir = "$project_dir\nupkg"
    $version_file = "$src_dir\FrameworkVersion.cs"
}

Task Default -Depends Build

Task ExtendPath {
    $path = get-item -path "ENV:PATH"
    $path = $path.value + ";$$nuget_path"
    set-item -force -path "ENV:PATH"  -value "$path" 
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
    Set-PackageVersion "$nuspec_dir\Aperea.Identity.nuspec" $version @{"Aperea.Bootstrap"=$depVersion}


}

Task CreateNuGet -Depends Build, SetPackageVersion  {
    Write-Host "Creating NuGet-Packages" -ForegroundColor Green   
	md $nupgk_dir -force   

    Exec { nuget pack "$nuspec_dir\Aperea.Bootstrap.nuspec" /OutputDirectory "$nupgk_dir\" }    
    Exec { nuget pack "$nuspec_dir\Aperea.Bootstrap.Mvc.nuspec" /OutputDirectory "$nupgk_dir\" }    
    Exec { nuget pack "$nuspec_dir\Aperea.Identity.nuspec" /OutputDirectory "$nupgk_dir\" }    
}


Task Release -Depends CreateNuGet  {
}

Task NuGetPush -Depends Release, BumpRevision  {
    $version = Get-AssemblyInfoVersion $version_file
    
    Write-Host "Creating NuGet-Packages" -ForegroundColor Green   
    Exec { nuget push "$nupgk_dir\Aperea.Bootstrap.$version.nupkg" }    
    Exec { nuget push "$nupgk_dir\Aperea.Bootstrap.Mvc.$version.nupkg" }    
    Exec { nuget push "$nupgk_dir\Aperea.Identity.$version.nupkg" }    
}


