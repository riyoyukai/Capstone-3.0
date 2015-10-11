using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class Data {

	/**** Save Data ****/
//	string petName;
//	int xp;
	public static int level;
//	int manners;
//	int stuff;
	// enums?
	// TODO: more of this
	/**** End Save Data ****/

	public static void LoadPreferences () {
		if(PlayerPrefs.HasKey("Level")) level = PlayerPrefs.GetInt ("Level");
		else level = 100;
		
		//invertLookY = PlayerPrefs.GetInt ("InvertLookY") == 1;
	}

	public static void SavePreferences(){
		//Settings.Gameplay.invertLook = invertLookY; // NOT PERSISTANT
		
		// that literal string could be a static class, settings.prefs.invertlook is a static string
		// stored in regedit
		//PlayerPrefs.SetInt("InvertLookY", invertLookY ? 1 : 0);
		PlayerPrefs.SetInt("Level", level);
	}

	public static void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream fs = File.Open (Application.persistentDataPath + "/taskmonster.rfg", FileMode.OpenOrCreate);
		
		SaveGame game = new SaveGame ();
		game.hp = 100;
		game.xp = 42;
		game.lvl = 1;
		
		bf.Serialize (fs, game);
		fs.Close ();
	}
	
	public static void Load(){
		string path = Application.persistentDataPath + "/taskmonster.rfg";
		
		if(!File.Exists(path)) return;
		
		FileStream fs = null;
		
		try{
			BinaryFormatter bf = new BinaryFormatter ();
			fs = File.Open (path, FileMode.Open);
			SaveGame game = bf.Deserialize(fs) as SaveGame;
			
			Debug.Log ("hp: " + game.hp);
			Debug.Log ("xp: " + game.xp);
			Debug.Log ("lvl: " + game.lvl);
		}catch(Exception e){
			Debug.Log ("Something went wrong while loading.\nException: " + e.ToString());
		}finally{
			if(fs != null) fs.Close();
		}
	}
}

[Serializable]
class SaveGame{
	public int hp;
	public int xp;
	public int lvl;
}