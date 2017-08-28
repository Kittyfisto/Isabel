using System;
using Isabel.Speech.Synthesis;

namespace Isabel
{
	public interface ISpeechSynthesisEngine
		: IDisposable
	{
		void Speak(Phrase phrase);
	}
}