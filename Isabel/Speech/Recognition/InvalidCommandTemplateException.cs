using System;
using System.Runtime.Serialization;

namespace Isabel.Speech.Recognition
{
	/// <summary>
	///     An exception which shall be thrown when an invalid command template is encountered (i.e. values
	///     are out of range, values are missing, etc...).
	/// </summary>
	public class InvalidCommandTemplateException
		: Exception
	{
		public InvalidCommandTemplateException()
		{
		}

		/// <summary>
		/// </summary>
		/// <param name="message"></param>
		public InvalidCommandTemplateException(string message)
			: base(message)
		{
		}

		public InvalidCommandTemplateException(string message, Exception inner)
			: base(message, inner)
		{
		}

		public InvalidCommandTemplateException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}