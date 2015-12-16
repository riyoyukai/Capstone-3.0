using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Task {

	public string name;
	public int id;
	public System.DateTime deadline;
	public System.DateTime reminderDate;
	public int difficulty;
	public Task parentTask;
	
	public List<Task> subtasks = new List<Task>();

	public Task(string pName, int pId){
		name = pName;
		id = pId;
	}

	public void ChangeReminderDate(System.DateTime pReminderDate){
		reminderDate = pReminderDate;
	}
}
