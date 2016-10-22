 using System;
using System.Collections.Generic;

namespace AnotherOOPGame
{
	public class GameObject
	{
		public static List<GameObject> gameObjects = new List<GameObject>();
		static int _id = 0;
		public int id;
		public GameObject ()
		{
			gameObjects.Add (this);
			this.id = _id;
			_id++;
		}
	}
}

