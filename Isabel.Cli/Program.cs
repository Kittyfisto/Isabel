using System;
using Isabel.Speech.Recognition;
using Isabel.Speech.Synthesis;

namespace Isabel.Cli
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var executor = new DelayedCommandExecutor())
			using (var speechSynthesisEngine = new WindowsSpeechSynthesisEngine(new Speech.Synthesis.Configuration(), new Speech.Synthesis.Template()))
			using (var speechRecognitionEngine = new WindowsSpeechRecognitionEngine(executor, speechSynthesisEngine))
			{
				Console.WriteLine("Type exit to close the application");

				string line;
				while ((line = Console.ReadLine()) != "exit")
				{

				}
			}
		}
	}
}
