using UnityEngine;
using System.Collections;

public class LadderController : MonoBehaviour {

    public FloorController floorUp;
    public FloorController floorDn;

    // Find the point at the top of the ladder:
    public Vector3 pointAtTop() {
        Vector3 result = new Vector3();
        result.x = transform.position.x;
        result.y = floorUp.SurfaceY();
        return result;
    }
    // Find the point at the bottom of the ladder:
    public Vector3 pointAtBottom() {
        Vector3 result = new Vector3();
        result.x = transform.position.x;
        result.y = floorDn.SurfaceY();
        return result;
    }


}
