param($installPath, $toolsPath, $package, $project)

$project.ProjectItems.Item("ResourceStrings.resx").Properties.Item("CustomTool").Value = "PublicResXFileCodeGenerator"