using System;
using System.Collections.Generic;
using System.Media;
using System.Speech.Synthesis;

namespace Isabel.Speech.Synthesis
{
	public sealed class WindowsSpeechSynthesisEngine
		: DelayedSpeechSynthesisEngine
	{
		private readonly SpeechSynthesizer _engine;
		private readonly IReadOnlyDictionary<Beep, SoundPlayer> _beeps;

		public WindowsSpeechSynthesisEngine(IConfiguration configuration, ITemplate template)
		{
			if (configuration == null)
				throw new ArgumentNullException(nameof(configuration));
			if (template == null)
				throw new ArgumentNullException(nameof(template));

			_engine = new SpeechSynthesizer();
			_engine.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);
			_engine.SetOutputToDefaultAudioDevice();

			_beeps = new Dictionary<Beep, SoundPlayer>
			{
				{Synthesis.Beep.Affirmative, CreateSoundPlayer(@"C:\Users\Simon\Documents\GitHub\Isabel\Isabel\input_ok_3_clean.wav")},
				{Synthesis.Beep.Error, CreateSoundPlayer(@"C:\Users\Simon\Documents\GitHub\Isabel\Isabel\computer_error.wav")}
			};
		}

		private static SoundPlayer CreateSoundPlayer(string location)
		{
			return new SoundPlayer(location);
		}

		protected override void Beep(Beep beep)
		{
			SoundPlayer player;
			if (_beeps.TryGetValue(beep, out player))
			{
				player.PlaySync();
			}
		}

		protected override void Speak(string phrase)
		{
			_engine.Speak(phrase);
		}

		protected override void DisposeAdditional()
		{
			_engine.Dispose();
		}
	}
}