using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Popup : MonoBehaviour {

	public Text hatchName;
	private int taskID;
	public Text taskName;
	public GameObject itemHolder;
	public GameObject itemPrefab;
	public Text doTaskText;
	public GameObject[] foods;
	private int foodType;

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

	public void DoTask(){
		if(GameData.tasks.Count > 0){
			// suggest task based on deadline
			Task t = GameData.tasks[Ease.RandomInt(0, GameData.tasks.Count)];
			doTaskText.text = "";
		}else{
			// go create a task
			doTaskText.text = "You have no tasks! Can you create and complete a task so I can eat?";
		}
	}

	public void E_Hatch(EggBehavior egg){
		if(hatchName.text.Trim() == "") return;
		egg.Hatch(hatchName.text);
		ClosePopup();
	}

	public void QueueTask(int tID){
		taskID = tID;
		taskName.text = GameData.completedTasks[tID].name;
		foodType = Ease.RandomInt(0, 3);
		for(int i = 0; i < foods.Length; i++){
			if(i == foodType) foods[i].SetActive(true);
			else foods[i].SetActive(false);
		}
	}

	public void E_CompleteTask(){
		// TODO: give random position
		Vector3 pos = itemPrefab.transform.position;
		pos.x = Ease.RandomFloat(-5.2f, 8.2f);
		pos.y = Ease.RandomFloat(-9f, 6f);

		GameObject newItem = Instantiate(
			itemPrefab,
			pos,
			itemPrefab.transform.rotation
			) as GameObject;
		newItem.transform.SetParent(itemHolder.transform, false);
		ItemBehavior ib = newItem.GetComponent<ItemBehavior>();
		ib.item = new Item("Food");
		ib.item.foodType = foodType;
		ib.item.itemBehavior = ib;
		GameData.items.Add(ib.item);
		
		// TODO: check if monster is hungry and initiate findfood if so
		if(GameData.activeMonster.Hungry()){
			GameData.activeMonster.monsterController.BeginAnimFindFood(false);
		}

		GameData.completedTasks.RemoveAt(taskID);
		ClosePopup();
		GameData.Save();
	}
}
