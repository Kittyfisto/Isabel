using System;
using System.Collections.Generic;
using Isabel.Commands;

namespace Isabel
{
	public interface IKeyboardInputEngine
		: IDisposable
	{
		/// <summary>
		///     Simulates a key press of the given keys.
		///     All keys are first pressed (in order first to last), held for the given amount of time
		///     and then released.
		/// </summary>
		/// <remarks>
		///     The given time is the minimum time the keys will be held.
		/// </remarks>
		/// <remarks>
		///     This method merely enqueues the given gestures and returns immediately.
		///     All gestures are executed in the order they are enqueued in.
		/// </remarks>
		void Enqueue(IReadOnlyList<KeyGesture> gestures);
	}
}