using UnityEngine;
using System.Collections.Generic;

public class Projectile : TransformObject
{
	public int Damage = 0;
	public int ArPen = 0;
	public int Force = 0;
	public int MaxHitCount = 1;

	int HitCount = 0;

	public Entity Owner { get; set; }

	public List<Entity> Hits { get; private set; }

	protected override void Awake()
	{
		base.Awake();

		Hits = new List<Entity>();
	}

	public virtual void OnHit(Entity E)
	{
		Hits.Add(E);

		if (MaxHitCount > 0)
		{
			HitCount++;

			if (HitCount >= MaxHitCount)
				GO.SetActive(false);
		}
	}

	public void ResetHits()
	{
		HitCount = 0;
		Hits.Clear();
	}
}