using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class SaveData {
	public List<SaveTask> tasks = new List<SaveTask>();
	public SaveMonster monster = new SaveMonster();

	public SaveData(){}
}