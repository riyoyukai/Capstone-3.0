using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	private float delay = 1;
	public Popup taskCompletePopup;
	private bool popupOpenLast;

	void Awake(){

	}

	void Update(){
		if(delay > 0) delay -= Time.deltaTime;
		if(delay < 0 && GameData.completedTasks.Count > 0){
			taskCompletePopup.gameObject.SetActive(true);
			taskCompletePopup.QueueTask(0);
			delay = 0;
		}

		bool popupOpen = taskCompletePopup.gameObject.activeSelf;
		if(popupOpenLast && !popupOpen){
			delay = 1;
		}

		popupOpenLast = popupOpen;
	}
}
