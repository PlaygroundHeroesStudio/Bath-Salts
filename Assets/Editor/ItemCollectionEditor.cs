using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(ItemCollection))]
public class ItemCollectionEditor : Editor
{
	int Count = 0;

	List<bool> Toggles = new List<bool>();

	public override void OnInspectorGUI()
	{
		EditorGUILayout.BeginVertical();

		ItemCollection IC = (ItemCollection)target;

		if (IC.Items == null)
			IC.Items = new List<Item>();

		Count = EditorGUILayout.IntField("Count", Count);

		if (Count < 0)
			Count = 0;

		for (int I = 0; I < Count; I++)
		{
			if (I >= IC.Items.Count)
			{
				IC.Items.Add(new Item());

				if (I >= Toggles.Count)
					Toggles.Add(false);
				else
					Toggles[I] = false;
			}
			else if (IC.Items[I] == null)
				IC.Items[I] = new Item();

			Item ICI = IC.Items[I];

			Toggles[I] = EditorGUILayout.Foldout(Toggles[I], "Element " + I);

			if (Toggles[I])
			{
				ICI.Name = EditorGUILayout.TextField("Type ID", ICI.Name);

				if (ItemDefinitions.Singleton.ItemDefs.ContainsKey(ICI.Name))
				{

				}
			}
		}

		for (int I = Count; I < IC.Items.Count; I++)
		{
			IC.Items[I] = null;
			Toggles[I] = false;
		}

		EditorGUILayout.EndVertical();
	}
}