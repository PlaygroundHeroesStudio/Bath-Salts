using UnityEngine;

public abstract class Interactable : TransformObject
{
	public abstract void OnInteract(Entity E);
}