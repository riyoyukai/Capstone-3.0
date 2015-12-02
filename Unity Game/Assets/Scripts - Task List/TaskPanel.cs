using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

// this gets connected to the Panel that holds the task item and options panel
public class TaskPanel : MonoBehaviour {

	// these buttons keep track of what stage of task creation you're in
	public Button addButton;
	public Button checkButton;
	public Button deleteButton;

	public InputField inputField;
	public Text taskNameLabel;
	public GameObject optionsPanel;

	// used to keep track of subtasks
	public GameObject subtaskListPanel;
	public GameObject subtaskPanelGrid;
	public GameObject subtaskItemPanelPrefab;
	public List<SubtaskPanel> subtaskPanels = new List<SubtaskPanel>();
	GameObject newSubtaskPanel;

	// used to add and remove task to the list, and to toggle options
	private TaskList taskList;
	// used to access edit options
	public OptionsMenu optionsMenu;

	// task class that is used in save data
	public Task task;

	public void Start(){
		taskList = GameObject.FindGameObjectWithTag("TaskList").GetComponent<TaskList>();
		if(optionsMenu == null){
			optionsMenu = taskList.optionsMenu;
		}
	}
	
	public void AddButton(){
		inputField.gameObject.SetActive (true);
		inputField.Select();
		addButton.gameObject.SetActive(false);
		checkButton.gameObject.SetActive(true);
	}
	
	public void CheckButton(){
		// TODO: 
		// tasks with the same name?
		if(inputField.text.Trim() == "") return;
		taskNameLabel.text = inputField.text;
		inputField.gameObject.SetActive (false);
		taskNameLabel.gameObject.SetActive(true);
		taskList.AddTask(this);
		checkButton.gameObject.SetActive(false);
		deleteButton.gameObject.SetActive(true);
	}
	
	public void DeleteButton(){
		taskList.RemoveTask(this);	
	}
	
	// called by event trigger on Main Task Panel
	public void ToggleOptions(){
		if (task != null) {
			if(optionsPanel.activeSelf){
				CloseOptions();
			}else{
				taskList.OpenOptionsForTask(task.id);
			}
		}
	}

	public void OpenOptions(){
		optionsPanel.SetActive (true);
		subtaskListPanel.SetActive(true);
	}

	public void CloseOptions(){
		optionsPanel.SetActive(false);
		subtaskListPanel.SetActive(false);
	}

	public void EditTask(){
		print ("Task: " + this.task);
		print ("Options: " + optionsMenu);
		optionsMenu.Open(this.task);
	}

	public void CompleteTask(){
		taskList.RemoveTask(this);
		Task t = task;
		GameData.tasks.Remove(t);
		//TODO: add completed task to queue (make a new list in gamedata)
		//save and load completed task queue
		//add checks on the gamecontroller for complete tasks
	}

	public void CreateSubtask(){
		if(newSubtaskPanel != null) return;

		newSubtaskPanel = Instantiate(
			subtaskItemPanelPrefab,
			subtaskItemPanelPrefab.transform.position,
			subtaskItemPanelPrefab.transform.rotation
		) as GameObject;
		newSubtaskPanel.transform.SetParent(subtaskPanelGrid.transform, false);
//		newSubtaskPanel.transform.SetSiblingIndex(0);
	}
	
	public void AddSubtask(SubtaskPanel subtaskPanel){
		Task newTask = new Task(subtaskPanel.subtaskNameLabel.text, subtaskPanels.Count); 
		Task parentTask = (Task) GameData.tasks[subtaskPanel.parentTask.task.id];
		parentTask.subtasks.Add(newTask);
		subtaskPanel.task = newTask;
		newTask.parentTask = task;

		subtaskPanels.Add (subtaskPanel);
		newSubtaskPanel = null;

		GameData.Save();
	}
	
	public void RemoveSubtask(SubtaskPanel subtaskPanel){
		ShiftIDs(subtaskPanel.task.id);
		Task parentTask = (Task) GameData.tasks[subtaskPanel.parentTask.task.id];
		parentTask.subtasks.Remove(subtaskPanel.task);
		subtaskPanels.Remove(subtaskPanel);
		Destroy(subtaskPanel.gameObject);

		GameData.Save();
	}
	
	private void ShiftIDs(int id){
		for(int i = id; i < subtaskPanels.Count; i++){
			subtaskPanels[i].task.id--;
			subtaskPanels[i].subtaskNameLabel.text += " ID: " + subtaskPanels[i].task.id;
		}
	}
}
