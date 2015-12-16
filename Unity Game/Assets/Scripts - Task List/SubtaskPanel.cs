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

	// used to access edit options
	public OptionsMenu optionsMenu;

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

		if(optionsMenu == null){
			optionsMenu = parentTask.taskList.optionsMenu;
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
				parentTask.OpenOptionsForSubtask(task.id);
				OpenOptions();
			}
		}
	}
	
	public void EditTask(){
		optionsMenu.Open(null, this);
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
		Task t = task;
		parentTask.task.subtasks.Remove(t);
		GameData.completedTasks.Add(t);
		
		parentTask.RemoveSubtask(this);
	}
}
