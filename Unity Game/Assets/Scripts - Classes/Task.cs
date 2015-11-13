using UnityEngine;
using System.Collections;

public class Task {

	public string name;
	public int id;
	public System.DateTime dueDate;
	public System.DateTime reminderDate;

	public Task(string pName, int pId){
		name = pName;
		id = pId;
	}

	public void ChangeDueDate(System.DateTime pDueDate){
		dueDate = pDueDate;
	}

	public void ChangeReminderDate(System.DateTime pReminderDate){
		reminderDate = pReminderDate;
	}
}
