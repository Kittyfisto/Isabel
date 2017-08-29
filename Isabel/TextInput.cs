using System;

namespace Isabel
{
	public sealed class TextInput
	{
		private readonly ICommandExecutionEngine _executionEngine;

		public TextInput(ICommandExecutionEngine executionEngine)
		{
			if (executionEngine == null)
				throw new ArgumentNullException(nameof(executionEngine));

			_executionEngine = executionEngine;
		}
	}
}