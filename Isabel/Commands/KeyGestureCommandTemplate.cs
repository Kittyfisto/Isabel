using System.Runtime.Serialization;
using Isabel.Speech.Recognition;

namespace Isabel.Commands
{
	[DataContract]
	public sealed class KeyGestureCommandTemplate
		: AbstractCommandTemplate
	{
		[DataMember]
		public string Gesture { get; set; }
	}
}