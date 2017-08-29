namespace Isabel.Commands
{
	public sealed class BeepCommand
		: ICommand
	{
		private readonly ISpeechSynthesisEngine _speechSynthesisEngine;

		public BeepCommandTemplate Template { get; set; }

		public BeepCommand(ISpeechSynthesisEngine speechSynthesisEngine)
		{
			_speechSynthesisEngine = speechSynthesisEngine;
		}

		public object Clone()
		{
			return new BeepCommand(_speechSynthesisEngine) {Template = Template};
		}

		public void Execute()
		{
			var beep = Template?.Beep;
			if (beep != null)
			{
				_speechSynthesisEngine.Enqueue(beep);
			}
		}
	}
}