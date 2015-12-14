using UnityEngine;
using System;
using System.Collections;

public class EggBehavior : MonoBehaviour {

	public float hatchTime;
	public GameObject hatchPopup;
	public MonsterBehavior monsterBehavior;

	void Start () {
		hatchTime = Ease.RandomFloat(3, 5);
		print (hatchTime);
	}

	void Update () {
		hatchTime -= Time.deltaTime;
		if(hatchTime <= 0){
			// play hatching animation
				// if hatching anim over, open pop up and instantiate monster
			hatchPopup.SetActive(true);
		}
	}

	// called when monster popup is closed
	public void Hatch(string name){
		GameData.activeMonster.name = name;
		GameData.activeMonster.hatched = true;
		GameData.activeMonster.birthday = DateTime.Now;
		monsterBehavior.gameObject.SetActive(true);
		monsterBehavior.SetUp(GameData.activeMonster);
		GameData.Save ();

		Destroy(this.gameObject);
	}
}
