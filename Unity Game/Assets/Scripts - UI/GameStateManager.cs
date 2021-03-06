﻿using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour {
	
	public void GSM_SwitchToTitle(){
		Application.LoadLevel ("Title");
	}
	
	public void GSM_SwitchToCredits(){
		Application.LoadLevel ("Credits");
	}
	
	public void GSM_SwitchToPlay(){
		Application.LoadLevel ("Play");
		GameData.onTitleScreen = false;
	}
	
	public void GSM_LoadAndSwitchToPlay(){
		GameData.Load();
		Application.LoadLevel ("Play");
	}
	
	public void GSM_SwitchToPetProfile(){
		GameData.Save();
		Application.LoadLevel ("PetProfile");
	}
	
	public void GSM_SwitchToTaskList(){
		GameData.Save();
		Application.LoadLevel ("TaskList");
	}
	
	public void GSM_Quit(){
		Application.Quit();
	}

	public void GSM_DeleteSave(){
		Serializer.DeleteSave();
	}

	void OnApplicationQuit() {
		if(!GameData.onTitleScreen){
			Debug.Log("Application ending");
			GameData.Save();
		}
	}
}
