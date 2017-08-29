using System.Collections.Generic;
using System.Runtime.Serialization;
using Isabel.Speech.Recognition;

namespace Isabel.Commands
{
	[DataContract]
	public sealed class CommandSeriesTemplate
		: AbstractCommandTemplate
	{
		public CommandSeriesTemplate()
		{
			Commands = new List<AbstractCommandTemplate>();
		}

		[DataMember]
		public List<AbstractCommandTemplate> Commands { get; set; }
	}
}
