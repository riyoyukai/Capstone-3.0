using UnityEngine;
using System.Collections;

public class FloorController : MonoBehaviour {

    public float offsetFloorSurface;
    public LadderController ladderUp;
    public LadderController ladderDn;

	public float SurfaceY() {
        return transform.position.y + offsetFloorSurface;
    }
}
