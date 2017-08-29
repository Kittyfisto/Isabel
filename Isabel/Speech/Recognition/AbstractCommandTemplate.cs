using System.Runtime.Serialization;

namespace Isabel.Speech.Recognition
{
	[DataContract]
	public abstract class AbstractCommandTemplate
		: ICommandTemplate
	{
		public abstract ICommand Create();
	}
}