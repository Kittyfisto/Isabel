﻿using System.Runtime.Serialization;
using Isabel.Speech.Recognition;

namespace Isabel.Commands
{
	/// <summary>
	///     A template for the <see cref="KeyGestureCommand" />.
	/// </summary>
	[DataContract]
	public sealed class KeyGestureCommandTemplate
		: AbstractCommandTemplate
	{
		public KeyGestureCommandTemplate()
		{
			KeyPressLength = "33ms";
		}

		/// <summary>
		///     The (human readable) key gesture that shall be performed.
		///     a+b means that the key a and b are pressed at the same time.
		///     a,b means that a is pressed, then released and then b is pressed
		///     and released. Both + and , can be concatenated ad infinitum.
		/// </summary>
		/// <example>
		/// "ctrl+r,ctrl+w"
		/// "ctrlleft+b"
		/// "Home", "HOME", "HoMe"
		/// </example>
		[DataMember]
		public string Gesture { get; set; }

		/// <summary>
		/// </summary>
		[DataMember]
		public string KeyPressLength { get; set; }
	}
}