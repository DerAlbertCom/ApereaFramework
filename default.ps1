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
    Exec { msbuild "$src_dir\Aperea.Bootstrap.sln" /t:Clean /v:quiet /p:Configuration=Release } 
    Exec { msbuild "$src_dir\Aperea.Bootstrap.sln" /t:Clean /v:quiet /p:Configuration=Debug } 
}


Task Build -Depends  Clean {
    Write-Host "Building ApereaFramework.All.sln" -ForegroundColor Green	
	Install-Packages $src_dir "$src_dir\packages"
    Exec { msbuild "$src_dir\Aperea.Bootstrap.sln" /t:Build /v:quiet /p:Configuration=Release /p:OutDir="$out_dir\" } 
	Exec { msbuild "$src_dir\ApereaFramework.All.sln" /t:Build /v:quiet /p:Configuration=Release /p:OutDir="$out_dir\" } 
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
	
    Set-PackageVersion "$nuspec_dir\Aperea.Core.nuspec" $version
    Set-PackageVersion "$nuspec_dir\Aperea.Mail.nuspec" $version @{"Aperea.Core"=$depVersion}
    Set-PackageVersion "$nuspec_dir\Aperea.Authentication.nuspec" $version @{"Aperea.Mail"=$depVersion}
    Set-PackageVersion "$nuspec_dir\Aperea.MVC.nuspec" $version @{"Aperea.Core"=$depVersion}
    Set-PackageVersion "$nuspec_dir\Aperea.MVC.Authentication.nuspec" $version @{"Aperea.Authentication"=$depVersion;"Aperea.MVC"=$depVersion}
    Set-PackageVersion "$nuspec_dir\Aperea.Mvc.Start.nuspec" $version @{"Aperea.MVC.Authentication"="$version"}
}

Task NuGet -Depends ConvertStart, Build, SetPackageVersion  {
    Write-Host "Creating NuGet-Packages" -ForegroundColor Green   
	md $nupgk_dir -force   
    Exec { .\tools\nuget.exe pack "$nuspec_dir\Aperea.Bootstrap.nuspec" /OutputDirectory "$nupgk_dir\" }    
    Exec { .\tools\nuget.exe pack "$nuspec_dir\Aperea.Bootstrap.Mvc.nuspec" /OutputDirectory "$nupgk_dir\" }    
    Exec { .\tools\nuget.exe pack "$nuspec_dir\Aperea.Core.nuspec" /OutputDirectory "$nupgk_dir\" }    
    Exec { .\tools\nuget.exe pack "$nuspec_dir\Aperea.Mail.nuspec" /OutputDirectory "$nupgk_dir\" }    
    Exec { .\tools\nuget.exe pack "$nuspec_dir\Aperea.Authentication.nuspec" /OutputDirectory "$nupgk_dir\" }    
    Exec { .\tools\nuget.exe pack "$nuspec_dir\Aperea.MVC.nuspec" /OutputDirectory "$nupgk_dir\" }    
    Exec { .\tools\nuget.exe pack "$nuspec_dir\Aperea.MVC.Authentication.nuspec" /OutputDirectory "$nupgk_dir\" }    
    Exec { .\tools\nuget.exe pack "$nuspec_dir\Aperea.Mvc.Start.nuspec" /OutputDirectory "$nupgk_dir\" }    
}

Task Release -Depends BumpRevision, NuGet  {
}

Task ConvertStart {
    Write-Host "Converting MVC Project to NugetPackage" -ForegroundColor Green   
	ConvertMvcProject "$src_dir\Aperea.MVC.Start\Aperea.Mvc.Start.csproj" "$out_dir\_ProjectContent"
}
