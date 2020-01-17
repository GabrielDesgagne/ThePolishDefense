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

    public void Pointed(bool value, Grabber grabber)
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

    public void OnPointExit(Grabber grabber)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointEnter(Grabber grabber)
    {
        throw new System.NotImplementedException();
    }

    public void WillBeGrabbed(Grabber grabber)
    {
        throw new System.NotImplementedException();
    }

    public void Grabbed(Grabber grabber)
    {
        throw new System.NotImplementedException();
    }

    public void WillBeReleased(Grabber grabber)
    {
        throw new System.NotImplementedException();
    }

    public void Released(Grabber grabber)
    {
        throw new System.NotImplementedException();
    }
}
