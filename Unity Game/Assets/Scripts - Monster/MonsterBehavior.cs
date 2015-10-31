using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterBehavior : MonoBehaviour {
	
	enum Facing {
		Left,
		Right,
		Away,
		Forward
	}
	
	enum AnimState {
		Idle,		// 0
		Walk,		// 1
		Climb,		// 2
		Turn,		// 3
		FindFood,	// 4
		Eat,		// 5
		Cuddle		// 6
	}

	private float eatTimer = 0;
	private ItemBehavior targetItem;
	
	private Facing facing = Facing.Right;
	private Facing facingTarget;
	private Quaternion turnLocation;
	private Vector3 turnDestination;
	float turnTimer = 0;
	private Vector3 facingLeft = new Vector3(0, 179, 0);
	private Vector3 facingRight = new Vector3(0, 1, 0);
	private Vector3 facingAway = new Vector3(0, 270, 0);
	private Vector3 facingForward = new Vector3(0, 90, 0);
	
	private AnimState lastState = AnimState.Walk;
	private AnimState state = AnimState.Walk;
	private Animator animator;

	// variables for animating walk
	int animateStep = 0;
	float animateTimeScale = .25f;
	float animateTimer = 0;
	List<Vector3> animatePath = new List<Vector3>();
	Pathfinder pathfinder;

	// variables for animating idle
	float idleTimer = 0;
	float idleTimerMin = 2;
	float idleTimerMax = 10;

	// link to monster class
	private Monster monster;

	// TODO: delete this
	public GameObject testItem;

	// Use this for initialization
	void Start () {
		GameData.activeMonster = new Monster(); // TODO: remove from testing
		monster = GameData.activeMonster;
		monster.monsterController = this;
		pathfinder = GetComponent<Pathfinder>();
		animator = GetComponentInChildren<Animator>();
//		BeginAnimWalk(null);
		BeginAnimIdle();
	}

	void OnMouseOver(){
		if(Input.GetMouseButtonDown(0)){
			if(state == AnimState.Idle || state == AnimState.Walk){
				BeginAnimCuddle();
			}
		}
	}

	// sets current state and updates animController
	void ChangeState(AnimState animState){
		state = animState;
		animator.SetInteger("AnimState", (int)state);
	}
	
	// Update is called once per frame
	void Update () {
		if(state == AnimState.Cuddle && Input.GetMouseButtonUp(0)){
			ChangeState(lastState);
		}
		UpdateMovement();
		monster.Update();
	}

	private void UpdateMovement(){
		switch(state){
			case AnimState.Idle:
				AnimIdle ();
				break;
			case AnimState.FindFood:
			case AnimState.Walk:
			case AnimState.Climb:
				AnimWalk ();
				break;
			case AnimState.Turn:
				AnimTurn();
				break;
			case AnimState.Eat:
				AnimEat();
				break;
			case AnimState.Cuddle:
				AnimCuddle();
				break;
		}
	}

	private void BeginAnimCuddle(){
		lastState = state;
		ChangeState(AnimState.Cuddle);
		transform.rotation = Quaternion.Euler(facingForward); //TODO: remove this
		print ("Cuddling...");
	}

	private void AnimCuddle(){

	}
	
	private void BeginAnimEat(){
		print("Eating...");
		ChangeState(AnimState.Eat);
		eatTimer = 3f;
	}
	
	private void AnimEat(){
		eatTimer -= Time.deltaTime;
		if(eatTimer <= 0){
			eatTimer = 0;
			print ("Done eating, time to react!");
			monster.EatFood();
			Destroy(targetItem.gameObject);
			// TODO: polite/rude reaction here
			lastState = AnimState.Walk;
			BeginAnimIdle();
		}
	}

	private void CheckForTurn(){
		Vector3 pt1 = transform.position;
		Vector3 pt2 = animatePath[animateStep+1]; // should not error
		//TODO: check if we need to turn first
		if(pt2.x > pt1.x){ // check to face right
			if(facing != Facing.Right){
				facingTarget = Facing.Right;
				BeginAnimTurn(facingRight);
			}
		}else if(pt2.x < pt1.x){ // check to face left
			if(facing != Facing.Left){
				facingTarget = Facing.Left;
				BeginAnimTurn(facingLeft);
			}
		}else{ // check to face toward ladder
			if(facing != Facing.Away){
				facingTarget = Facing.Away;
				BeginAnimTurn(facingAway);
			}
		}
	}
	
	private void BeginAnimTurn(Vector3 whichWay){
		print ("Turning...");
		lastState = state;
		ChangeState(AnimState.Turn);
		turnLocation = transform.rotation;
		turnDestination = whichWay;
		turnTimer = 0;
	}
	
	private void AnimTurn(){
		//lerp the Y rotation
		turnTimer += Time.deltaTime / 1.5f;
		Quaternion rot = Quaternion.Lerp(turnLocation, Quaternion.Euler(turnDestination), turnTimer);
		transform.rotation = rot;

		if(turnTimer >= 1){
			turnTimer = 1;
			ChangeState(lastState);
			facing = facingTarget;
		}
	}

	private void BeginAnimIdle(){
		// pick random time to wait
		print ("Idling...");
		ChangeState(AnimState.Idle);
		idleTimer = Ease.RandomFloat(idleTimerMin, idleTimerMax);
	}

	private void AnimIdle(){
		idleTimer -= Time.deltaTime;
		if(idleTimer <= 0){
			idleTimer = 0;
			BeginAnimWalk(null);
		}
	}

	public void BeginAnimWalk(ItemBehavior target){
		ChangeState(AnimState.Walk);
		// Pick a new point, and generate a path to it.
		// Also, reset animation variables.
		Vector3 destination;
		if(target != null){
			print ("Finding food...");
			targetItem = target;
			ChangeState(AnimState.FindFood);
			destination = target.transform.position;
		}else{
			print ("Walking...");
			ChangeState(AnimState.Walk);
			destination = pathfinder.RandomPoint();
		}
		animatePath = pathfinder.GetPath(transform.position, destination);
		animateTimer = 0;
		animateStep = 0;

		CheckForTurn();		
	}

	private void AnimWalk(){
		if (animatePath.Count > 0) {
			// Use a timescale to control the speed of an animation.
			// Just using Time.deltaTime would cause every animation
			// to be exactly 1 second long.
			animateTimer += Time.deltaTime * animateTimeScale;
			if (animateTimer >= 1) {
				// If the timer has completed, we've arrived at the next
				// point in the path. Time to step forward to the next point.
				animateStep++;
				transform.position = animatePath[animateStep];
				animateTimer = 0;
				// if you want the timing to be different (and thus the
				// speed to be consistent) between the points in your animation,
				// you'd want to calculate a new animateTimeScale here, based off
				// of the distance between the next two points in the animation.
				
				// For consistent speed, you may want to calculate the distance
				// between the points of any segment of the animation and modify 
				// the time scale variable to be inversely proportional to the distance.
				if(animateStep < animatePath.Count - 1){
					
					Vector3 pt1 = animatePath[animateStep];
					Vector3 pt2 = animatePath[animateStep + 1];

					float distance = Vector3.Distance(pt1, pt2);
					animateTimeScale = (1/distance);
					if(distance < 1) animateTimeScale = distance/2; // to prevent nyooming
					// TODO: Check if you're going vertically or horizontally next, and play the correct animation
					if(state != AnimState.FindFood && transform.position.x == animatePath[animateStep + 1].x){
						ChangeState(AnimState.Climb);
					}
					CheckForTurn();
				} else {
					
					// If we've hit the end of the array, just set our position to
					// the last position in the array.
					
					transform.position = animatePath[animatePath.Count - 1];
					if(state == AnimState.FindFood){
						print ("Food reached. TODO: Check if it's still there");
						BeginAnimEat();
					}else{
						BeginAnimIdle();
					}
				}
			}else if (animateStep < animatePath.Count - 1) {
				
				// Get the two points we're interpolating between...
				
				Vector3 pt1 = animatePath[animateStep];
				Vector3 pt2 = animatePath[animateStep + 1];
				
				// ... and interpolate between them:
				
				transform.position = Vector3.Lerp(pt1, pt2, animateTimer);
				
			}
		}else{
			print ("We're trying to walk, but there are no points in the path.");
		}
	}
}
