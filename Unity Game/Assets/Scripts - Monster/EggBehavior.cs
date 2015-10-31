using UnityEngine;
using System.Collections;

public class EggBehavior : MonoBehaviour {

	float hatchTime;
	public GameObject monsterPrefab;
	public GameObject hatchPopup;
	public GameObject monsterBehavior;

	// Use this for initialization
	void Start () {
		hatchTime = Ease.RandomFloat(3, 5);
		print (hatchTime);
	}
	
	// Update is called once per frame
	void Update () {
		hatchTime -= Time.deltaTime;
		if(hatchTime <= 0){
			// play hatching animation
				// if hatching anim over, open pop up and instantiate monster
			hatchPopup.SetActive(true);
		}
	}

	public void Hatch(){
		Monster monster = new Monster();
		//Instantiate(monsterPrefab, this.transform.position, this.transform.rotation);
		//monsterBehavior.transform.position = this.transform.position;
		monsterBehavior.SetActive(true);
		Destroy(this.gameObject);
	}
}
