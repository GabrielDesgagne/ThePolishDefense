using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInteract : InteractObject
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private Transform teleportPoint;
    public override void OnPointEnter(Grabber grabber, RaycastHit hitInfo)
    {
        Debug.Log("On Pointed enter: " + teleportPoint.position, this);
        arrow.SetActive(true);
    }

    public override void IsPointed(Grabber grabber, RaycastHit hitInfo)
    {
        bool teleport = OVRInput.GetDown(OVRInput.Button.One);
        if (teleport)
        {
            Debug.Log(teleportPoint.position);
            grabber.Player.transform.position = teleportPoint.position;
        }
    }

    public override void OnPointExit(Grabber grabber, RaycastHit hitInfo)
    {
        Debug.Log("On Pointed exit", this);
        arrow.SetActive(false);
    }
}
