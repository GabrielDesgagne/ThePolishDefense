using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jpMainEntry : MonoBehaviour
{
    // Start is called before the first frame update
    private PodManager podManager;

    private ArrowManager arrowManager;
    // Start is called before the first frame update
    private void Awake()
    {
        arrowManager = ArrowManager.Instance;
        podManager = PodManager.Instance;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        podManager.Refresh();
        arrowManager.Refresh();
    }

    void FixedUpdate()
    {
        
    }
}
