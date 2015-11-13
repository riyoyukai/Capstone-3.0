using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TaskList : MonoBehaviour {
	
	public GameObject taskItemPanelPrefab;
	public List<TaskPanel> taskPanels;

	void Start(){
		GameObject newTask = Instantiate(taskItemPanelPrefab, taskItemPanelPrefab.transform.position, taskItemPanelPrefab.transform.rotation) as GameObject;
		newTask.transform.SetParent(this.gameObject.transform, false);
		taskPanels.Add (newTask.GetComponent<TaskPanel>());
	}

	public void AddTask(TaskPanel taskPanel){
		Task newTask = new Task(taskPanel.taskNameLabel.text, taskPanels.Count - 1); // accounts for blank task
		GameData.tasks.Add(taskPanel.taskNameLabel.text, newTask);
		taskPanel.task = newTask;

		GameObject newTaskPanel = Instantiate(taskItemPanelPrefab, LastTaskPosition(), taskItemPanelPrefab.transform.rotation) as GameObject;
		newTaskPanel.transform.SetParent(this.gameObject.transform, false);
		newTaskPanel.transform.position = LastTaskPosition();
		taskPanels.Add (newTaskPanel.GetComponent<TaskPanel>());
		// TODO: delete this print("SIZE: " + newTaskPanel.GetComponent<RectTransform>().rect.size);
	}

	public void RemoveTask(TaskPanel taskPanel){
		// get y height of panel, AND options, if options are open
		// get task id
		// shift positions with that id and y height
		// remove from taskPanels by id
		// remove from gamedata tasks by name
		if(taskPanel.optionsPanel.gameObject.activeSelf){
			CloseOptionsForTask(taskPanel.task.id);
		}
		ShiftPositions(taskPanel.task.id);
		ShiftIDs(taskPanel.task.id);
		taskPanels.Remove(taskPanel);
		GameData.tasks.Remove(taskPanel.task.name);
		Destroy(taskPanel.gameObject);
	}

	private Vector3 LastTaskPosition(){
		Vector3 newPos = taskPanels[taskPanels.Count-1].gameObject.transform.position;
		newPos.y -= taskPanels[taskPanels.Count-1].GetComponent<RectTransform>().rect.size.y;
		return newPos;
	}

	public void ShiftPositions(int id){
		for(int i = id + 1; i < taskPanels.Count; i++){
			float amount = taskPanels[id].GetComponent<RectTransform>().rect.size.y;
			
			Vector3 newPos = taskPanels[i].transform.position;
			newPos.y += amount;
			taskPanels[i].transform.position = newPos;
		}
	}

	private void ShiftIDs(int id){
		for(int i = id + 1; i < taskPanels.Count - 1; i++){ // count - 1 to skip blank task
			taskPanels[i].task.id--;
		}
	}

	public void OpenOptionsForTask(int id){	
		for(int i = 0; i < taskPanels.Count; i++){
			TaskPanel tp = taskPanels[i];
			if(i == id){
				tp.OpenOptions();
			}else if(tp.optionsPanel.activeSelf){
				// if we find one that's already open, close it and shift everything up
				CloseOptionsForTask(i);
			}
			if(i > id){
				// push down
				float amount = tp.optionsPanel.GetComponent<RectTransform>().rect.size.y * -1;
				
				Vector3 newPos = tp.transform.position;
				newPos.y += amount;
				tp.transform.position = newPos;
			}
		}
	}

	public void CloseOptionsForTask(int id){
		for(int i = 0; i < taskPanels.Count; i++){
			TaskPanel tp = taskPanels[i];
			if(i == id){
				tp.CloseOptions();
			}
			if(i > id){
				// push up
				float amount = tp.optionsPanel.GetComponent<RectTransform>().rect.size.y;
				
				Vector3 newPos = tp.transform.position;
				newPos.y += amount;
				tp.transform.position = newPos;
			}
		}
	}
}
