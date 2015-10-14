using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinder : MonoBehaviour {
	
	private float extentsX = 3;
	public Floor[] floors;

	public List<Vector3> GetPath(Vector3 start, Vector3 end){
		List<Vector3> path = new List<Vector3>();
		
		path.Add(start);

		Floor floorCurr = FindNearestFloor(start);
		Floor floorEnd = FindNearestFloor(end);
		
		while (floorCurr != floorEnd) {
			if (floorCurr.transform.position.y < floorEnd.transform.position.y) {
				// find the ladder that goes up:
				Ladder ladder = floorCurr.ladderUp;
				// ask the ladder for the points at bottom and top:
				path.Add(ladder.GetBottomPoint());
				path.Add(ladder.GetTopPoint());
				// set the current floor to the next floor:
				floorCurr = ladder.floorUp;
			} else {
				// find the ladder that goes down:
				Ladder ladder = floorCurr.ladderDn;
				// ask the ladder for the points at bottom and top:
				path.Add(ladder.GetTopPoint());
				path.Add(ladder.GetBottomPoint());
				// set the current floor to the next floor:
				floorCurr = ladder.floorDn;
			}
		}

		path.Add(new Vector3(end.x, floorEnd.SurfaceY(), end.z));
		//path.Add(end);

		return path;
	}

	Floor FindNearestFloor(Vector3 pt) {
		float dis = float.MaxValue;
		Floor result = null;
		foreach(Floor floor in floors) {
			float d = Mathf.Abs(floor.SurfaceY() - pt.y);
			if(d < dis) {
				result = floor;
				dis = d;
			}
		}
		return result;
	}
	
	public Vector3 RandomPoint() {
		int index = Random.Range(0, floors.Length);
		
		Vector3 result = new Vector3(0, 0, 0);
		result.x = Random.Range(-extentsX, extentsX);
		result.y = floors[index].SurfaceY();
		return result;
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