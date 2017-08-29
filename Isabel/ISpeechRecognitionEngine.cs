using System;
using Isabel.Speech.Recognition;

namespace Isabel
{
	/// <summary>
	///     The interface for an object, responsible for recognizing voice commands and translating them to
	///     actual <see cref="ICommand" />s.
	///     Each detected command is to be forwarded to a <see cref="ICommandExecutionEngine" />.
	/// </summary>
	public interface ISpeechRecognitionEngine
		: IDisposable
	{
		/// <summary>
		/// </summary>
		/// <param name="template"></param>
		void AddTemplate(ITemplate template);

		/// <summary>
		/// </summary>
		/// <remarks>
		///     Templates are identified by reference.
		/// </remarks>
		/// <param name="template"></param>
		void RemoveTemplate(ITemplate template);
	}
}