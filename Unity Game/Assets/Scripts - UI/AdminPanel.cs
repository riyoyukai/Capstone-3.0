using UnityEngine;
using System.Collections;

public class AdminPanel : MonoBehaviour {

	public GameObject hatchPopup;
	public Popup taskCompletePopup;

	public void E_Toggle(){
		this.gameObject.SetActive(!this.gameObject.activeSelf);
	}

	public void E_MakeHungry(){
		GameData.activeMonster.hungry = false;
		GameData.activeMonster.hunger = GameData.activeMonster.hungerThreshold;
	}

	public void E_Hatch(){
		hatchPopup.SetActive(true);
	}

	public void E_SpawnItem(){
		taskCompletePopup.E_CompleteTask();
	}
}
