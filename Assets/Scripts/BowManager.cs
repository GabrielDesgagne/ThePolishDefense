using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class BowManager : MonoBehaviour
{
    public float forceArrowMaxShoot = 0.5f;
    private float pourcentString;
    //public static BowManager Instance;
    public GameObject arrowPrefab;
    public GameObject stringAttachPoint;
    public GameObject stringStartPoint;
    private Vector3 stringAttachPointBase;
    private Arrow currentSnapArrow;
    private bool bowLeftHand = false;
    AmbianceManager ambiance;
    private OVRGrabbable hand;
    public bool isLeft;
    public Vector3 rotOffSet;
    public float yHandOffset = 20;
    public float yHandOffsetPos;
    Vector3 rotation;
    // Start is called before the first frame update
    public void DestroyBow()
    {
        Destroy(this.gameObject);
    }
    private void Awake()
    {
        ambiance = AmbianceManager.Instance;
        rotation = Vector3.zero;
        hand = GetComponent<OVRGrabbable>();
    }

    private void Start()
    {
        Vector3 temp;
        if (rotation == Vector3.zero)
        {
            Vector3 rotation = transform.GetChild(0).transform.localEulerAngles;
        }

        temp = rotation;
        temp += rotOffSet;
        temp.x += yHandOffset;

        transform.GetChild(0).transform.localPosition += new Vector3(0.03f, yHandOffsetPos, 0);
        transform.GetChild(0).transform.localEulerAngles = temp;
    }
    void FixedUpdate()
    {

        if (currentSnapArrow != null)
        {
            PullString();
        }
    }
    public void AttachBowToArrow(Arrow arrowToAttach)
    {
        currentSnapArrow = arrowToAttach;
        currentSnapArrow.transform.parent = stringAttachPoint.transform;
        currentSnapArrow.transform.localPosition = new Vector3(0, -3.15f, 0);
        currentSnapArrow.transform.localEulerAngles = new Vector3(90, 0, 0);
    }
    private void PullString()
    {
        if (currentSnapArrow != null)
        {
            Vector3 temp = stringAttachPointBase;
            float dist = (OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch) - OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch)).magnitude;
            if (dist >= 0.26f && dist < 0.60f)
            {
                pourcentString = 100 * (dist - 0.15f) / 0.20f;
                temp.y -= 2.8f * pourcentString / 100f;
                stringAttachPoint.transform.localPosition = temp;
            }
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) < 0.1f || OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch) < 0.1f)
            {
                Fire();
                pourcentString = 0;
                currentSnapArrow = null;
            }
        }
    }
    private void Fire()
    {
        ambiance.BowSounds();
        currentSnapArrow.Fired((forceArrowMaxShoot * pourcentString) / 100f);

        stringAttachPoint.transform.localPosition = stringStartPoint.transform.localPosition;

        currentSnapArrow = null;
    }
}
