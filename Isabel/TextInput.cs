using System;

namespace Isabel
{
	public sealed class TextInput
	{
		private readonly ICommandExecutor _executor;

		public TextInput(ICommandExecutor executor)
		{
			if (executor == null)
				throw new ArgumentNullException(nameof(executor));

			_executor = executor;
		}
	}
}