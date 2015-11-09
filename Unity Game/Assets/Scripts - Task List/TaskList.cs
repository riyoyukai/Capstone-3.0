using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TaskList : MonoBehaviour {
	
	public GameObject taskItemPanelPrefab;
	public List<TaskItem> tasks;

	public void AddTask(TaskItem task){
		tasks.Add(task);
		GameData.tasks.Add (task.text.text, new Task(task.text.text));
		GameObject newTask = Instantiate(taskItemPanelPrefab, LastTaskPosition(), taskItemPanelPrefab.transform.rotation) as GameObject;
		newTask.transform.SetParent(this.gameObject.transform, false);
		newTask.transform.position = LastTaskPosition();
	}

	private Vector3 LastTaskPosition(){
		Vector3 newPos = tasks[tasks.Count-1].gameObject.transform.position;
		//newPos.y *= (tasks.Count + 1);
		return newPos;
	}
}
