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
	}

	// user tapped + or check.
	public void ActionButtonPressed(){
//		print ("PRESSING BUTTON.");
//		print ("Plus: " + plus);
//		print ("Check: " + check);
//		print ("Delete: " + delete);

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
			subtaskNameLabel.text = inputField.text;
			inputField.gameObject.SetActive (false);
			subtaskNameLabel.gameObject.SetActive(true);
			actionButtonText.text = "x";
			plus = false;
			check = false;
			delete = true;
			parentTask.AddSubtask(this);
			print ("Finish making task");
		}
		else if (delete) {
			parentTask.RemoveSubtask(this);
			print ("TODO: fix this because it's triggering twice");
			// NICK: why is this happening at all? nothing should be calling this function!
		}

//		print ("BUTTON DONE CURRENT STATE:");
//		print ("Plus: " + plus);
//		print ("Check: " + check);
//		print ("Delete: " + delete);
//		print ("------------------");
	}
}
