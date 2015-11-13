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
	private TaskList taskList;
	
	public bool plus = true;
	public bool check = false;
	public bool delete = false;

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
		if (task != null && !plus && !check) {
			if(optionsPanel.activeSelf){
				taskList.CloseOptionsForTask(task.id);
			}else{
				taskList.OpenOptionsForTask(task.id);
			}
		}
	}

	public void ShiftPosition(float amount){
		Vector3 newPos = transform.position;
		newPos.y += amount;
		transform.position = newPos;
	}

	public void OpenOptions(){
		optionsPanel.SetActive (true);
	}

	public void CloseOptions(){
		optionsPanel.SetActive(false);
	}
}
