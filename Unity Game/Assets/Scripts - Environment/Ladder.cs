using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {

	public Floor floorUp;
	public Floor floorDn;
	
	// Find the point at the top of the ladder:
	public Vector3 GetTopPoint() {
		Vector3 result = new Vector3();
		result.x = transform.position.x;
		result.y = floorUp.SurfaceY();
		return result;
	}
	// Find the point at the bottom of the ladder:
	public Vector3 GetBottomPoint() {
		Vector3 result = new Vector3();
		result.x = transform.position.x;
		result.y = floorDn.SurfaceY();
		return result;
	}
}
