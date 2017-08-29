using System;

namespace Isabel.Commands
{
	public sealed class SpeechCommand
		: ICommand
	{
		private readonly ISpeechSynthesisEngine _speechSynthesisEngine;

		public SpeechCommand(ISpeechSynthesisEngine speechSynthesisEngine)
		{
			if (speechSynthesisEngine == null)
				throw new ArgumentNullException(nameof(speechSynthesisEngine));

			_speechSynthesisEngine = speechSynthesisEngine;
		}

		public SpeechCommandTemplate Template { get; set; }

		public object Clone()
		{
			return new SpeechCommand(_speechSynthesisEngine) {Template = Template};
		}

		public void Execute()
		{
			var speech = Template?.Speech;
			if (speech != null)
			{
				if (Template.IsAsync)
				{
					_speechSynthesisEngine.Enqueue(speech);
				}
				else
				{
					_speechSynthesisEngine.Execute(speech);
				}
			}
		}
	}
}