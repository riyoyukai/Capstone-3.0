using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	private float speed = 5;
	private int startPosY = -410;
	private int endPosY = 1200;

	void Update(){
		Vector3 newPos = transform.position;
		newPos.y += speed;
		if(newPos.y > endPosY){
			newPos.y = startPosY;
		}
		transform.position = newPos;
	}
}
