using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TaskList : MonoBehaviour {
	
	public GameObject taskItemPanelPrefab;
	public List<TaskItem> tasks;

	void Start(){
		GameObject newTask = Instantiate(taskItemPanelPrefab, taskItemPanelPrefab.transform.position, taskItemPanelPrefab.transform.rotation) as GameObject;
		newTask.transform.SetParent(this.gameObject.transform, false);
		tasks.Add (newTask.GetComponent<TaskItem>());
	}

	public void AddTask(TaskItem task){
		GameData.tasks.Add (task.text.text, new Task(task.text.text));
		GameObject newTask = Instantiate(taskItemPanelPrefab, LastTaskPosition(), taskItemPanelPrefab.transform.rotation) as GameObject;
		newTask.transform.SetParent(this.gameObject.transform, false);
		newTask.transform.position = LastTaskPosition();
		tasks.Add (newTask.GetComponent<TaskItem>());
		print("SIZE: " + newTask.GetComponent<RectTransform>().rect.size);
	}

	private Vector3 LastTaskPosition(){
		Vector3 newPos = tasks[tasks.Count-1].gameObject.transform.position;
		newPos.y -= tasks[tasks.Count-1].GetComponent<RectTransform>().rect.size.y;
		return newPos;
	}
}
