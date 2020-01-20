using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InteractObject : MonoBehaviour
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

    public void Pointed(bool value, Grabber grabber, RaycastHit ray)
    {

        if (!pointed && value)
            OnPointEnter(grabber, ray);

        else if (pointed && !value)
            OnPointExit(grabber, ray);

        pointed = value;

        if (pointed && value)
            IsPointed(grabber, ray);

    }
    private void Update()
    {
        
    }
    virtual protected void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Interact");
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

    virtual protected void Start()
    {
        Main.Instance.interactObjects.Add(gameObject, this);
        //meshRenderer = GetComponent<Renderer>();
        //defaultShader = meshRenderer.material.shader;
        //currentMat = meshRenderer.material;
        //if (highlightShader)
        //{
        //    currentMat.shader = highlightShader;
        //    currentMat.SetColor("_OutlineColor", Color.white);
        //    currentMat.SetFloat("_OutlineWidth", 0.0f);
        //}
        //else
        //{
        //    useHighlight = false;
        //}
    }

    #region Event

    virtual public void OnPointExit(Grabber grabber, RaycastHit hitInfo)
    {
        
    }
    virtual public void OnPointEnter(Grabber grabber, RaycastHit hitInfo)
    {
        
    }
    virtual public void IsPointed(Grabber grabber, RaycastHit hitInfo)
    {

    }

    #endregion

}
