using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class SceneTransition : MonoBehaviour
{
    public string nameSceneAction;
    public string nameBarScene;

    void Start()
    {
      
       
    }

    // Update is called once per frame
    void Update()
    {
       

    }
    public void loadActionScene()
    {
        SceneManager.LoadScene(nameSceneAction);
    }

   

    public void loadBarScene()
    {
        SceneManager.LoadScene(nameBarScene);
    }
}
