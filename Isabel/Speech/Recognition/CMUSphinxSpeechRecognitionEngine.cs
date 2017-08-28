using System.Threading;

namespace Isabel.Speech.Recognition
{
	/// <summary>
	///     Uses the CMU Sphinx speech recognition library.
	/// </summary>
	public sealed class CMUSphinxSpeechRecognitionEngine
		: AbstractSpeechRecognitionEngine
	{
		public CMUSphinxSpeechRecognitionEngine(ICommandExecutor executor)
			: base(executor)
		{
		}

		protected override ICommand RecognizeCommand(CancellationToken token)
		{
			throw new System.NotImplementedException();
		}

		protected override void DisposeAdditional()
		{
			throw new System.NotImplementedException();
		}
	}
}