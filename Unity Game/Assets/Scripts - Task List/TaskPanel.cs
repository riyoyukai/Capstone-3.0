using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// this gets connected to the Panel that holds the task item and options panel
public class TaskPanel : MonoBehaviour {

	public Button actionButton;
	public Text actionButtonText;
	public InputField inputField;
	public Text taskNameLabel;
	public GameObject optionsPanel;

	// used to add and remove task to the list, and to toggle options
	private TaskList taskList;

	// these bools keep track of what stage of task creation you're in
	public bool plus = true;
	public bool check = false;
	public bool delete = false;

	// task class that is used in save data
	public Task task;

	public void Start(){
		taskList = GameObject.FindGameObjectWithTag ("TaskList").GetComponent<TaskList>();
	}

	// user tapped + or check.
	public void ActionButtonPressed(){
		if (plus) {
			inputField.gameObject.SetActive (true);
			inputField.Select();
			actionButtonText.text = ((char)0x2713).ToString ();
			plus = false;
			check = true;
			delete = false;
		}
		else if(check){
			// TODO: 
			// tasks with the same name
			if(inputField.text.Trim() == "") return;
			taskNameLabel.text = inputField.text;
			inputField.gameObject.SetActive (false);
			taskNameLabel.gameObject.SetActive(true);
			actionButtonText.text = "x";
			plus = false;
			check = false;
			delete = true;
			taskList.AddTask(this);
		}
		else if (delete) {
			taskList.RemoveTask(this);
		}
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
	}

	public void CloseOptions(){
		optionsPanel.SetActive(false);
	}
}
