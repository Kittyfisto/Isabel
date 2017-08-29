using System;
using Isabel.Speech.Synthesis;

namespace Isabel
{
	public interface ISpeechSynthesisEngine
		: IDisposable
	{
		void Enqueue(Phrase phrase);
		void Execute(Phrase phrase);
	}
}