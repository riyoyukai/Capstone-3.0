using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour {
	
	public void GSM_SwitchToTitle(){
		Application.LoadLevel ("Title");
	}
	
	public void GSM_SwitchToOptions(){
		Application.LoadLevel ("Options");
	}
	
	public void GSM_SwitchToCredits(){
		Application.LoadLevel ("Credits");
	}
	
	public void GSM_SwitchToPlay(){
		Application.LoadLevel ("Play");
	}
	
	public void GSM_SwitchToPetProfile(){
		Application.LoadLevel ("PetProfile");
	}
	
	public void GSM_SwitchToTaskList(){
		Application.LoadLevel ("TaskList");
	}
}
