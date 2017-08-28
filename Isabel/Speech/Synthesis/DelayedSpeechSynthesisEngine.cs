using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Threading;
using log4net;

namespace Isabel.Speech.Synthesis
{
	public abstract class DelayedSpeechSynthesisEngine
		: ISpeechSynthesisEngine
	{
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		private readonly ConcurrentQueue<Phrase> _pendingPhrases;
		private readonly CancellationTokenSource _disposedTokenSource;
		private readonly Thread _thread;

		protected DelayedSpeechSynthesisEngine()
		{
			_pendingPhrases = new ConcurrentQueue<Phrase>();
			_disposedTokenSource = new CancellationTokenSource();
			_thread = new Thread(Synthesize)
			{
				IsBackground = true,
				Name = "Speech Synthesis"
			};
			_thread.Start();
		}

		private void Synthesize()
		{
			var token = _disposedTokenSource.Token;
			try
			{
				while (!token.IsCancellationRequested)
				{
					Phrase phrase;
					if (_pendingPhrases.TryDequeue(out phrase))
					{
						TrySpeak(phrase);
					}
					else
					{
						Thread.Sleep(TimeSpan.FromMilliseconds(100));
					}
				}
			}
			catch (Exception e)
			{
				Log.FatalFormat("Caught unexpected exception: {0}", e);
			}
		}

		private void TrySpeak(Phrase phrase)
		{
			try
			{
				var text = phrase.Text;
				if (text != null)
				{
					Speak(text);
				}
				else
				{
					var beep = phrase.Beep;
					if (beep != Synthesis.Beep.None)
					{
						Beep(beep);
					}
				}
			}
			catch (Exception e)
			{
				Log.ErrorFormat("Caught unexpected exception: {0}", e);
			}
		}

		public void Dispose()
		{
			_disposedTokenSource.Cancel();
			var waitTime = TimeSpan.FromSeconds(1);
			if (_thread.Join(waitTime))
			{
				_disposedTokenSource.Dispose();
			}
			else
			{
				Log.WarnFormat("Thread {0} didn't stop after {1} seconds", _thread, waitTime.TotalSeconds);
			}

			DisposeAdditional();
		}

		protected abstract void Beep(Beep beep);
		protected abstract void Speak(string phrase);
		protected abstract void DisposeAdditional();

		public void Speak(Phrase phrase)
		{
			_pendingPhrases.Enqueue(phrase);
		}
	}
}