using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsBoardController : MonoBehaviour
{
    public TextMeshProUGUI[] textList = new TextMeshProUGUI[9];
    public int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        preInit();
    }

    // Update is called once per frame
    void Update()
    {
        Refresh();
    }

    public void preInit()
    {
        textList = GetComponentsInChildren<TextMeshProUGUI>();

    }

    private void Init()
    {
        
    }

    public void Refresh()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            counter++;
            foreach(TextMeshProUGUI text in textList)
            {
                text.text = "stats updated " + counter + " times";
            }
        }
    }

    public void PhysicsRefresh()
    {

    }
}
