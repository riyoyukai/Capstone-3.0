using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TaskList : MonoBehaviour {
	
	public GameObject taskItemPanelPrefab;
	public GameObject taskPanelGrid;
	private List<TaskPanel> taskPanels = new List<TaskPanel>();

	void Start(){

	}

	public void AddTask(TaskPanel taskPanel){
		Task newTask = new Task(taskPanel.taskNameLabel.text, taskPanels.Count); 
		GameData.tasks.Add(taskPanel.taskNameLabel.text, newTask);
		taskPanel.task = newTask;
		taskPanel.taskNameLabel.text += " ID: " + taskPanel.task.id;

		GameObject newTaskPanel = Instantiate(taskItemPanelPrefab, taskItemPanelPrefab.transform.position, taskItemPanelPrefab.transform.rotation) as GameObject;
		newTaskPanel.transform.SetParent(taskPanelGrid.transform, false);
		newTaskPanel.transform.SetSiblingIndex(0);
		taskPanels.Add (taskPanel);
	}

	public void RemoveTask(TaskPanel taskPanel){
		ShiftIDs(taskPanel.task.id);
		GameData.tasks.Remove(taskPanel.task.name);
		taskPanels.Remove(taskPanel);
		Destroy(taskPanel.gameObject);
	}

	private void ShiftIDs(int id){
		for(int i = id; i < taskPanels.Count; i++){
			taskPanels[i].task.id--;
			taskPanels[i].taskNameLabel.text += " ID: " + taskPanels[i].task.id;
		}
	}

	private void MoveInHierarchy(GameObject obj, int delta){
		int index = obj.transform.GetSiblingIndex();
		obj.transform.SetSiblingIndex(index + delta);
	}

	public void OpenOptionsForTask(int id){	
		for(int i = 0; i < taskPanels.Count; i++){
			TaskPanel tp = taskPanels[i];
			if(i == id){
				tp.OpenOptions();
			}else{
				tp.CloseOptions();
			}
		}
	}
}
