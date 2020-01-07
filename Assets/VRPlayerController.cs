using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRPlayerController : MonoBehaviour
{
    public Transform uiDebugParent;
    Rigidbody rb;
    Dictionary<string, Text> nameToUITextDict = new Dictionary<string, Text>();
    // Start is called before the first frame update
    string[] axises = new string[]
    {
          "Oculus_GearVR_LThumbstickX"                        ,
          "Oculus_GearVR_LThumbstickY"                        ,
          "Oculus_GearVR_RThumbstickX"                        ,
          "Oculus_GearVR_RThumbstickY"                        ,
          "Oculus_GearVR_DpadX"                               ,
          "Oculus_GearVR_DpadY"                               ,
          "Oculus_GearVR_LIndexTrigger"                       ,
          "Oculus_GearVR_RIndexTrigger"                       ,
          "Oculus_CrossPlatform_PrimaryIndexTrigger"          ,
          "Oculus_CrossPlatform_SecondaryIndexTrigger"        ,
          "Oculus_CrossPlatform_PrimaryHandTrigger"           ,
          "Oculus_CrossPlatform_SecondaryHandTrigger"         ,
          "Oculus_CrossPlatform_PrimaryThumbstickHorizontal"  ,
          "Oculus_CrossPlatform_PrimaryThumbstickVertical"    ,
          "Oculus_CrossPlatform_SecondaryThumbstickHorizontal",
          "Oculus_CrossPlatform_SecondaryThumbstickVertical"
    };

    string[] ovrbuttons = new string[]
    {
          "Oculus_CrossPlatform_Button2",               
          "Oculus_CrossPlatform_Button4",               
          "Oculus_CrossPlatform_PrimaryThumbstick",     
          "Oculus_CrossPlatform_SecondaryThumbstick"    
    };


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameObject textResource = Resources.Load<GameObject>("TestText");
        foreach (string s in axises)
        {
            GameObject go = GameObject.Instantiate(textResource, uiDebugParent);
            nameToUITextDict.Add(s,go.GetComponent<Text>());
            nameToUITextDict[s].text = s.Replace("Oculus_", "").Replace("GearVR_", "").Replace("CrossPlatform_", "");
        }
        foreach (string s in ovrbuttons)
        {
            GameObject go = GameObject.Instantiate(textResource, uiDebugParent);
            nameToUITextDict.Add(s, go.GetComponent<Text>());
            nameToUITextDict[s].text = s.Replace("Oculus_", "").Replace("GearVR_", "").Replace("CrossPlatform_", "");
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(KeyValuePair<string,Text> kv in nameToUITextDict)
        {
            kv.Value.text = kv.Key.Replace("Oculus_", "").Replace("GearVR_", "").Replace("CrossPlatform_", "") + ": \n" + Input.GetAxis(kv.Key);
        }
        //transform.position = transform.position + Vector3.up;
    }

    private void FixedUpdate()
    {
        //rb.velocity = new Vector3(Input.GetAxis("Oculus_GearVR_LThumbstickX"), 0, Input.GetAxis("Oculus_GearVR_LThumbstickY"));
        rb.MovePosition(new Vector3(10, 10, 10));
    }
}
