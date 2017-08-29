using System.Collections.Generic;

namespace Isabel.Speech.Recognition
{
	public interface ITemplate
	{
		IReadOnlyList<VoiceCommandTemplate> VoiceCommands { get; }
	}
}