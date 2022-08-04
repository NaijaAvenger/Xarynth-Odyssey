using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkPLayer : MonoBehaviour
{
    private PhotonView photonView;

    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    public Animator leftHandAnimator;
    public Animator rightHandAnimator;

    public Transform headRig;
    public Transform leftHandRig;
    public Transform rightHandRig;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();

        XROrigin rig = FindObjectOfType<XROrigin>();
        headRig = rig.transform.Find("CameraOffset/XRCamera");
        leftHandRig = rig.transform.Find("CameraOffset/LeftHand Controller");
        rightHandRig = rig.transform.Find("CameraOffset/RightHand Controller");

        if(photonView.IsMine)
        {
            foreach (var item in GetComponentsInChildren<Renderer>())
            {
                if(item.gameObject.tag == "Head")
                {
                    item.enabled = false;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            MapPosition(head, headRig);
            MapPosition(leftHand, leftHandRig);
            MapPosition(rightHand, rightHandRig);

            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.LeftHand), leftHandAnimator);
            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.RightHand), rightHandAnimator);
        }

    }

    void UpdateHandAnimation(InputDevice targetDevice, Animator handAnimator)
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }


    void MapPosition(Transform target, Transform rigTransform)
    {
        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation;
    }
}
