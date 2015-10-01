using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

	public Ladder ladderUp;
	public Ladder ladderDn;
	
	public float SurfaceY() {
		Bounds bounds = GetComponent<BoxCollider2D>().bounds;
		return transform.position.y + bounds.size.y/2 + .05f;
	}
}
