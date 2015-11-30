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
	public static List<Item> inventory = new List<Item>();
	public static List<Item> envInventory = new List<Item>();

	public static Hashtable tasks = new Hashtable();

	public static void TestInventory(){
		inventory.Add(new Item("Toy"));
		inventory.Add(new Item("Toy"));
		inventory.Add(new Item("Toy"));
	}

	public static void Save(){
		GameData.saveData.monster = new SaveMonster();
		List<SaveTask> saveTasks = new List<SaveTask>();
		// TODO: set properties saveData.monster.prop = GameData.activeMonster.prop;
		foreach(DictionaryEntry entry in tasks){
			Task t = (Task)entry.Value;
			Debug.Log("Order saving: " + t.id);
			SaveTask st = new SaveTask();
			st.difficulty = t.difficulty;
			st.name = t.name;
			st.id = t.id;
			saveTasks.Add(st);
		}
		GameData.saveData.tasks = saveTasks;
		Serializer.Save(saveData);
	}

	public static bool Load(){
		SaveData tempSave = Serializer.Load();
		// populate everything...
		if(tempSave == null) return false;
		Debug.Log("Found save data...");
		saveData = tempSave;
		foreach(SaveTask st in saveData.tasks){
			Debug.Log("Order loading: " + st.id);
			Task t = new Task(st.name, st.id);
			t.difficulty = st.difficulty;
			foreach(SaveTask sst in st.subtasks){
				Task subt = new Task(sst.name, sst.id);
				subt.difficulty = sst.difficulty;
				t.subtasks.Add(subt.name, st);
			}
			tasks.Add(t.name, t);
		}
		return true;
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
