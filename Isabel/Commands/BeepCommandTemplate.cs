using System.Runtime.Serialization;
using Isabel.Speech.Recognition;
using Isabel.Speech.Synthesis;

namespace Isabel.Commands
{
	[DataContract]
	public sealed class BeepCommandTemplate : AbstractCommandTemplate
	{
		[DataMember]
		public Beep Beep { get; set; }

		[DataMember]
		public bool IsAsync { get; set; }
	}
}