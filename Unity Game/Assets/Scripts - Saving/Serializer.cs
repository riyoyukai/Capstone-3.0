﻿using UnityEngine;
using System.Collections;

using System; // Needed for [Serializable]
using System.Runtime.Serialization.Formatters.Binary; // Needed for BinaryFormatter
using System.IO; // Needed for FileStream and File

public static class Serializer {

	public static string path = Application.persistentDataPath + "/taskmonster.rfg";
	
	public static void Save(SaveData data){	
//		Debug.Log(path);
		
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream fs = File.Open (path, FileMode.OpenOrCreate);
		bf.Serialize (fs, data);
		fs.Close ();
	}
	
	public static SaveData Load(){
//		string path = Application.persistentDataPath + "/" + filename;
		if (File.Exists (path)) {
			try{
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream fs = File.Open (path, FileMode.Open);
				SaveData data = (SaveData) bf.Deserialize (fs);
				fs.Close ();
				return data;
			}catch(Exception e){
				Debug.Log ("Something went wrong while loading.\nException: " + e.ToString());
			}
		}
		return null;
	}

	public static void DeleteSave(){
		File.Delete(path);
	}
}
