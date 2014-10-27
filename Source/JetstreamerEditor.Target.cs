// Copyright (c) 2014 Sean Davies and Jonathan Meldrum

using UnrealBuildTool;
using System.Collections.Generic;

public class JetstreamerEditorTarget : TargetRules
{
	public JetstreamerEditorTarget(TargetInfo Target)
	{
		Type = TargetType.Editor;
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
