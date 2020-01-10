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
    public GameObject canvas;
    public TMP_InputField cheats;

    void Start()
    {
        canvas.SetActive(false);
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.BackQuote))
        {
            canvas.SetActive(true);
        }

    }
    private void loadActionScene()
    {
        SceneManager.LoadScene(nameSceneAction);
    }

    public void checkCheat()
    {
        if (cheats.text.Equals("action"))
        {
            loadActionScene();
        }
        if (cheats.text.Equals("bar"))
        {
            loadBarScene();
        }
    }

    private void loadBarScene()
    {
        SceneManager.LoadScene(nameBarScene);
    }
}
