using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheatsManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] SceneTransition scene;
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

    public void checkCheat()
    {
        if (cheats.text.Equals("action"))
        {
            scene.loadActionScene();
        }
        if (cheats.text.Equals("bar"))
        {
            scene.loadBarScene();
        }
        if (cheats.text.Equals("Towers"))
        {
            scene.loadTowerScene();
        }
        if (cheats.text.Equals("traps"))
        {
            scene.loadTrapScene();
        }
        if (cheats.text.Equals("baddies"))
        {
            scene.loadEnemyScene();
        }
        if (cheats.text.Equals("ui"))
        {
            scene.loadUiScene();
        }
        if (cheats.text.Equals("map"))
        {
            scene.loadMapScene();
        }
    }
}
