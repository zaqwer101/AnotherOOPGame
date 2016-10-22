using System;
using System.Collections.Generic;

namespace AnotherOOPGame
{
	public class Creature : GameObject
	{
		public bool isEnemy = false;
		Location location;   //Ссылка на текущую локацию персонажа 
		List<Item> inventory;
		Creature target;
		public string name;
		float hp, maxhp, damage, armor;
		int lvl, exp, exp_to_lvl;
		public Creature (string name, float hp, float damage, Location location)
		{
			this.target = null;
			this.lvl = 1;
			this.exp = 0;
			this.exp_to_lvl = 100;
			this.inventory = new List<Item> ();
			this.armor = 0;
			this.damage = damage;
			this.maxhp = hp;
			this.hp = hp;
			this.name = name;
			this.location = location;			//Стартовая локация
			this.location.addCreature (this);
		}
		public Creature getTarget()
		{
			return target;
		}
		public void selectTarget(Creature creature)
		{
			this.target = creature;
		}
		public Location getLocation()
		{
			return location;
		}
		public void goToLocation(Location location)
		{
			this.location.removeCreature (this);
			location.addCreature (this);
			this.location = location;
		}

		public string goToDirection(int x_direction, int y_direction)
		{
			if (this.location.x + x_direction >= 0 && this.location.y + y_direction >= 0 &&
				(this.location.x + x_direction) < Location.size_x && (this.location.y + y_direction) < Location.size_y) 
			{
				goToLocation (Location.world [this.location.x + x_direction, this.location.y + y_direction]);
				return this.name + " перешел в локацию [" + this.location.x + ", " + this.location.y + "]";
			}
			else
				return "Не удалось перейти в локацию";
			}

		public Item[] getInventory()
		{
			return this.inventory.ToArray ();
		}

		public string getTextInventory()
		{
			string _inventory = "";
			for (int i = 0; i < this.inventory.Count; i++) {
				_inventory += this.inventory[i].name + ":" + this.inventory[i].id + "\n";
			}

			return _inventory;
		}

		public float getHp()
		{
			return this.hp;
		}

		public void addToInventory(Item item)
		{
			this.inventory.Add (item);
		}

		public void removeFromInventory(Item item)
		{
			this.inventory.Remove (item);
		}

		public void takeDamage(float damage)
		{
			this.hp -= damage - damage * this.armor;
		}

		public bool isAlive()
		{
			if (this.hp > 0)
				return true;
			else {
				return false;
			}
		}

		public void Die()
		{
			for (int i = 0; i < inventory.Count; i++)
				location.addItem (inventory [i]);
			this.location.removeCreature (this);
		}

		public string attack(Creature enemy)
		{
			target = enemy;
			if (target != null) {
				if (target.isAlive ()) {
					float _enemy_hp = target.getHp ();  //нужно для расчёта нанесённого урона
					target.takeDamage (this.damage);
					if (!target.isAlive ()) {
						string _enemy_name = target.name;
						this.takeExp ((int)target.maxhp);
						target.Die ();
						target = null;
						return this.name + " убил " + _enemy_name;
					} else
						return this.name + " нанёс " + Convert.ToInt32 (_enemy_hp - target.hp) + " урона по " + target.name;
				}
				else
					return target.name + " уже мёртв";
			} else
				return ("Цель отсутствует");
		}

		public string printStats()
		{ 
			return
			"Name : " + this.name + "\n" +
			"HP   : " + this.hp + "/" + this.maxhp + "\n" + 
			"LVL  : " + this.lvl + "\n" +
			"EXP  : " + this.exp + "/" + this.exp_to_lvl + "\n" +
			"DMG  : " + this.damage + "\n" +
			"ARMOR: " + this.armor * 100 + "%" + "\n" +
			"POS  : " + this.location.x + ", " + this.location.y;
		}

		public void takeExp(int exp)
		{
			this.exp += exp;
			if (this.exp >= this.exp_to_lvl)
				this.lvlUp ();
		}

		public void lvlUp()
		{
			this.exp_to_lvl *= 2;
			this.exp = 0;
			this.lvl++;
			this.damage += 5;
			this.maxhp += 50;
			this.hp = this.maxhp;
			Console.WriteLine (this.name + " поднял свой уровень!");
		}

		public string pickUpItem(Item item)
		{
			if (location.getItems ().Contains (item)) {
				location.removeItem (item);
				addToInventory (item);
				return name + " подобрал " + item.name;
			} else
				return "Предмета не существует в локации";
		}
	}
}

