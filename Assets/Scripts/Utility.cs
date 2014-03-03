using UnityEngine;

public class AnimEvent : Object
{
	private System.Action<object> AnimAction = null;

	public AnimEvent(System.Action<object> _AnimAction)
	{
		AnimAction = _AnimAction;
	}

	public void RunEvent(object Param)
	{
		AnimAction(Param);
	}
}