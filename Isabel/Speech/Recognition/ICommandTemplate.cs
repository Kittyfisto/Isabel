namespace Isabel.Speech.Recognition
{
	public interface ICommandTemplate
	{
		/// <summary>
		///     Creates a command from this template.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="InvalidCommandTemplateException">When the command template is misconfigured</exception>
		ICommand Create();
	}
}