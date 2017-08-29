using System.Diagnostics.Contracts;
using Isabel.Speech.Recognition;

namespace Isabel
{
	/// <summary>
	///     Responsible for actually creating <see cref="ICommand" />s from their respective <see cref="ICommandTemplate" />.
	/// </summary>
	public interface ICommandFactory
	{
		/// <summary>
		/// Tries to create a command from its template.
		/// </summary>
		/// <param name="template"></param>
		/// <returns>A command or null if no command for this template can be found</returns>
		[Pure]
		ICommand TryCreate(ICommandTemplate template);
	}
}