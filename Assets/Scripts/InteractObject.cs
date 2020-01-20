using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InteractObject : MonoBehaviour, IInteract
{
    /* Var visible in Inspector */
    [SerializeField] private bool useHighlight;
    [SerializeField] private float distanceRange = 10;
    [SerializeField] protected Shader highlightShader;
    [SerializeField] protected Color focusColor;

    /* Var private/protected */
    
    protected Material currentMat;
    protected Renderer meshRenderer;
    protected Shader defaultShader;
    private bool pointed;

    /* Getter */
    public float DistanceRange => distanceRange;
    public bool UseHighlight => useHighlight;

    virtual public void Pointed(bool value, Grabber grabber, RaycastHit ray)
    {
        if (!pointed && value)
            OnPointEnter(grabber);

        else if (pointed && !value)
            OnPointExit(grabber);


        pointed = value;
    }
    private void Update()
    {
        
    }


    public void SetHighlight(bool active)
    {
        if (useHighlight)
        {
            if (active)
            {
                currentMat.SetColor("_OutlineColor", focusColor);
                currentMat.SetFloat("_OutlineWidth", 0.05f);
            }

            else
            {
                currentMat.SetColor("_OutlineColor", Color.white);
                currentMat.SetFloat("_OutlineWidth", 0.0f);
            }
        }
    }

    private void Start()
    {
        meshRenderer = GetComponent<Renderer>();
        defaultShader = meshRenderer.material.shader;
        currentMat = meshRenderer.material;
        if (highlightShader)
        {
            currentMat.shader = highlightShader;
            currentMat.SetColor("_OutlineColor", Color.white);
            currentMat.SetFloat("_OutlineWidth", 0.0f);
        }
        else
        {
            useHighlight = false;
        }
    }

    virtual public void OnPointExit(Grabber grabber)
    {
        
    }

    virtual public void OnPointEnter(Grabber grabber)
    {
        
    }

    virtual public void WillBeGrabbed(Grabber grabber)
    {
        
    }

    virtual public void Grabbed(Grabber grabber)
    {
        
    }

    virtual public void WillBeReleased(Grabber grabber)
    {
       
    }

    virtual public void Released(Grabber grabber)
    {
        
    }
}
