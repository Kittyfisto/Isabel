using System.Runtime.Serialization;

namespace Isabel.Commands
{
	[DataContract]
	public enum Key
	{
		[EnumMember] Ctrl,
		[EnumMember] CtrlLeft,
		[EnumMember] CtrlRight,

		[EnumMember] Alt,
		[EnumMember] AltLeft,
		[EnumMember] AltRight,

		[EnumMember] Shift,
		[EnumMember] ShiftLeft,
		[EnumMember] ShiftRight,

		[EnumMember] CapsLock,
		[EnumMember] Tab,
		[EnumMember] Return,
		[EnumMember] Escape,

		[EnumMember] Pause,
		[EnumMember] Print,
		[EnumMember] Roll,

		#region Multimedia Keys

		[EnumMember] PlayPause,
		[EnumMember] PreviousTrack,
		[EnumMember] NextTrack,
		[EnumMember] MuteVolume,
		[EnumMember] DecreaseVolume,
		[EnumMember] IncreaseVolume,

		#endregion
	}
}