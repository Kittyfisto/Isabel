using System.Collections.Generic;
using System.Linq;

namespace Isabel.Commands
{
	public sealed class CommandSeries
		: ICommand
	{
		private readonly ICommand[] _commands;

		public CommandSeries(IEnumerable<ICommand> commands)
		{
			_commands = commands.ToArray();
		}

		public object Clone()
		{
			return new CommandSeries(_commands.Select(x => (ICommand)x.Clone()));
		}

		public void Execute()
		{
			foreach (var command in _commands)
			{
				command.Execute();
			}
		}
	}
}