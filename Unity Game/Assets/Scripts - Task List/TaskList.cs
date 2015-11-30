using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TaskList : MonoBehaviour {
	
	public GameObject taskItemPanelPrefab;
	public GameObject savedTaskItemPanelPrefab;
	public GameObject taskPanelGrid;
	private List<TaskPanel> taskPanels = new List<TaskPanel>();
	public OptionsMenu optionsMenu;

	void Start(){
		if(GameData.Load()){
			print ("loaded data");
			foreach(DictionaryEntry entry in GameData.tasks){
				Task t = (Task)entry.Value;
				print ("Looking at task \"" + t.name + "\"");
				GameObject newTaskObject = Instantiate(
					savedTaskItemPanelPrefab,
					savedTaskItemPanelPrefab.transform.position,
					savedTaskItemPanelPrefab.transform.rotation
					) as GameObject;
				TaskPanel newTask = newTaskObject.GetComponent<TaskPanel>();

				newTask.plus = false;
				newTask.check = false;
				newTask.delete = true;
				newTask.task = t;
				newTask.taskNameLabel.text = t.name;
				
				
				newTaskObject.transform.SetParent(taskPanelGrid.transform, false);
				newTaskObject.transform.SetSiblingIndex(1);
				taskPanels.Add (newTask);
			}
		}else{
			print ("didn't load data");
		}
	}

	public void AddTask(TaskPanel taskPanel){
		Task newTask = new Task(taskPanel.taskNameLabel.text, taskPanels.Count); 
		GameData.tasks.Add(taskPanel.taskNameLabel.text, newTask);
		taskPanel.task = newTask;

		GameObject newTaskPanel = Instantiate(
			taskItemPanelPrefab,
			taskItemPanelPrefab.transform.position,
			taskItemPanelPrefab.transform.rotation
		) as GameObject;
		newTaskPanel.transform.SetParent(taskPanelGrid.transform, false);
		newTaskPanel.transform.SetSiblingIndex(0);
		taskPanels.Add (taskPanel);

		GameData.Save();
	}

	public void RemoveTask(TaskPanel taskPanel){
		ShiftIDs(taskPanel.task.id);
		GameData.tasks.Remove(taskPanel.task.name);

		taskPanels.Remove(taskPanel);
		Destroy(taskPanel.gameObject);

		GameData.Save();
	}

	private void ShiftIDs(int id){
		for(int i = id; i < taskPanels.Count; i++){
			taskPanels[i].task.id--;
		}
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
