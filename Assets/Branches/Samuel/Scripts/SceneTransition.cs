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
    public string nameTowersScene;
    public string nameTrapScene;
    public string enemySceneName;
    public string uiSceneName;
    public string mapSceneName;


    public void loadActionScene()
    {
        SceneManager.LoadScene(nameSceneAction);
    }

    public void loadBarScene()
    {
        SceneManager.LoadScene(nameBarScene);
    }
    public void loadTowerScene()
    {
        SceneManager.LoadScene(nameTowersScene);
        Debug.Log("Tower scene");
    }
    public void loadTrapScene()
    {
        SceneManager.LoadScene(nameTrapScene);
    }
    public void loadEnemyScene()
    {

        SceneManager.LoadScene(enemySceneName);
    }
    public void loadUiScene()
    {
        SceneManager.LoadScene(uiSceneName);
    }
    public void loadMapScene()
    {
        SceneManager.LoadScene(mapSceneName);
    }
}
