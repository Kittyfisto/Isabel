using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Isabel.Speech.Recognition
{
	/// <summary>
	/// </summary>
	[DataContract]
	public sealed class Template
	{
		public Template()
		{
			Commands = new List<CommandTemplate>();
		}

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public List<CommandTemplate> Commands { get; set; }
	}
}