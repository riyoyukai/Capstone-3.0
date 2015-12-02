using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SubtaskPanel : MonoBehaviour {
	
	
	// these buttons keep track of what stage of task creation you're in
	public Button checkButton;
	public Button deleteButton;

	public InputField inputField;
	public Text subtaskNameLabel;
	public GameObject optionsPanel;

	public Task task;
	
	// used to add and remove task to the list, and to toggle options
	public TaskPanel parentTask;

	void Start(){
		if(inputField != null){
			parentTask = transform.parent.parent.parent.GetComponent<TaskPanel>();
			inputField.gameObject.SetActive (true);
			inputField.Select();
			checkButton.gameObject.SetActive(true);
			deleteButton.gameObject.SetActive(false);
		}else{
			checkButton.gameObject.SetActive(false);
			deleteButton.gameObject.SetActive(true);
		}
	}
	
	public void CheckButton(){
		// TODO: 
		// tasks with the same name?
		if(inputField.text.Trim() == "") return;
		subtaskNameLabel.text = inputField.text;
		inputField.gameObject.SetActive (false);
		subtaskNameLabel.gameObject.SetActive(true);
		parentTask.AddSubtask(this);
		checkButton.gameObject.SetActive(false);
		deleteButton.gameObject.SetActive(true);
	}
	
	public void DeleteButton(){
		parentTask.RemoveSubtask(this);
	}
	
	// called by event trigger on Main Task Panel
	public void ToggleOptions(){
		if (task != null) {
			if(optionsPanel.activeSelf){
				CloseOptions();
			}else{
//				taskList.OpenOptionsForTask(task.id);
				OpenOptions();
			}
		}
	}
	
	public void OpenOptions(){
		optionsPanel.SetActive (true);
		// TODO: call something from parenttask to
		// collapse options for all other subtasks
	}
	
	public void CloseOptions(){
		optionsPanel.SetActive(false);
	}
	
	public void CompleteTask(){
		parentTask.RemoveSubtask(this);
		Task t = task;
		parentTask.task.subtasks.Remove(t);
		//TODO: add completed task to queue (make a new list in gamedata)
		//save and load completed task queue
		//add checks on the gamecontroller for complete tasks
	}
}
