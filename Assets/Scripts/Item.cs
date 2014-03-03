using UnityEngine;
using System.IO;

public class Item : TransformObject
{
	public Entity Owner = null;

	public string Name = "";

	public bool Consumable = false;

	public int Stack = 1;

	public int Value = 0;

	public bool InWorld { get { return GO.activeSelf; } }

	public virtual bool CanUse(Entity Owner)
	{
		return Owner.HasControl;
	}

	public virtual bool Use(Entity Owner)
	{
		if (!CanUse(Owner))
			return false;

		Stack--;
		return true;
	}

	public virtual void Save(BinaryWriter BW)
	{
		BW.Write(Name);
		BW.Write(Consumable);
		BW.Write(Stack);
		BW.Write(Value);
	}

	public Item Clone()
	{
		return (Item)MemberwiseClone();
	}
}