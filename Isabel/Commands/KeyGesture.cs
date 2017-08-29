using System;
using System.Collections.Generic;
using System.Linq;

namespace Isabel.Commands
{
	public struct KeyGesture
	{
		public readonly Key[] Keys;
		public readonly TimeSpan Length;

		public KeyGesture(TimeSpan length, IEnumerable<Key> keys)
			: this(length, keys.ToArray())
		{ }

		public KeyGesture(TimeSpan length, params Key[] keys)
		{
			Length = length;
			Keys = keys;
		}

		public override string ToString()
		{
			return string.Format("{0}, {1}s", string.Join("+", Keys), Length.TotalSeconds);
		}
	}
}