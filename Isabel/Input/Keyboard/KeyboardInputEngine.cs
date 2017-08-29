using System.Diagnostics.Contracts;
using WindowsInput;
using WindowsInput.Native;
using Isabel.Commands;

namespace Isabel.Input.Keyboard
{
	/// <summary>
	///     Uses <see cref="InputSimulator" /> to simulate keyboard presses in the system.
	/// </summary>
	public sealed class KeyboardInputEngine
		: AbstractKeyboardInputEngine
	{
		private readonly InputSimulator _simulator;

		public KeyboardInputEngine()
		{
			_simulator = new InputSimulator();
		}

		protected override void DisposeAdditional()
		{
		}

		protected override void Down(Key key)
		{
			var virtualKey = Translate(key);
			if (virtualKey != null)
				_simulator.Keyboard.KeyDown(virtualKey.Value);
		}

		protected override void Up(Key key)
		{
			var virtualKey = Translate(key);
			if (virtualKey != null)
				_simulator.Keyboard.KeyUp(virtualKey.Value);
		}

		[Pure]
		private static VirtualKeyCode? Translate(Key key)
		{
			switch (key)
			{
				case Key.Ctrl: return VirtualKeyCode.CONTROL;
				case Key.CtrlLeft: return VirtualKeyCode.LCONTROL;
				case Key.CtrlRight: return VirtualKeyCode.RCONTROL;

				case Key.Shift: return VirtualKeyCode.SHIFT;
				case Key.ShiftLeft: return VirtualKeyCode.LSHIFT;
				case Key.ShiftRight: return VirtualKeyCode.RSHIFT;

				case Key.CapsLock: return VirtualKeyCode.CAPITAL;
				case Key.Tab: return VirtualKeyCode.TAB;
				case Key.Escape: return VirtualKeyCode.ESCAPE;
				case Key.Return: return VirtualKeyCode.RETURN;
				case Key.Pause: return VirtualKeyCode.PAUSE;
				case Key.Print: return VirtualKeyCode.PRINT;

				case Key.PlayPause: return VirtualKeyCode.MEDIA_PLAY_PAUSE;
				case Key.NextTrack: return VirtualKeyCode.MEDIA_NEXT_TRACK;
				case Key.PreviousTrack: return VirtualKeyCode.MEDIA_PREV_TRACK;
				case Key.DecreaseVolume: return VirtualKeyCode.VOLUME_DOWN;
				case Key.IncreaseVolume: return VirtualKeyCode.VOLUME_UP;
				case Key.MuteVolume: return VirtualKeyCode.VOLUME_MUTE;

				default:
					return null;
			}
		}
	}
}