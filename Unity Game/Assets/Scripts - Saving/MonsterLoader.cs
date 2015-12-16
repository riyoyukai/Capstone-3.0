using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MonsterLoader : MonoBehaviour {

	public MonsterBehavior monster;
	public EggBehavior egg;


	void Start(){
		if(GameData.activeMonster.hatched){
			Destroy(egg.gameObject);
			monster.gameObject.SetActive(true);
			monster.SetUp(GameData.activeMonster);
			if(GameData.activeMonster.Hungry()){
				bool showPopup = true;
				if(GameData.completedTasks.Count > 0) showPopup = false;
				monster.BeginAnimFindFood(showPopup);
			}
		}else{
//			egg.hatchTime = GameData.activeMonster.hatchTime;
		}
	}

}
