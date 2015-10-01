using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterBehavior : MonoBehaviour {
	
	bool animateAlongPath = true;
	int animateStep = 0;
	float animateTimeScale = .25f;
	float animateTimer = 0;
	List<Vector3> animatePath = new List<Vector3>();
	
	Pathfinder pathfinder;

	private Monster monster = new Monster();

	public GameObject testItem;

	// Use this for initialization
	void Start () {
		pathfinder = GetComponent<Pathfinder>();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMovement();
		monster.Update ();
	}

	private void UpdateMovement(){
		if (animateAlongPath && animatePath.Count > 0) {
			// Use a timescale to control the speed of an animation.
			// Just using Time.deltaTime would cause every animation
			// to be exactly 1 second long.
			animateTimer += Time.deltaTime * animateTimeScale;
			if (animateTimer > 1) {
				// If the timer has completed, we've arrived at the next
				// point in the path. Time to step forward to the next point.
				animateStep++;
				animateTimer = 0;
				// TODO: if you want the timing to be different (and thus the
				// speed to be consistent) between the points in your animation,
				// you'd want to calculate a new animateTimeScale here, based off
				// of the distance between the next two points in the animation.

				// For consistent speed, you may want to calculate the distance
				// between the points of any segment of the animation and modify 
				// the time scale variable to be inversely proportional to the distance.
			}
			if (animateStep < animatePath.Count - 1) {
				
				// Get the two points we're interpolating between...
				
				Vector3 pt1 = animatePath[animateStep];
				Vector3 pt2 = animatePath[animateStep + 1];
				
				// ... and interpolate between them:
				
				transform.position = Vector3.Lerp(pt1, pt2, animateTimer);
				
			} else {
				
				// If we've hit the end of the array, just set our position to
				// the last position in the array. Also, turn off an animation
				// boolean to indicate that we're done pathfinding.
				
				transform.position = animatePath[animatePath.Count - 1];
				animateAlongPath = false;
			}
		} else {
			// If we're not currently pathfinding, pick a new point,
			// and generate a path to it. Also, reset animation variables.

			// TODO: add a 'wait' timer
			Vector3 destination = pathfinder.RandomPoint();
			animatePath = pathfinder.GetPath(transform.position, destination);
			animateTimer = 0;
			animateStep = 0;
			animateAlongPath = true;
		}
	}

	public void EatFood(/*Food foodite*/){
		// TODO: 
		monster.EatFood ();
	}
}
