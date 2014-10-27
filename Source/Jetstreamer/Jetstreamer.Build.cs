// Copyright (c) 2014 Sean Davies and Jonathan Meldrum

using UnrealBuildTool;

public class Jetstreamer : ModuleRules
{
	public Jetstreamer(TargetInfo Target)
	{
		PublicDependencyModuleNames.AddRange(new string[] 
        { 
            "Core", 
            "CoreUObject", 
            "Engine", 
            "InputCore",
            "Paper2D"
        });
	}
}
