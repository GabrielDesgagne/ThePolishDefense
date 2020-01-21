using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public TextMeshPro lifeUi;
    public Transform camToTurn;
    private OVRInput.Controller controllerUseToTp;
    private bool teleportBegin;
    private GameObject teleportZonePrefab;
    public float offSet;
    private bool onTeleport;
    [HideInInspector]
    public PlayerStats playerStat;
    public Grabber LeftHand;
    public Grabber RightHand;
    Vector3 positionTp;

    private CharacterController characterController;
    public float range = 1;
    public void PreInitialize()
    {
        teleportZonePrefab = Instantiate(Resources.Load("Prefabs/Player/TeleportPoint")) as GameObject;
       teleportZonePrefab.SetActive(false);
        LeftHand.PreInitialize();
        RightHand.PreInitialize();
        playerStat = GetComponent<PlayerStats>();
        PlayerStats.addHp(playerStat.initialHp);
    }
    public void Initialize()
    {
        LeftHand.Initialize();
        RightHand.Initialize();
        //playerController.Initialize();
    }

    public void Refresh()
    {
        LeftHand.Refresh();
        RightHand.Refresh();
        //playerController.Refresh();
    }

    public void PhysicsRefresh()
    {
        Vector3 bob = camToTurn.localEulerAngles;
        if(InputManager.Instance.inputs.Touch[OVRInput.Controller.LTouch].JoystickLeft)
            bob.y += 1f;
        else if(InputManager.Instance.inputs.Touch[OVRInput.Controller.LTouch].JoystickRight)
            bob.y -= 1f;
        camToTurn.localEulerAngles = bob;
        if (InputManager.Instance.inputs.Touch[OVRInput.Controller.LTouch].ButtonOne)
        {
            controllerUseToTp = OVRInput.Controller.LTouch;
            onTeleport = true;
        }
        else if(InputManager.Instance.inputs.Touch[OVRInput.Controller.RTouch].ButtonOne)
        {
            controllerUseToTp = OVRInput.Controller.RTouch;
            onTeleport = true;
        }
        else
        {
            controllerUseToTp = 0;
            teleportZonePrefab.SetActive(false);
            endTeleport();
        }

        if (onTeleport)
        {
            beginTeleport();
        }
        changeLife();
    }

    public void EndFlow()
    {

    }

    private void beginTeleport()
    {
        Ray ray = new Ray();
        if (controllerUseToTp == OVRInput.Controller.LTouch)
        {
            ray = new Ray(LeftHand.transform.position, LeftHand.transform.forward);
        }
        else if (controllerUseToTp == OVRInput.Controller.RTouch)
        {
            ray = new Ray(RightHand.transform.position, RightHand.transform.forward);
        }

        RaycastHit rayHit;
        if (Physics.Raycast(ray, out rayHit, 100,1 << 10))
        {
            positionTp = rayHit.point;
            teleportZonePrefab.SetActive(true);
            positionTp.y += offSet;
            teleportZonePrefab.transform.position = positionTp;
        }
        else
        {
            teleportZonePrefab.SetActive(false);
        }
    }

    private void changeLife()
    {
        if (lifeUi.text != PlayerStats.Hp.ToString())
        {
            lifeUi.text = "Vie: " + PlayerStats.Hp;
        }
    }
    private void endTeleport()
    {
        if (onTeleport && positionTp != Vector3.zero)
        {
            onTeleport = false;
            transform.position = positionTp;
            positionTp = Vector3.zero;
        }
    }
}
