using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GameData {

	/**** Save Data ****/
	public static SaveData saveData = new SaveData();
	//	string petName;
	//	int xp;
	public static int volume;
	// enums?
	// TODO: more of this
	/**** End Save Data ****/


	public static Monster activeMonster = new Monster();
//	public static List<Item> inventory = new List<Item>();
//	public static List<Item> envInventory = new List<Item>();

	public static List<Item> items = new List<Item>();
	public static List<Task> tasks = new List<Task>();
	public static List<Task> completedTasks = new List<Task>();
	public static bool onTitleScreen = true;

//	public static void TestInventory(){
//		inventory.Add(new Item("Toy"));
//		inventory.Add(new Item("Toy"));
//		inventory.Add(new Item("Toy"));
//	}

	public static void Save(){
		/***** Monster */
		// TODO: set properties saveData.monster.prop = GameData.activeMonster.prop;
		SaveMonster savemon = new SaveMonster();
		if(!activeMonster.hatched){
			savemon.hatchTimeLeft = (int)activeMonster.hatchTime;
		}
		savemon.name = activeMonster.name;
		savemon.hatched = activeMonster.hatched;
		savemon.birthday = activeMonster.birthday;
		savemon.hunger = activeMonster.hunger;

		if(activeMonster.monsterController != null){
			Vector3 mpos = activeMonster.position;
			savemon.x = mpos.x;
			savemon.y = mpos.y;
			savemon.z = mpos.z;
		}

		saveData.monster = savemon;

		/***** Items */
		List<SaveItem> saveItems = new List<SaveItem>();
		foreach(Item item in items){
			SaveItem si = new SaveItem();
			Vector3 pos = item.position;
			si.x = pos.x;
			si.y = pos.y;
			si.z = pos.z;
			si.type = (int)item.type;
			si.foodType = item.foodType;
			saveItems.Add (si);
		}
		saveData.items = saveItems;

		/***** Tasks */
		List<SaveTask> saveTasks = new List<SaveTask>();
		foreach(Task t in tasks){
			SaveTask st = new SaveTask(t.name, t.id);
			st.difficulty = t.difficulty;
			foreach(Task subt in t.subtasks){
				SaveTask sst = new SaveTask(subt.name, subt.id);
				sst.difficulty = subt.difficulty;
				st.subtasks.Add(sst);
			}
			saveTasks.Add(st);
		}
		saveData.tasks = saveTasks;

		/***** Completed Tasks */
		List<SaveTask> cTasks = new List<SaveTask>();
		foreach(Task c in completedTasks){
			SaveTask st = new SaveTask(c.name, c.id);
			st.difficulty = c.difficulty;
			cTasks.Add(st);
			// TODO: parent task somehow? to display what the task was a child of?
		}
		saveData.completedTasks = cTasks;

		Serializer.Save(saveData);
	}

	public static void Load(){
		SaveData tempSave = Serializer.Load();
		// populate everything...
		if(tempSave == null) return;

		Debug.Log("Found save data...");
		saveData = tempSave;

		/***** Monster */
		activeMonster = new Monster();
		if(!saveData.monster.hatched){
			activeMonster.hatchTime = saveData.monster.hatchTimeLeft;
			Debug.Log("GameData: I will hatch in " + activeMonster.hatchTime + " minutes!");
		}
		activeMonster.name = saveData.monster.name;
		activeMonster.hatched = saveData.monster.hatched;
		activeMonster.birthday = saveData.monster.birthday;
		activeMonster.hunger = saveData.monster.hunger;

		if(saveData.monster.x != null){
			Vector3 mpos = Vector3.zero;
			
			mpos.x = saveData.monster.x;
			mpos.y = saveData.monster.y;
			mpos.z = saveData.monster.z;

			activeMonster.position = mpos;
		}

		/***** Items */
		foreach(SaveItem si in saveData.items){
			Item item = new Item("Food");
			item.position.x = si.x;
			item.position.y = si.y;
			item.position.z = si.z;
			item.type = (ItemType)si.type;
			item.foodType = si.foodType;
			items.Add(item);
		}

		/***** Tasks */
		foreach(SaveTask st in saveData.tasks){
			Task t = new Task(st.name, st.id);
			t.difficulty = st.difficulty;
			foreach(SaveTask sst in st.subtasks){
				Task subt = new Task(sst.name, sst.id);
				subt.difficulty = sst.difficulty;
				t.subtasks.Add(subt);
			}
			tasks.Add(t);
		}

		/***** Completed Tasks */
		foreach(SaveTask ct in saveData.completedTasks){
			Task t = new Task(ct.name, ct.id);
			// TODO: parent task somehow? to display what the task was a child of?
			completedTasks.Add(t);
		}
	}

	public static void LoadPreferences () {
		if(PlayerPrefs.HasKey("Volume")) volume = PlayerPrefs.GetInt ("Volume");
		else volume = 100;
		
		//invertLookY = PlayerPrefs.GetInt ("InvertLookY") == 1;
	}
	
	public static void SavePreferences(){
		//Settings.Gameplay.invertLook = invertLookY; // NOT PERSISTENT
		
		// that literal string "Volume" could be a static class, settings.prefs.invertlook is a static string
		// stored in regedit
		//PlayerPrefs.SetInt("InvertLookY", invertLookY ? 1 : 0);
		PlayerPrefs.SetInt("Volume", volume);
	}
}
