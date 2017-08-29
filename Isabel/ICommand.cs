using System;

namespace Isabel
{
	public interface ICommand
		: ICloneable
	{
		void Execute();
	}
}