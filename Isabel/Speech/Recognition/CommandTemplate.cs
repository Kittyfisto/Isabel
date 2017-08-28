using System.Runtime.Serialization;

namespace Isabel.Speech.Recognition
{
	[DataContract]
	public sealed class CommandTemplate
	{
		[DataMember]
		public string Phrase { get; set; }

		[DataMember]
		public AbstractCommandTemplate Command { get; set; }
	}
}