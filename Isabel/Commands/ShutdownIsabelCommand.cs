namespace Isabel.Commands
{
	public sealed class ShutdownIsabelCommand
		: ICommand
	{
		private readonly IApplication _application;

		public ShutdownIsabelCommand(IApplication application)
		{
			_application = application;
		}

		public object Clone()
		{
			return this;
		}

		public void Execute()
		{
			_application.Exit();
		}
	}
}