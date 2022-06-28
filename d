[1mdiff --git a/FileCabinet.sln b/FileCabinet.sln[m
[1mnew file mode 100644[m
[1mindex 0000000..c2e1d70[m
[1m--- /dev/null[m
[1m+++ b/FileCabinet.sln[m
[36m@@ -0,0 +1,22 @@[m
[32m+[m[32m﻿[m
[32m+[m[32mMicrosoft Visual Studio Solution File, Format Version 12.00[m
[32m+[m[32m# Visual Studio Version 16[m
[32m+[m[32mVisualStudioVersion = 16.0.30114.105[m
[32m+[m[32mMinimumVisualStudioVersion = 10.0.40219.1[m
[32m+[m[32mProject("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "FileCabinetApp", "FileCabinetApp\FileCabinetApp.csproj", "{F3334E96-B830-45C2-B882-F8DB0DE67BE6}"[m
[32m+[m[32mEndProject[m
[32m+[m[32mGlobal[m
[32m+[m	[32mGlobalSection(SolutionConfigurationPlatforms) = preSolution[m
[32m+[m		[32mDebug|Any CPU = Debug|Any CPU[m
[32m+[m		[32mRelease|Any CPU = Release|Any CPU[m
[32m+[m	[32mEndGlobalSection[m
[32m+[m	[32mGlobalSection(SolutionProperties) = preSolution[m
[32m+[m		[32mHideSolutionNode = FALSE[m
[32m+[m	[32mEndGlobalSection[m
[32m+[m	[32mGlobalSection(ProjectConfigurationPlatforms) = postSolution[m
[32m+[m		[32m{F3334E96-B830-45C2-B882-F8DB0DE67BE6}.Debug|Any CPU.ActiveCfg = Debug|Any CPU[m
[32m+[m		[32m{F3334E96-B830-45C2-B882-F8DB0DE67BE6}.Debug|Any CPU.Build.0 = Debug|Any CPU[m
[32m+[m		[32m{F3334E96-B830-45C2-B882-F8DB0DE67BE6}.Release|Any CPU.ActiveCfg = Release|Any CPU[m
[32m+[m		[32m{F3334E96-B830-45C2-B882-F8DB0DE67BE6}.Release|Any CPU.Build.0 = Release|Any CPU[m
[32m+[m	[32mEndGlobalSection[m
[32m+[m[32mEndGlobal[m
[1mdiff --git a/FileCabinetApp/FileCabinetApp.csproj b/FileCabinetApp/FileCabinetApp.csproj[m
[1mnew file mode 100644[m
[1mindex 0000000..0f7a2a5[m
[1m--- /dev/null[m
[1m+++ b/FileCabinetApp/FileCabinetApp.csproj[m
[36m@@ -0,0 +1,23 @@[m
[32m+[m[32m<Project Sdk="Microsoft.NET.Sdk">[m
[32m+[m
[32m+[m[32m  <PropertyGroup>[m
[32m+[m[32m    <OutputType>Exe</OutputType>[m
[32m+[m[32m    <TargetFramework>net6.0</TargetFramework>[m
[32m+[m[32m    <ImplicitUsings>enable</ImplicitUsings>[m
[32m+[m[32m    <Nullable>enable</Nullable>[m
[32m+[m[32m    <CodeAnalysisRuleSet>code-analysis.ruleset</CodeAnalysisRuleSet>[m
[32m+[m[32m    <DocumentationFile>$(OutputPath)$(AssemblyName).xml</DocumentationFile>[m
[32m+[m[32m    <NoWarn>$(NoWarn),1573,1591,1712</NoWarn>[m
[32m+[m[32m    <EnableNETAnalyzers>true</EnableNETAnalyzers>[m
[32m+[m[32m    <AnalysisMode>AllEnabledByDefault</AnalysisMode>[m
[32m+[m[32m    <CodeAnalysisTreatWarningsAsErrors>false</CodeAnalysisTreatWarningsAsErrors>[m
[32m+[m[32m  </PropertyGroup>[m
[32m+[m
[32m+[m[32m  <ItemGroup>[m
[32m+[m[32m    <PackageReference Include="StyleCop.Analyzers" Version="1.1.118">[m
[32m+[m[32m      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>[m
[32m+[m[32m      <PrivateAssets>all</PrivateAssets>[m
[32m+[m[32m    </PackageReference>[m
[32m+[m[32m  </ItemGroup>[m
[32m+[m
[32m+[m[32m</Project>[m
[1mdiff --git a/FileCabinetApp/FileCabinetApp.xml b/FileCabinetApp/FileCabinetApp.xml[m
[1mnew file mode 100644[m
[1mindex 0000000..0cb5c7b[m
[1m--- /dev/null[m
[1m+++ b/FileCabinetApp/FileCabinetApp.xml[m
[36m@@ -0,0 +1,8 @@[m
[32m+[m[32m<?xml version="1.0"?>[m
[32m+[m[32m<doc>[m
[32m+[m[32m    <assembly>[m
[32m+[m[32m        <name>FileCabinetApp</name>[m
[32m+[m[32m    </assembly>[m
[32m+[m[32m    <members>[m
[32m+[m[32m    </members>[m
[32m+[m[32m</doc>[m
[1mdiff --git a/FileCabinetApp/Program.cs b/FileCabinetApp/Program.cs[m
[1mnew file mode 100644[m
[1mindex 0000000..3751555[m
[1m--- /dev/null[m
[1m+++ b/FileCabinetApp/Program.cs[m
[36m@@ -0,0 +1,2 @@[m
[32m+[m[32m﻿// See https://aka.ms/new-console-template for more information[m
[32m+[m[32mConsole.WriteLine("Hello, World!");[m
[1mdiff --git a/FileCabinetApp/code-analysis.ruleset b/FileCabinetApp/code-analysis.ruleset[m
[1mnew file mode 100644[m
[1mindex 0000000..2c882a9[m
[1m--- /dev/null[m
[1m+++ b/FileCabinetApp/code-analysis.ruleset[m
[36m@@ -0,0 +1,84 @@[m
[32m+[m[32m<?xml version="1.0" encoding="utf-8"?>[m
[32m+[m[32m<RuleSet Name="Custom Rulset" Description="Custom Rulset" ToolsVersion="14.0">[m
[32m+[m[32m    <Rules AnalyzerId="AsyncUsageAnalyzers" RuleNamespace="AsyncUsageAnalyzers">[m
[32m+[m[32m        <Rule Id="UseConfigureAwait" Action="Warning" />[m
[32m+[m[32m    </Rules>[m
[32m+[m[32m    <Rules AnalyzerId="Microsoft.Analyzers.ManagedCodeAnalysis" RuleNamespace="Microsoft.Rules.Managed">[m
[32m+[m[32m        <Rule Id="CA1001" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA1009" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA1014" Action="None" />[m
[32m+[m[32m        <Rule Id="CA1016" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA1033" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA1049" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA1052" Action="Error" />[m
[32m+[m[32m        <Rule Id="CA1060" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA1061" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA1063" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA1065" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA1301" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA1303" Action="None" />[m
[32m+[m[32m        <Rule Id="CA1400" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA1401" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA1403" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA1404" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA1405" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA1410" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA1415" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA1801" Action="None" />[m
[32m+[m[32m        <Rule Id="CA1821" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA1900" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA1901" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2002" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2100" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2101" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2108" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2111" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2112" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2114" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2116" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2117" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2122" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2123" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2124" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2126" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2131" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2132" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2133" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2134" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2137" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2138" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2140" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2141" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2146" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2147" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2149" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2200" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2202" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2207" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2212" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2213" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2214" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2216" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2220" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2229" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2231" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2232" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2235" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2236" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2237" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2238" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2240" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2241" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA2242" Action="Warning" />[m
[32m+[m[32m        <Rule Id="CA1012" Action="Warning" />[m
[32m+[m[32m    </Rules>[m
[32m+[m[32m    <Rules AnalyzerId="StyleCop.Analyzers" RuleNamespace="StyleCop.Analyzers">[m
[32m+[m[32m        <Rule Id="SA1200" Action="None" />[m
[32m+[m[32m        <Rule Id="SA1305" Action="Warning" />[m
[32m+[m[32m        <Rule Id="SA1400" Action="Error" />[m
[32m+[m[32m        <Rule Id="SA1412" Action="Warning" />[m
[32m+[m[32m        <Rule Id="SA1600" Action="None" />[m
[32m+[m[32m        <Rule Id="SA1609" Action="Warning" />[m
[32m+[m[32m        <Rule Id="SA1633" Action="None" />[m
[32m+[m[32m    </Rules>[m
[32m+[m[32m</RuleSet>[m
\ No newline at end of file[m
