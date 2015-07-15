
Microsoft Visual Studio Solution File, Format Version 11.00
# Visual Studio 2010
Project("{E24C65DC-7377-472B-9ABA-BC803B73C61A}") = "nidushealth.com", ".", "{286F83A5-A04F-4673-8B60-9B5F084FEE9E}"
	ProjectSection(WebsiteProperties) = preProject
		TargetFrameworkMoniker = ".NETFramework,Version%3Dv4.0"
		Debug.AspNetCompiler.VirtualPath = "/nidushealth.com"
		Debug.AspNetCompiler.PhysicalPath = "H:\SoftWeb\nidushealth.com\"
		Debug.AspNetCompiler.TargetPath = "PrecompiledWeb\nidushealth.com\"
		Debug.AspNetCompiler.Updateable = "true"
		Debug.AspNetCompiler.ForceOverwrite = "true"
		Debug.AspNetCompiler.FixedNames = "false"
		Debug.AspNetCompiler.Debug = "True"
		Release.AspNetCompiler.VirtualPath = "/nidushealth.com"
		Release.AspNetCompiler.PhysicalPath = "H:\SoftWeb\nidushealth.com\"
		Release.AspNetCompiler.TargetPath = "PrecompiledWeb\nidushealth.com\"
		Release.AspNetCompiler.Updateable = "true"
		Release.AspNetCompiler.ForceOverwrite = "true"
		Release.AspNetCompiler.FixedNames = "false"
		Release.AspNetCompiler.Debug = "False"
		VWDPort = "51804"
	EndProjectSection
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{286F83A5-A04F-4673-8B60-9B5F084FEE9E}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{286F83A5-A04F-4673-8B60-9B5F084FEE9E}.Debug|Any CPU.Build.0 = Debug|Any CPU
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
EndGlobal
