using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(ItemDefinitions))]
public class ItemDefinitionsEditor : Editor
{
	int Count = 0;
	
	List<bool> Toggles = new List<bool>();

	List<string> Removals = new List<string>();

	KeyValuePair<string, Item> NameChanger;
	
	public override void OnInspectorGUI()
	{
		EditorGUILayout.BeginVertical();
		
		ItemDefinitions ID = (ItemDefinitions)target;
		
		if (ID.ItemDefs == null)
			ID.ItemDefs = new Dictionary<string, Item>();
		
		Count = EditorGUILayout.IntField("Count", Count);
		
		if (Count < 0)
			Count = 0;
		
		for (int I = 0; I < Count; I++)
		{
			if (I >= Toggles.Count)
				Toggles.Add(false);
			else
				Toggles[I] = false;
		}

		int DefCount = 0;

		foreach (KeyValuePair<string, Item> KVP in ID.ItemDefs)
		{
			if (DefCount++ >= Count)
			{
				Removals.Add(KVP.Key);
				Toggles[DefCount] = false;
				continue;
			}

			Toggles[DefCount] = EditorGUILayout.Foldout(Toggles[DefCount], "Element " + DefCount);
			
			if (Toggles[DefCount])
			{
				Item ICI = KVP.Value;
				
				string ItemName = ICI.Name;

				Toggles[DefCount] = EditorGUILayout.Foldout(Toggles[DefCount], "Element " + DefCount);

				ICI.Name = EditorGUILayout.TextField("Name", ICI.Name);
				ICI.Consumable = EditorGUILayout.Toggle("Consumable", ICI.Consumable);
				ICI.Stack = EditorGUILayout.IntField("Stack", ICI.Stack);
				ICI.Value = EditorGUILayout.IntField("Value", ICI.Value);

				if (ID.ItemDefs.ContainsKey(ICI.Name))
					ICI.Name = ItemName;
				else
					NameChanger = KVP;
			}
		}

		foreach (string S in Removals)
			ID.ItemDefs.Remove(S);

		Removals.Clear();

		if (NameChanger.Value != null)
		{
			ID.ItemDefs.Remove(NameChanger.Key);
			ID.ItemDefs.Add(NameChanger.Key, NameChanger.Value);
		}

		NameChanger = new KeyValuePair<string, Item>();

		EditorGUILayout.EndVertical();
	}
}