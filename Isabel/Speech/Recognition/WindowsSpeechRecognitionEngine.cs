using System;
using System.Collections.Concurrent;
using System.Speech.Recognition;
using System.Threading;
using Isabel.Speech.Synthesis;

namespace Isabel.Speech.Recognition
{
	/// <summary>
	///     Uses the windows speech recognition engine.
	/// </summary>
	public sealed class WindowsSpeechRecognitionEngine
		: AbstractSpeechRecognitionEngine
	{
		private readonly ISpeechSynthesisEngine _speechSynthesisEngine;
		private readonly SpeechRecognitionEngine _engine;
		private readonly ConcurrentQueue<RecognitionResult> _results;

		public WindowsSpeechRecognitionEngine(ICommandExecutor executor, ISpeechSynthesisEngine speechSynthesisEngine)
			: base(executor)
		{
			if (speechSynthesisEngine == null)
				throw new ArgumentNullException(nameof(speechSynthesisEngine));

			_speechSynthesisEngine = speechSynthesisEngine;
			_results = new ConcurrentQueue<RecognitionResult>();

			_engine = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));

			var builder = new GrammarBuilder("Computer");
			var grammar = new Grammar(builder);
			_engine.LoadGrammar(grammar);
			_engine.SpeechRecognized += EngineOnSpeechRecognized;

			_engine.SetInputToDefaultAudioDevice();
			_engine.RecognizeAsync(RecognizeMode.Multiple);
		}

		private void EngineOnSpeechRecognized(object sender, SpeechRecognizedEventArgs args)
		{
			_results.Enqueue(args.Result);
		}

		protected override ICommand RecognizeCommand(CancellationToken token)
		{
			while (!token.IsCancellationRequested)
			{
				RecognitionResult result;
				if (_results.TryDequeue(out result))
				{
					return TryCreateCommandFrom(result);
				}

				Thread.Sleep(TimeSpan.FromMilliseconds(100));
			}

			return null;
		}

		private ICommand TryCreateCommandFrom(RecognitionResult result)
		{
			if (string.Equals(result.Text, "computer", StringComparison.InvariantCultureIgnoreCase))
			{
				_speechSynthesisEngine.Speak(Beep.Affirmative);
				//_speechSynthesisEngine.Speak("Affirmative");
			}
			else
			{
				_speechSynthesisEngine.Speak(Beep.Error);
			}

			return null;
		}

		protected override void DisposeAdditional()
		{
			_engine.Dispose();
		}
	}
}