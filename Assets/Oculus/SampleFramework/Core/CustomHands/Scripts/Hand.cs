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
using UnityEngine.SceneManagement;
#endif


public class Hand
{
    public const string ANIM_LAYER_NAME_POINT = "Point Layer";
    public const string ANIM_LAYER_NAME_THUMB = "Thumb Layer";
    public const string ANIM_PARAM_NAME_FLEX = "Flex";
    public const string ANIM_PARAM_NAME_POSE = "Pose";

    public const float INPUT_RATE_CHANGE = 20.0f;
    public const float TRIGGER_DEBOUNCE_TIME = 0.05f;
    public const float THUMB_DEBOUNCE_TIME = 0.15f;

    protected OVRInput.Controller m_controller = OVRInput.Controller.None;
    [SerializeField]
    public Animator m_animator { get; private set; }
    [SerializeField]
    public HandPose m_defaultGrabPose { get; private set; }

    private int m_animLayerIndexThumb = -1;
    private int m_animLayerIndexPoint = -1;
    private int m_animParamIndexFlex = -1;
    private int m_animParamIndexPose = -1;

    private bool m_isPointing = false;
    private bool m_isGivingThumbsUp = false;
    private float m_pointBlend = 0.0f;
    private float m_thumbsUpBlend = 0.0f;

    private bool m_restoreOnInputAcquired = false;

    private Grabber grabber;

    public Hand(Animator anim, HandPose grabPose, Grabber grabberComponent)
    {
        m_animator = anim;
        m_defaultGrabPose = grabPose;
        grabber = grabberComponent;
    }

    protected void Start()
    {
        // Get animator layer indices by name, for later use switching between hand visuals
        m_animLayerIndexPoint = m_animator.GetLayerIndex(ANIM_LAYER_NAME_POINT);
        m_animLayerIndexThumb = m_animator.GetLayerIndex(ANIM_LAYER_NAME_THUMB);
        m_animParamIndexFlex = Animator.StringToHash(ANIM_PARAM_NAME_FLEX);
        m_animParamIndexPose = Animator.StringToHash(ANIM_PARAM_NAME_POSE);
    }
  
    protected void Update()
    {
        m_pointBlend = InputValueRateChange(m_isPointing, m_pointBlend);
        m_thumbsUpBlend = InputValueRateChange(m_isGivingThumbsUp, m_thumbsUpBlend);

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
       
        bool grabbing = grabber.GetGrabbedObject() != null;
        HandPose grabPose = m_defaultGrabPose;
        if (grabbing)
        {
            HandPose customPose = grabber.GetGrabbedObject().GetComponent<HandPose>();
            if (customPose != null) grabPose = customPose;
        }
        // Pose
        HandPoseId handPoseId = grabPose.PoseId;
        m_animator.SetInteger(m_animParamIndexPose, (int)handPoseId);

        // Flex
        // blend between open hand and fully closed fist
        float flex = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, m_controller);

        ///Need to add check to add our restrictions for the hand closing movement (ie.hand closing around tower)
        ///**check if something is grabbed, if so get its flex restriction ammount and "Clamp" flex to it**

        flex = Mathf.Clamp(flex, 0, .15f);

        m_animator.SetFloat(m_animParamIndexFlex, flex);

        // Point
        if (!grabbing)
        {
            bool canPoint = !grabbing || grabPose.AllowPointing;
            float point = canPoint ? m_pointBlend : 0.0f;
            m_animator.SetLayerWeight(m_animLayerIndexPoint, point);
        }

        // Thumbs up
        if (!grabbing)
        {
            bool canThumbsUp = !grabbing || grabPose.AllowThumbsUp;
            float thumbsUp = canThumbsUp ? m_thumbsUpBlend : 0.0f;
            m_animator.SetLayerWeight(m_animLayerIndexThumb, thumbsUp);
        }

        float pinch = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, m_controller);
        m_animator.SetFloat("Pinch", pinch);
    }
}

