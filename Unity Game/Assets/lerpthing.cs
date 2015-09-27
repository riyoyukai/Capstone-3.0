using UnityEngine;
using System.Collections;

public class lerpthing : MonoBehaviour {

	float time = 0;
	Vector3 initialPosition;
	Vector3 finalPosition;

	void Start(){
		initialPosition = transform.position;
		finalPosition = initialPosition + new Vector3(3, 3, 3);
	}

	// Update is called once per frame
	void Update () {
		if(time < 1){
			print(time);
			time += Time.deltaTime;
			if(time > 1) time = 1;
			Vector3 pos = transform.position;
			pos = Vector3.Lerp(initialPosition, finalPosition, time);
			transform.position = pos;
		}
	}
}
