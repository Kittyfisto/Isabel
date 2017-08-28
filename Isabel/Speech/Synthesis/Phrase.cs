namespace Isabel.Speech.Synthesis
{
	public sealed class Phrase
	{
		public string Text;
		public Beep Beep;

		public static implicit operator Phrase(string text)
		{
			return new Phrase { Text = text };
		}

		public static implicit operator Phrase(Beep beep)
		{
			return new Phrase { Beep = beep };
		}
	}
}