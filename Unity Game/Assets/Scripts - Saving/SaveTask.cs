using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SaveTask {
	public string name;
	public int id;
//	public System.DateTime dueDate;
//	public System.DateTime reminderDate;
	public int difficulty;
	public List<SaveTask> subtasks = new List<SaveTask>();

	public SaveTask(string name, int id){
		this.name = name;
		this.id = id;
	}
}
