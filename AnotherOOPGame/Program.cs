using System;

namespace AnotherOOPGame
{
	class MainClass
	{
		static public void testHandler()
		{
			Location.worldInit ();

			Creature hero = new Creature ("Hero", 100, 5, Location.world[0,0]);
			Creature ogre = new Creature ("Ogre", 100, 5, Location.world[0,0]);
			ogre.isEnemy = true;
			Creature ork =  new Creature ("Ork",  100, 5, Location.world[0,0]);
			ork.isEnemy = true;
			hero.addToInventory (new Item("Бабулех"		 ));
			hero.addToInventory (new Item ("Сладкий хлеб"));
			ogre.addToInventory(new Item("Штука огра"));

			Location.world [0, 0].addItem (new Item("Штука, лежащая в локации"));

			Console.WriteLine (hero.printStats());
			Console.WriteLine (hero.getTextInventory ());
			while(true)
			{
				ConsoleKeyInfo key;
				key = Console.ReadKey (true);
				switch ( key.Key ) {
				case ConsoleKey.A:
					{
						Console.WriteLine (hero.attack (hero.getTarget ()));
						break;
					}
				case ConsoleKey.S:
					{
						Console.WriteLine (hero.printStats ());
						break;
					}
				case ConsoleKey.M:
					{
						Console.Write ("M+");
						ConsoleKeyInfo _key = Console.ReadKey ();
						Console.WriteLine ();
						if (_key.Key == ConsoleKey.S)
						if (hero.getTarget () != null) {
							Console.WriteLine (hero.getTarget ().printStats ());
						}
						break;
					}
				case ConsoleKey.L:
					{
						Console.WriteLine ("Существа в локации: ");
						Console.WriteLine ("  " + hero.getLocation().returnCreatures ());
						Console.WriteLine ("Вещи в локации: ");
						Console.WriteLine ("  " + hero.getLocation().returnItems ());
						break;
					}
				case ConsoleKey.T:
					{
						Console.WriteLine ("Выберите цель:");
						for (int i = 0; i < hero.getLocation ().creatures.Count; i++) {
							Console.WriteLine ("  " + i + "  " + hero.getLocation ().creatures [i].name + " " + (int)hero.getLocation ().creatures [i].getHp ());
						}
						hero.selectTarget (hero.getLocation ().creatures [Convert.ToInt32 (Console.ReadLine ())]);
						break;
					}
				case ConsoleKey.I:
					{
						Console.WriteLine ("Инвентарь: ");
						for (int i = 0; i < hero.getInventory ().Length; i++) {
							Console.Write ("  " + hero.getInventory () [i].name + "; ");
						}
						break;
					}
				case ConsoleKey.P:
					{
						Console.WriteLine ("Выберите предмет");
						for (int i = 0; i < hero.getLocation ().getItems ().Count; i++) {
							Console.WriteLine ("  " + i + "  " + hero.getLocation ().getItems () [i].name);
						}
						int choice = Convert.ToInt32 (Console.ReadLine ());
						hero.pickUpItem (hero.getLocation ().getItems () [choice]);
						break;
					}
				case ConsoleKey.G:
					{
						Console.WriteLine ("Выберите направление");
						ConsoleKeyInfo choice = Console.ReadKey (true);
						switch (choice.Key) {
						case ConsoleKey.W:
							{
								Console.WriteLine(hero.goToDirection (0, 1));
								break;
							}
						case ConsoleKey.S:
							{
								Console.WriteLine(hero.goToDirection (0, -1));
								break;
							}
						case ConsoleKey.A:
							{
								Console.WriteLine(hero.goToDirection (-1, 0));
								break;
							}
						case ConsoleKey.D:
							{
								Console.WriteLine(hero.goToDirection (1, 0));
								break;
							}
						}
						break;
					}
				}
			}
		}
		public static void Main (string[] args)
		{
			testHandler ();
		}
	}
}
