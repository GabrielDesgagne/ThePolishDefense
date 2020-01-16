using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlockRotation : MonoBehaviour
{
    public float yOffset;

    public float zOffSet;
    void Update()
    {
        transform.eulerAngles = new Vector3(0,transform.parent.eulerAngles.y,0);
        Vector3 pos = transform.parent.position;
        pos.y += yOffset;
        pos.z += zOffSet;
        transform.position = pos;
    }
}
