using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InteractObject : MonoBehaviour
{
    [SerializeField] private bool useHighlight;
    [Range(0, 10)]
    [SerializeField] private float range;

    public Shader highlightShader;
    public Color focusColor;
    Material currentMat;
    public float Range { get; set; } // TODO rangefield

    private bool pointed;
    public bool Pointed {
        get { return pointed; }
        
        set {
            if (!pointed && value)
            {
                SetHighlight(value);
                pointed = value;
            }
            else if (pointed && !value)
            {
                SetHighlight(value);
                pointed = value;
            }
        } 
    }

    private void SetHighlight(bool active)
    {
        if (useHighlight)
        {
            if (active)
            {
                currentMat.SetColor("_OutlineColor", Color.cyan);
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
        gameObject.layer = LayerMask.GetMask("Grabbable");
        currentMat = GetComponent<Renderer>().material;
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

    






}
