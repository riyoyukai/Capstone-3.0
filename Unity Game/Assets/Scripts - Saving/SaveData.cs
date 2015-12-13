using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class SaveData {
	public List<SaveTask> tasks = new List<SaveTask>();
	public List<SaveTask> completedTasks = new List<SaveTask>();
	public List<SaveItem> items = new List<SaveItem>();
	public SaveMonster monster = new SaveMonster();

	public SaveData(){}
}