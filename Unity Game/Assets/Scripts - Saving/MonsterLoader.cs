using UnityEngine;
using System.Collections;

public class MonsterLoader : MonoBehaviour {

	public MonsterBehavior monster;
	public EggBehavior egg;

	void Start(){
		if(GameData.activeMonster != null){ // idk wtf i am doinggg
			Destroy(egg.gameObject);
			monster.gameObject.SetActive(true);
			monster.SetUp(GameData.activeMonster);
		}
	}

}
