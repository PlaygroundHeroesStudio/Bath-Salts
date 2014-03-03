using UnityEngine;
using System.IO;

public class Equipment : Item
{
	public int MaxDurability = 100;

	public float Durability { get; set; }

	public Transform Bind = null;

	public Equipment()
	{
		Durability = MaxDurability;
	}

	public override void Save(BinaryWriter BW)
	{
		base.Save(BW);

		BW.Write(Durability);
		BW.Write(MaxDurability);
	}
}