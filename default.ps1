Properties {
    $project_dir = Split-Path $psake.build_script_file
    $src_dir = "$project_dir\src"
    $out_dir = "$project_dir\out"
    
    $nuget = "$project_dir\tools\nuget.exe"
    $nuspec_dir = "$project_dir\nuspecs"
    $nupgk_dir = "$project_dir\nupkg"
    $version_file = "$src_dir\FrameworkVersion.cs"
}

Task Default -Depends Build

Task Clean {
    Write-Host "Clean all Builds" -ForegroundColor Green
	Exec { msbuild "$src_dir\ApereaFramework.All.sln" /t:Clean /v:quiet /p:Configuration=Release } 
	Exec { msbuild "$src_dir\ApereaFramework.All.sln" /t:Clean /v:quiet /p:Configuration=Debug } 
}


Task Build -Depends  Clean {
    Write-Host "ApereaFramework.All.sln" -ForegroundColor Green
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
    Write-Host "Building NgauGet-Packages" -ForegroundColor Green
    $version = Get-AssemblyInfoVersion $version_file
    $depVersion = "[$version]"
    Set-PackageVersion "$nuspec_dir\Aperea.Core.nuspec" $version
    Set-PackageVersion "$nuspec_dir\Aperea.Mail.nuspec" $version @{"Aperea.Core"=$depVersion}
    Set-PackageVersion "$nuspec_dir\Aperea.Membership.nuspec" $version @{"Aperea.Mail"=$depVersion}
    Set-PackageVersion "$nuspec_dir\Aperea.Mvc.nuspec" $version @{"Aperea.Core"=$depVersion}
    Set-PackageVersion "$nuspec_dir\Aperea.Mvc.Start.nuspec" $version @{"Aperea.Membership"="$version";"Aperea.MVC"="$version"}
}

Task NuGet -Depends ConvertStart, Build, SetPackageVersion  {
    Write-Host "Building NuGet-Packages" -ForegroundColor Green   
	md $nupgk_dir -force
    Exec { .\tools\nuget.exe pack "$nuspec_dir\Aperea.Core.nuspec" /OutputDirectory "$nupgk_dir\" }    
    Exec { .\tools\nuget.exe pack "$nuspec_dir\Aperea.Mail.nuspec" /OutputDirectory "$nupgk_dir\" }    
    Exec { .\tools\nuget.exe pack "$nuspec_dir\Aperea.Membership.nuspec" /OutputDirectory "$nupgk_dir\" }    
    Exec { .\tools\nuget.exe pack "$nuspec_dir\Aperea.Mvc.nuspec" /OutputDirectory "$nupgk_dir\" }    
    Exec { .\tools\nuget.exe pack "$nuspec_dir\Aperea.Mvc.Start.nuspec" /OutputDirectory "$nupgk_dir\" }    
}

Task Release -Depends BumpRevision, NuGet  {
}

Task ConvertStart {
    Write-Host "Converting MVC Project to NugetPackage" -ForegroundColor Green   
	ConvertMvcProject "$src_dir\Aperea.MVC.Start\Aperea.Mvc.Start.csproj" "$out_dir\_ProjectContent"
}
