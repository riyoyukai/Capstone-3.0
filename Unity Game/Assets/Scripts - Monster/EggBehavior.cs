using UnityEngine;
using System.Collections;

public class EggBehavior : MonoBehaviour {

	float hatchTime;

	// Use this for initialization
	void Start () {
		hatchTime = Ease.RandomFloat(10, 20);
		print (hatchTime);
	}
	
	// Update is called once per frame
	void Update () {
		hatchTime -= Time.deltaTime;
		if(hatchTime <= 0){
			// play hatching animation
				// if hatching anim over, open pop up and instantiate monster
		}
	}
}
