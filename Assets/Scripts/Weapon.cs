using UnityEngine;
using System.IO;

public class Weapon : Equipment
{
	public Projectile Proj = null;

	public override void Save(BinaryWriter BW)
	{
		base.Save(BW);

		BW.Write(Proj.Damage);
		BW.Write(Proj.ArPen);
		BW.Write(Proj.Force);
		BW.Write(Proj.MaxHitCount);
	}

	public void ResetHits()
	{
		if (Proj != null)
			Proj.ResetHits();
	}
}