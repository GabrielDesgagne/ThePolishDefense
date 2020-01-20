using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods {
    public static RaycastHit GetClosestHit(this RaycastHit[] hits, Vector3 fromWhere) {
        ushort indexSmallestDistance = 0;
        Vector3 closestHit = hits[indexSmallestDistance].point;

        for (ushort i = 1; i < hits.Length; i++) {
            //Find smallest distance
            if (Vector3.Distance(fromWhere, hits[i].point) < Vector3.Distance(fromWhere, closestHit)) {
                closestHit = hits[i].point;
                indexSmallestDistance = i;
            }
        }

        return hits[indexSmallestDistance];
    }
}
