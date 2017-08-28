using FluentAssertions;
using Isabel.Commands;
using Isabel.Speech.Recognition;
using NUnit.Framework;

namespace Isabel.Test.Speech.Recognition
{
	[TestFixture]
	public sealed class TemplateTest
	{
		[Test]
		public void TestRoundtrip()
		{
			var value = new Template
			{
				Commands =
				{
					new CommandTemplate
					{
						Phrase = "Dimn lights",
						Command = new KeyGestureCommandTemplate
						{
							Gesture = "ctrl+a"
						}
					}
				}
			};
			var serializer = new Serializer();
			var data = serializer.Serialize(value);
			serializer.Print(data);
			var actualValue = serializer.Deserialize<Template>(data);
			actualValue.Should().NotBeNull();
		}
	}
}