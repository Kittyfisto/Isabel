using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;

namespace Isabel.Commands
{
	public sealed class KeyGestureCommand
		: ICommand
	{
		private readonly IKeyboardInputEngine _keyboardInputEngine;
		private IReadOnlyList<KeyGesture> _gestures;
		private KeyGestureCommandTemplate _template;

		public KeyGestureCommand(IKeyboardInputEngine keyboardInputEngine)
		{
			if (keyboardInputEngine == null)
				throw new ArgumentNullException(nameof(keyboardInputEngine));

			_keyboardInputEngine = keyboardInputEngine;
		}

		public KeyGestureCommandTemplate Template
		{
			get { return _template; }
			set
			{
				if (value == _template)
					return;

				_template = value;
				_gestures = Parse(value);
			}
		}

		private static TimeSpan? ParseTimeSpan(string timespan)
		{
			var prefixes = new Dictionary<TimeSpan, string>
			{
				{TimeSpan.FromMilliseconds(1), "ms"},
				{TimeSpan.FromSeconds(1), "s"}
			};

			foreach (var pair in prefixes)
			{
				if (timespan.EndsWith(pair.Value))
				{
					var number = timespan.Substring(0, timespan.Length - pair.Value.Length);
					float value;
					if (!float.TryParse(number, NumberStyles.Float, CultureInfo.InvariantCulture, out value))
					{
						return null;
					}

					var actualValue = TimeSpan.FromTicks((long) (value * pair.Key.Ticks));
					return actualValue;
				}
			}

			return null;
		}

		[Pure]
		private static IReadOnlyList<KeyGesture> Parse(KeyGestureCommandTemplate value)
		{
			var length = ParseTimeSpan(value.KeyPressLength) ?? TimeSpan.FromMilliseconds(100);
			var gestures = new List<KeyGesture>();
			foreach (var tmp in value.Gesture.Split(','))
			{
				var gesture = ParseGesture(length, tmp);
				if (gesture != null)
					gestures.Add(gesture.Value);
			}
			return gestures;
		}

		private static KeyGesture? ParseGesture(TimeSpan length, string keys)
		{
			var tmp = keys.Split('+');
			var actualKeys = new List<Key>(tmp.Length);
			foreach(var key in tmp)
			{
				Key actualKey;
				if (!Enum.TryParse(key, out actualKey))
					return null;

				actualKeys.Add(actualKey);
			}
			return new KeyGesture(length, actualKeys);
		}

		public void Execute()
		{
			_keyboardInputEngine.Enqueue(_gestures);
		}

		public IReadOnlyList<KeyGesture> Gestures => _gestures;

		public object Clone()
		{
			return new KeyGestureCommand(_keyboardInputEngine)
			{
				Template = Template
			};
		}
	}
}