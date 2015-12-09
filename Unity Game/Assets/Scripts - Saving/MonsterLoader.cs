using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MonsterLoader : MonoBehaviour {

	public MonsterBehavior monster;
	public EggBehavior egg;


	void Start(){
		if(GameData.activeMonster.hatched){ // idk wtf i am doinggg
			Destroy(egg.gameObject);
			monster.gameObject.SetActive(true);
			monster.SetUp(GameData.activeMonster);
		}
	}

}
