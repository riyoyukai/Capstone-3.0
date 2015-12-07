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
	
	public void GSM_LoadAndSwitchToPlay(){
		Application.LoadLevel ("Play");
		GameData.Load();
	}
	
	public void GSM_SwitchToPetProfile(){
		Application.LoadLevel ("PetProfile");
		GameData.Save();
	}
	
	public void GSM_SwitchToTaskList(){
		Application.LoadLevel ("TaskList");
		GameData.Save();
	}
	
	public void GSM_Quit(){
		Application.Quit();
	}

	public void GSM_DeleteSave(){
		Serializer.DeleteSave();
	}
}
