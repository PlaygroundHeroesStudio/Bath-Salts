using UnityEngine;
using System.Collections.Generic;

public class ItemCollection : TransformObject
{
	public List<Item> Items = new List<Item>();

	public void Spawn(Vector3 SpawnPos)
	{
		position = SpawnPos;

		GO.SetActive(true);
	}

	public void Add(Item ItemPrefab)
	{
		Items.Add((Item)ItemPrefab.Clone());
	}
}