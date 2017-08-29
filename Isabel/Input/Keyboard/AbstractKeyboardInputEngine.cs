using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Isabel.Commands;
using log4net;

namespace Isabel.Input.Keyboard
{
	public abstract class AbstractKeyboardInputEngine
		: IKeyboardInputEngine
	{
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		private readonly Thread _thread;
		private readonly CancellationTokenSource _disposedTokenSource;
		private readonly ConcurrentQueue<IReadOnlyList<KeyGesture>> _pendingGestures;

		protected AbstractKeyboardInputEngine()
		{
			_pendingGestures = new ConcurrentQueue<IReadOnlyList<KeyGesture>>();
			_disposedTokenSource = new CancellationTokenSource();
			_thread = new Thread(PressKeys)
			{
				IsBackground = true,
				Name = "Press keys"
			};
			_thread.Start();
		}

		private void PressKeys()
		{
			var token = _disposedTokenSource.Token;
			try
			{
				while (!token.IsCancellationRequested)
				{
					IReadOnlyList<KeyGesture> gesture;
					if (_pendingGestures.TryDequeue(out gesture))
					{
						TryExecute(gesture);
					}
					else
					{
						Thread.Sleep(TimeSpan.FromMilliseconds(10));
					}
				}
			}
			catch (Exception e)
			{
				Log.FatalFormat("Caught unexpected exception: {0}", e);
			}
		}

		private void TryExecute(IReadOnlyList<KeyGesture> gestures)
		{
			try
			{
				foreach (var gesture in gestures)
				{
					try
					{
						foreach (var key in gesture.Keys)
						{
							Down(key);
						}
						Thread.Sleep(gesture.Length);
					}
					finally
					{
						foreach (var key in gesture.Keys)
						{
							Up(key);
						}
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

		protected abstract void DisposeAdditional();
		protected abstract void Down(Key key);
		protected abstract void Up(Key key);

		public void Enqueue(IReadOnlyList<KeyGesture> gestures)
		{
			if (gestures == null)
				throw new ArgumentNullException(nameof(gestures));

			_pendingGestures.Enqueue(gestures);
		}
	}
}