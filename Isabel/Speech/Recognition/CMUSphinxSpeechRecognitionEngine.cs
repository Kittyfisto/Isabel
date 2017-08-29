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
		public CMUSphinxSpeechRecognitionEngine(ICommandExecutionEngine commandExecutionEngine)
			: base(commandExecutionEngine)
		{
		}

		protected override ICommand RecognizeNextCommand(CancellationToken token)
		{
			throw new NotImplementedException();
		}

		protected override void DisposeAdditional()
		{
			throw new NotImplementedException();
		}

		public override void AddTemplate(ITemplate template)
		{
			throw new NotImplementedException();
		}

		public override void RemoveTemplate(ITemplate template)
		{
			throw new NotImplementedException();
		}
	}
}