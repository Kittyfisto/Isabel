using Isabel.Commands;
using Isabel.Speech.Recognition;
using Isabel.Speech.Synthesis;
using ITemplate = Isabel.Speech.Recognition.ITemplate;
using Template = Isabel.Speech.Recognition.Template;

namespace Isabel.DefaultTemplates
{
	public static class TrekTemplate
	{
		public static readonly ITemplate Instance;

		static TrekTemplate()
		{
			Instance = new Template
			{
				VoiceCommands =
				{
					new VoiceCommandTemplate
					{
						Phrase = "Computer",
						Command = new BeepCommandTemplate
						{
							Beep = Beep.Affirmative
						}
					},

					new VoiceCommandTemplate
					{
						Phrase = "Shutdown",
						Command = new CommandSeriesTemplate
						{
							Commands =
							{
								new SpeechCommandTemplate {Speech = "Goodbye"},
								new ShutdownIsabelCommandTemplate()
							}
						}
					}
				}
			};
		}
	}
}