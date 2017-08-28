using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Threading;
using log4net;

namespace Isabel
{
	public sealed class DelayedCommandExecutor
		: ICommandExecutor
		, IDisposable
	{
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		private readonly ConcurrentQueue<ICommand> _pendingCommands;
		private readonly Thread _thread;
		private readonly CancellationTokenSource _disposedTokenSource;

		public DelayedCommandExecutor()
		{
			_disposedTokenSource = new CancellationTokenSource();
			_pendingCommands = new ConcurrentQueue<ICommand>();

			_thread = new Thread(ExecuteCommands)
			{
				IsBackground = true,
				Name = "Command Executor"
			};
			_thread.Start();
		}

		private void ExecuteCommands()
		{
			var token = _disposedTokenSource.Token;
			while (!token.IsCancellationRequested)
			{
				ICommand command;
				if (_pendingCommands.TryDequeue(out command))
				{
					ExecuteCommand(command);
				}
			}
		}

		private void ExecuteCommand(ICommand command)
		{
			try
			{
				command.Execute();
			}
			catch (Exception e)
			{
				Log.ErrorFormat("Caught unexpected exception: {0}", e);
			}
		}
		
		public void Execute(ICommand command)
		{
			_pendingCommands.Enqueue(command);
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
		}
	}
}