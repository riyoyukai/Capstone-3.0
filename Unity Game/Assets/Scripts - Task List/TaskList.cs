using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TaskList : MonoBehaviour {
	
	public GameObject taskItemPanelPrefab;
	public GameObject savedTaskItemPanelPrefab;
	public GameObject savedSubtaskItemPanelPrefab;
	public GameObject taskPanelGrid;
	private List<TaskPanel> taskPanels = new List<TaskPanel>();
	public OptionsMenu optionsMenu;

	void Start(){
		if(GameData.tasks.Count > 0){
			print ("loaded data");
			foreach(Task t in GameData.tasks){
				print ("Looking at task \"" + t.name + "\"");
				GameObject newTaskObject = Instantiate(
					savedTaskItemPanelPrefab,
					savedTaskItemPanelPrefab.transform.position,
					savedTaskItemPanelPrefab.transform.rotation
				) as GameObject;
				TaskPanel newTask = newTaskObject.GetComponent<TaskPanel>();

				newTask.addButton.gameObject.SetActive(false);
				newTask.checkButton.gameObject.SetActive(false);
				newTask.deleteButton.gameObject.SetActive(true);

				newTask.task = t;
				newTask.taskNameLabel.text = t.name;
				
				
				newTaskObject.transform.SetParent(taskPanelGrid.transform, false);
				newTaskObject.transform.SetSiblingIndex(1);
				taskPanels.Add (newTask);

				foreach(Task st in t.subtasks){
					GameObject newSubtaskObject = Instantiate(
						savedSubtaskItemPanelPrefab,
						savedSubtaskItemPanelPrefab.transform.position,
						savedSubtaskItemPanelPrefab.transform.rotation
					) as GameObject;
					SubtaskPanel newSubtask = newSubtaskObject.GetComponent<SubtaskPanel>();
					
					newSubtask.checkButton.gameObject.SetActive(false);
					newSubtask.deleteButton.gameObject.SetActive(true);
					newSubtask.task = st;
					newSubtask.parentTask = newTask;
					print ("st.name: " + st.name);
					newSubtask.subtaskNameLabel.text = st.name;

					newSubtaskObject.transform.SetParent(newTask.subtaskPanelGrid.transform, false);
					newTask.subtaskListPanel.gameObject.SetActive(false);

					newTask.subtaskPanels.Add (newSubtask);
				}
			}
		}else{
			print ("didn't load data");
		}
	}

	public void AddTask(TaskPanel taskPanel){
		Task newTask = new Task(taskPanel.taskNameLabel.text, taskPanels.Count); 
		GameData.tasks.Add(newTask);
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
		GameData.tasks.Remove(taskPanel.task);

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
