using System;

public interface ISaveable
{
	object CaptureState();
	void RestoreState(object obj);
	Type Type { get; }
}
