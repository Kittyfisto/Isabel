using System;
using Isabel.Commands;
using Isabel.DefaultTemplates;
using Isabel.Input.Keyboard;
using Isabel.Speech.Recognition;
using Isabel.Speech.Synthesis;

namespace Isabel.Cli
{
	class Program
		: IApplication
	{
		static void Main(string[] args)
		{
			new Program().Run();
		}

		public void Run()
		{
			using (var commandExecutionEngine = new DelayedCommandExecutionEngine())
			using (var speechSynthesisEngine = new WindowsSpeechSynthesisEngine(new Speech.Synthesis.Configuration(), new Speech.Synthesis.Template()))
			using (var keyboardInputEngine = new KeyboardInputEngine())
			using (var speechRecognitionEngine = new WindowsSpeechRecognitionEngine(commandExecutionEngine, new CommandFactory(speechSynthesisEngine, keyboardInputEngine, this), new Speech.Recognition.Configuration()))
			{
				speechRecognitionEngine.AddTemplate(TrekTemplate.Instance);

				Console.WriteLine("Type exit to close the application");

				string line;
				while ((line = Console.ReadLine()) != "exit")
				{

				}
			}
		}

		public void Exit()
		{
			Environment.Exit(0);
		}
	}
}
