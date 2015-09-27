using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinder {

	public List<Vector3> nodes = new List<Vector3>();
	public int counter;
	public float time = 0;
	bool next = false;
	Vector3 pos = Vector3.zero;

	public Pathfinder(){ }

	public Vector3 Update(float deltaTime){
		time += deltaTime * .1f;

		if(time > 1){
			time = 1;
			next = true;
		}

		if(counter < nodes.Count-1){
			pos = Vector3.Lerp(nodes[counter], nodes[counter+1], time);
		}

		if(next){
			NextNode();
		}

		return pos;
	}

	public void Reset(){
		nodes.Clear();
		counter = 0;
		time = 0;
	}

	public void NextNode(){
		time = 0;
		counter++;
		next = false;
	}

}



/*
// linear path animation class
PathNodes
	Array of points (1,1) to (3,1) to (3,3)
	Counter for which point (on or heading to) (or segment between points)
	Timer (which adds delta time and eventually becomes 1, starts at 0 for each point-to-point)
	output a vector 3 depending on how much time

	vec3.lerp from beginning point to next point

	update increases timer
		each time it's above 1, reset timer, increment counter
		- don't go beyond final node

	abort all animation function/interrupt

	- not a monobehavior
 */