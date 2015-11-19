using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SubtaskPanel : MonoBehaviour {
	
	public Button actionButton;
	public Text actionButtonText;
	public InputField inputField;
	public Text subtaskNameLabel;
	public GameObject optionsPanel;

	public Task task;
	
	// used to add and remove task to the list, and to toggle options
	public TaskPanel parentTask;
	
	// these bools keep track of what stage of task creation you're in
	public bool plus = true;
	public bool check = false;
	public bool delete = false;

	void Start(){
		parentTask = transform.parent.parent.parent.GetComponent<TaskPanel>();
		inputField.gameObject.SetActive (true);
		inputField.Select();
		actionButtonText.text = ((char)0x2713).ToString ();
		plus = false;
		check = true;
		delete = false;
	}
	
	// user tapped + or check.
	public void ActionButtonPressed(){
		if(check){
			// TODO: 
			// tasks with the same name
			if(inputField.text.Trim() == "") return;
			subtaskNameLabel.text = inputField.text;
			inputField.gameObject.SetActive (false);
			subtaskNameLabel.gameObject.SetActive(true);
			actionButtonText.text = "x";
			plus = false;
			check = false;
			delete = true;
			parentTask.AddSubtask(this);
		}
		else if (delete) {
			parentTask.RemoveSubtask(this);
		}
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
	}
	
	public void CloseOptions(){
		optionsPanel.SetActive(false);
	}
}
