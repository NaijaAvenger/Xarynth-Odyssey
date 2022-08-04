using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VRMap
{
    public Transform m_vrTarget;
    public Transform m_rigTarget;
    public Vector3 m_trackingPositionOffset;
    public Vector3 m_trackingRotationOffset;

    public void VRMapping()
    {
        m_rigTarget.position = m_vrTarget.TransformPoint(m_trackingPositionOffset);
        m_rigTarget.rotation = m_vrTarget.rotation * Quaternion.Euler(m_trackingRotationOffset);
    }
}

public class VRRig : MonoBehaviour
{
    public VRMap m_head;
    public VRMap m_leftHand;
    public VRMap m_rightHand;

    public Transform m_headConstraint;
    public Vector3 m_headBodyOffset;
    public float m_turnSmoothness;

    // Start is called before the first frame update
    void Start()
    {
        m_headBodyOffset = transform.position - m_headConstraint.position;
        m_turnSmoothness = 2f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = m_headBodyOffset + m_headConstraint.position;
        transform.forward = Vector3.Lerp(transform.forward,
            Vector3.ProjectOnPlane(m_headConstraint.forward, Vector3.up).normalized, Time.deltaTime * m_turnSmoothness);

        m_head.VRMapping();
        m_leftHand.VRMapping();
        m_rightHand.VRMapping();
    }
}
