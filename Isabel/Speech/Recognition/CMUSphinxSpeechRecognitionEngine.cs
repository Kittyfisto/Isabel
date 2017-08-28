using System;
using System.Threading;

namespace Isabel.Speech.Recognition
{
	/// <summary>
	///     Uses the CMU Sphinx speech recognition library.
	/// </summary>
	/// <remarks>
	///     TODO: Implement this class to actually make use of this platform independent speech recognition engine.
	/// </remarks>
	public sealed class CMUSphinxSpeechRecognitionEngine
		: AbstractSpeechRecognitionEngine
	{
		public CMUSphinxSpeechRecognitionEngine(ICommandExecutor executor)
			: base(executor)
		{
		}

		protected override ICommand RecognizeCommand(CancellationToken token)
		{
			throw new NotImplementedException();
		}

		protected override void DisposeAdditional()
		{
			throw new NotImplementedException();
		}
	}
}