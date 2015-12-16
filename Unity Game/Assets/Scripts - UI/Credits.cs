using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	private float speed = .8f;
	private int startPosY = -250;
	private int endPosY = 1520;

	void Update(){
		Vector3 newPos = transform.position;
		newPos.y += speed;
		if(newPos.y > endPosY){
			newPos.y = startPosY;
		}
		transform.position = newPos;
	}
}
