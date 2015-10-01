using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WanderNoCC : MonoBehaviour {
	
	enum FacingDirection {
		Left,
		Right
	}
	
	enum AnimState {
		Idle,
		Walk,
		Turn
	}

	private Vector3 target = new Vector3(0, 0, -10);
	private Pathfinder pathfinder = new Pathfinder();
	public List<Floor> floors;
	public List<Ladder> ladders;
	private FacingDirection dir = FacingDirection.Left;

	public List<GameObject> testItems;

	void Start(){
		PickNewPoint();
	}

	private void PickNewPoint(){
//		target.x = Ease.RandomFloat (-3, 3);
//
//		int r = Ease.RandomInt (0, floors.Count-1);
//		Bounds bounds = floors[r].GetComponent<BoxCollider2D>().bounds;
//		target.y = floors[r].transform.position.y + bounds.size.y/2; // gets top of floor
//		
//		Vector3 currentTarget = transform.position;
//		pathfinder.nodes.Add(currentTarget);
////		pathfinder.nodes.Add(target);
//		int noInf = 0;
//		while(currentTarget != target && noInf < 10){
//			print ("TEST: " + currentTarget.y + ", " + target.y);
//			if(Ease.Approx(currentTarget.y, target.y)){ // on same floor
//				//if(
////				currentTarget = target;
//			}else if(target.y > currentTarget.y){ // target above monster
////				currentTarget = FindLadderAt(true, currentTarget.y);
//			}else if(target.y < currentTarget.y){ // target below monster
////				currentTarget = FindLadderAt(false, currentTarget.y);
//			}
//			noInf++;
//		}
//		pathfinder.nodes.Add(target);
//		
//		string asdf = "";
//		for(int i = 0; i < pathfinder.nodes.Count; i++){
//			asdf += pathfinder.nodes[i] + " --- ";
//			testItems[i].transform.position = pathfinder.nodes[i];
//		}
//		print(asdf);
	}

	private void FindLadderAt(bool goingUp, float yPos){
//		for(int i = 0; i < ladders.Count; i++){
//			if(goingUp == ladders[i].bottomOfLadder && // both true or both false
//			   Ease.Approx(ladders[i].transform.position.y, yPos)){
//				pathfinder.nodes.Add(ladders[i].transform.position);
//				print ("Found one ladder");
//				for(int j = 0; j < ladders.Count; j++){
//					if(i != j && Ease.Approx(ladders[i].transform.position.y, ladders[j].transform.position.y)){
//						pathfinder.nodes.Add(ladders[j].transform.position);
//						print ("Found both ladders");
//						return ladders[j].transform.position;
//					}
//				}
//			}
//		}
//		print ("Ladder finder didn't work");
//		return Vector3.zero;
	}

	void Update(){
//		Vector3 pos = pathfinder.Update(Time.deltaTime);
//		transform.position = pos;

	}
}
