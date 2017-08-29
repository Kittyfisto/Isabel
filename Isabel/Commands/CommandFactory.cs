using System;
using System.Collections.Generic;
using Isabel.Speech.Recognition;

namespace Isabel.Commands
{
	public sealed class CommandFactory
		: ICommandFactory
	{
		private readonly Dictionary<Type, Func<ICommandTemplate, ICommand>> _factories;

		public CommandFactory(ISpeechSynthesisEngine speechSynthesisEngine, IKeyboardInputEngine keyboardInputEngine, IApplication application)
		{
			if (speechSynthesisEngine == null)
				throw new ArgumentNullException(nameof(speechSynthesisEngine));
			if (keyboardInputEngine == null)
				throw new ArgumentNullException(nameof(keyboardInputEngine));
			if (application == null)
				throw new ArgumentNullException(nameof(application));

			_factories = new Dictionary<Type, Func<ICommandTemplate, ICommand>>();

			Add<BeepCommandTemplate>(x => new BeepCommand(speechSynthesisEngine) {Template = x});
			Add<KeyGestureCommandTemplate>(x => new KeyGestureCommand(keyboardInputEngine) {Template = x});
			Add<ShutdownIsabelCommandTemplate>(x => new ShutdownIsabelCommand(application));
		}

		public ICommand TryCreate(ICommandTemplate template)
		{
			var type = template.GetType();
			Func<ICommandTemplate, ICommand> factoryMethod;
			if (_factories.TryGetValue(type, out factoryMethod))
			{
				var command = factoryMethod(template);
				return command;
			}

			return null;
		}

		private void Add<T>(Func<T, ICommand> factory) where T : ICommandTemplate
		{
			_factories.Add(typeof(T), x => factory((T) x));
		}
	}
}