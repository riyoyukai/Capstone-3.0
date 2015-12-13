using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Popup : MonoBehaviour {

	public Text hatchName;
	private int taskID;
	public Text taskName;
	public GameObject itemHolder;
	public GameObject itemPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ClosePopup(){
		this.gameObject.SetActive (false);
	}

	public void E_LevelUp(int startXP, int xpToAdd, int level, int xpToNextLevel){

	}

	public void E_Hatch(EggBehavior egg){
		if(hatchName.text.Trim() == "") return;
		egg.Hatch(hatchName.text);
		ClosePopup();
	}

	public void QueueTask(int tID){
		taskID = tID;
		taskName.text = GameData.completedTasks[tID].name;
	}

	public void E_CompleteTask(){
		// TODO: give random position
		// TODO: check if monster is hungry and initiate findfood if so
		GameObject newItem = Instantiate(
			itemPrefab,
			itemPrefab.transform.position,
			itemPrefab.transform.rotation
			) as GameObject;
		newItem.transform.SetParent(itemHolder.transform, false);
		ItemBehavior ib = newItem.GetComponent<ItemBehavior>();
		ib.item = new Item("Food");
		ib.item.itemBehavior = ib;
		GameData.items.Add(ib.item);

		GameData.completedTasks.RemoveAt(taskID);
		ClosePopup();
		GameData.Save();
	}
}
