using UnityEngine;
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
		Monster monster = new Monster();
		monster.name = name;
		GameData.activeMonster = monster;
		monsterBehavior.gameObject.SetActive(true);
		monsterBehavior.SetUp(monster);

		Destroy(this.gameObject);
	}
}
