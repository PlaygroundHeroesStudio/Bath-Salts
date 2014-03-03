using UnityEngine;
using System.Collections.Generic;

public static class PlayerManager
{
	public static List<Player> Players { get; private set; }

	public static Player MainPlayer = null;

	static PlayerManager()
	{
		Players = new List<Player>();
	}
}