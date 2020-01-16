/********************************************************************************//**
\file      Hand.cs
\brief     Basic hand impementation.
\copyright Copyright 2015 Oculus VR, LLC All Rights reserved.
************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using OVRTouchSample;
#if UNITY_EDITOR
#endif


public class Hand : MonoBehaviour
{
    protected enum Handside
    {
        Left = OVRPlugin.Controller.LTouch,
        Right = OVRPlugin.Controller.RTouch
    }
    [SerializeField] protected Handside handside;

    public const string ANIM_LAYER_NAME_POINT = "Point Layer";
    public const string ANIM_LAYER_NAME_THUMB = "Thumb Layer";
    public const string ANIM_PARAM_NAME_FLEX = "Flex";
    public const string ANIM_PARAM_NAME_POSE = "Pose";

    public const float INPUT_RATE_CHANGE = 20.0f;
    public const float TRIGGER_DEBOUNCE_TIME = 0.05f;
    public const float THUMB_DEBOUNCE_TIME = 0.15f;

    protected OVRInput.Controller controller;

    protected Dictionary<OVRInput.Controller, TouchController> inputs;

    [SerializeField]
    public Animator m_animator;
    [SerializeField]
    public HandPose defaultHandPose;
    protected HandPose currentPose;

    private int m_animLayerIndexThumb = -1;
    private int m_animLayerIndexPoint = -1;
    private int m_animParamIndexFlex = -1;
    private int m_animParamIndexPose = -1;

    private bool m_isPointing = false;
    private bool m_isGivingThumbsUp = false;
    private float m_pointBlend = 0.0f;
    private float m_thumbsUpBlend = 0.0f;

    protected bool updateAnim = true;
    protected bool canThumbsUp = true;

    protected float flex = 0;
    protected float index = 0;
    protected float pinch = 0;
    protected float point = 0;
    protected bool m_restoreOnInputAcquired = false;

    virtual protected void Start()
    {
        // Get animator layer indices by name, for later use switching between hand visuals
        m_animLayerIndexPoint = m_animator.GetLayerIndex(ANIM_LAYER_NAME_POINT);
        m_animLayerIndexThumb = m_animator.GetLayerIndex(ANIM_LAYER_NAME_THUMB);
        m_animParamIndexFlex = Animator.StringToHash(ANIM_PARAM_NAME_FLEX);
        m_animParamIndexPose = Animator.StringToHash(ANIM_PARAM_NAME_POSE);
        currentPose = defaultHandPose;


        //Sanity Checks
        if (!m_animator) { Debug.LogError("No Hand animator in Grabber Script."); return; }
        if (!currentPose) { Debug.LogError("No Hand pose in Grabber Script."); return; }
    }

    protected void Update()
    {
        flex = inputs[controller].HandTrigger;
        index = inputs[controller].IndexTrigger;
        m_isPointing = !inputs[controller].NearPrimaryIndexTrigger;

        m_isGivingThumbsUp = !(inputs[controller].NearButtons);

        m_pointBlend = InputValueRateChange(m_isPointing, m_pointBlend);
        m_thumbsUpBlend = InputValueRateChange(m_isGivingThumbsUp, m_thumbsUpBlend);
        pinch = inputs[controller].IndexTrigger;

        UpdateAnimStates();
    }

    private float InputValueRateChange(bool isDown, float value)
    {
        float rateDelta = Time.deltaTime * INPUT_RATE_CHANGE;
        float sign = isDown ? 1.0f : -1.0f;
        return Mathf.Clamp01(value + rateDelta * sign);
    }
    private void UpdateAnimStates()
    {
        if (updateAnim)
        {
            m_animator.SetInteger(m_animParamIndexPose, (int)currentPose.PoseId);
            m_animator.SetFloat(m_animParamIndexFlex, flex);


            point = m_pointBlend;
            m_animator.SetLayerWeight(m_animLayerIndexPoint, point);


            float thumbsUp = canThumbsUp ? m_thumbsUpBlend : 0.0f;
            m_animator.SetLayerWeight(m_animLayerIndexThumb, thumbsUp);



            m_animator.SetFloat("Pinch", pinch);
        }

    }
}

