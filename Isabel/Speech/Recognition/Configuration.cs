using System.Runtime.Serialization;

namespace Isabel.Speech.Recognition
{
	[DataContract]
	public sealed class Configuration
		: IConfiguration
	{
		[DataMember]
		public Engine Engine { get; set; }
	}
}