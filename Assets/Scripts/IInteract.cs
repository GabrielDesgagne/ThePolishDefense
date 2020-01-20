using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteract
{
    void OnPointExit(Grabber grabber);
    void OnPointEnter(Grabber grabber);
    void WillBeGrabbed(Grabber grabber);
    void Grabbed(Grabber grabber);
    void WillBeReleased(Grabber grabber);
    void Released(Grabber grabber);
}
