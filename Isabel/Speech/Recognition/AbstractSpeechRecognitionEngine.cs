﻿using System;
using System.Reflection;
using System.Threading;
using log4net;

namespace Isabel.Speech.Recognition
{
	public abstract class AbstractSpeechRecognitionEngine
		: ISpeechRecognitionEngine
	{
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		private readonly ICommandExecutor _executor;
		private readonly Thread _thread;
		private readonly CancellationTokenSource _disposedTokenSource;

		protected AbstractSpeechRecognitionEngine(ICommandExecutor executor)
		{
			if (executor == null)
				throw new ArgumentNullException(nameof(executor));

			_executor = executor;
			_disposedTokenSource = new CancellationTokenSource();
			_thread = new Thread(RecognizeSpeech)
			{
				IsBackground = true,
				Name = "Recognize Speech"
			};
			_thread.Start();
		}

		private void RecognizeSpeech()
		{
			try
			{
				var token = _disposedTokenSource.Token;
				while (!token.IsCancellationRequested)
				{
					var command = TryRecognizeCommand(token);
					if (command != null)
					{
						_executor.Execute(command);
					}
				}
			}
			catch (Exception e)
			{
				Log.FatalFormat("Caught unexpected exception: {0}", e);
			}
		}

		private ICommand TryRecognizeCommand(CancellationToken token)
		{
			try
			{
				return RecognizeCommand(token);
			}
			catch (Exception e)
			{
				Log.ErrorFormat("Caught unexpected exception: {0}", e);
				return null;
			}
		}

		protected abstract ICommand RecognizeCommand(CancellationToken token);

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

		protected abstract void DisposeAdditional();
	}
}