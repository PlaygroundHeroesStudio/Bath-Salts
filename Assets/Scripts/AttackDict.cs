using UnityEngine;
using System.Collections.Generic;

public class AttackDict
{
	Dictionary<string, Attack> Attacks = new Dictionary<string, Attack>();

	public void Add(string Name, float MinRange, float MaxRange)
	{
		if (!Attacks.ContainsKey(Name))
			Attacks.Add(Name, new Attack(Name, MinRange, MaxRange));
	}

	public void Remove(string Name)
	{
		Attacks.Remove(Name);
	}

	public Attack Get(string Name)
	{
		return Attacks[Name];
	}
	
	public Attack GetRandom()
	{
		int Rand = Random.Range(0, Attacks.Count);

		int Count = -1;

		foreach (KeyValuePair<string, Attack> KVP in Attacks)
			if (++Count == Rand)
				return KVP.Value;

		return null;
	}
}

public class Attack
{
	public string Name { get; set; }
	public float MinRange { get; set; }
	public float MaxRange { get; set; }
	
	public Attack(string _Name, float _MinRange, float _MaxRange)
	{
		Name = _Name;
		MinRange = _MinRange;
		MaxRange = _MaxRange;
	}
}