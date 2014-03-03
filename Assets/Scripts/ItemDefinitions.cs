using UnityEngine;
using System.Collections.Generic;

public class ItemDefinitions : MonoBehaviour
{
	public static ItemDefinitions Singleton = null;

	public Dictionary<string, Item> ItemDefs = new Dictionary<string, Item>();

	void Awake()
	{
		if (Singleton != null && Singleton != this)
		{
			Destroy(gameObject);
			return;
		}

		Singleton = this;

		DontDestroyOnLoad(gameObject);
	}
}