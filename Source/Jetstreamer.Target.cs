// Copyright (c) 2014 Sean Davies and Jonathan Meldrum

using UnrealBuildTool;
using System.Collections.Generic;

public class JetstreamerTarget : TargetRules
{
	public JetstreamerTarget(TargetInfo Target)
	{
		Type = TargetType.Game;
	}

	//
	// TargetRules interface.
	//

	public override void SetupBinaries(
		TargetInfo Target,
		ref List<UEBuildBinaryConfiguration> OutBuildBinaryConfigurations,
		ref List<string> OutExtraModuleNames
		)
	{
		OutExtraModuleNames.AddRange( new string[] { "Jetstreamer" } );
	}
}
