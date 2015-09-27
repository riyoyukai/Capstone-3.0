using UnityEngine;
using System.Collections.Generic;

public class PathFinder : MonoBehaviour {

    public float extentsX;
    public FloorController[] floors;

	public List<Vector3> getPath(Vector3 start, Vector3 end) {

        List<Vector3> path = new List<Vector3>();

        path.Add(start);

        FloorController floorCurr = findNearestFloor(start);
        FloorController floorEnd = findNearestFloor(end);

        while (floorCurr != floorEnd) {
            if (floorCurr.transform.position.y < floorEnd.transform.position.y) {
                // find the ladder that goes up:
                LadderController ladder = floorCurr.ladderUp;
                // ask the ladder for the points at bottom and top:
                path.Add(ladder.pointAtBottom());
                path.Add(ladder.pointAtTop());
                // set the current floor to the next floor:
                floorCurr = ladder.floorUp;
            } else {
                // find the ladder that goes down:
                LadderController ladder = floorCurr.ladderDn;
                // ask the ladder for the points at bottom and top:
                path.Add(ladder.pointAtTop());
                path.Add(ladder.pointAtBottom());
                // set the current floor to the next floor:
                floorCurr = ladder.floorDn;
            }
        }

        path.Add(end);

        return path;
    }
    FloorController findNearestFloor(Vector3 pt) {
        float dis = float.MaxValue;
        FloorController result = null;
        foreach(FloorController floor in floors) {
            float d = Mathf.Abs(floor.transform.position.y + floor.offsetFloorSurface - pt.y);
            if(d < dis) {
                result = floor;
                dis = d;
            }
        }
        return result;
    }

    public Vector3 pickARandomPoint() {
        int index = Random.Range(0, floors.Length);

        Vector3 result = new Vector3();
        result.x = Random.Range(-extentsX, extentsX);
        result.y = floors[index].SurfaceY();
        return result;
    }
}
