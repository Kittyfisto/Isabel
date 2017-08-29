namespace Isabel
{
	public interface ICommandExecutionEngine
	{
		void Execute(ICommand command);
	}
}