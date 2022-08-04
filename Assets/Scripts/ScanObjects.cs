using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanObjects : MonoBehaviour
{
    [SerializeField] private Handness m_hand1 = Handness.Right;
    [SerializeField] private Handness m_hand2 = Handness.Left;
    private string m_grip;
    private string m_grip2;
    public bool scanInteraction = false;
    public GameController controller;

    public AudioClip zapAudio;
    public AudioSource zapSource;
    // Start is called before the first frame update
    void Start()
    {
        m_grip = "XRI_" + m_hand1 + "_GripButton";
        m_grip2 = "XRI_" + m_hand2 + "_GripButton";
    }

    // Update is called once per frame
    void Update()
    {
        if (scanInteraction == true && Input.GetButtonDown(m_grip) || scanInteraction == true && Input.GetButtonDown(m_grip2))
        {
            scanInteraction = false;
            controller.interactionText.SetActive(false);
            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<MeshCollider>().enabled = false;
            gameObject.GetComponentInChildren<ParticleSystem>().Stop();
            zapSource.PlayOneShot(zapAudio);

            if(controller.scienceCheck.worldScanObjects)
            {
                controller.m_scannedCount2++;
            }
            else if(!controller.scienceCheck.worldScanObjects)
            {
                controller.m_scannedCount++;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameObject.activeInHierarchy == true && gameObject.GetComponent<Renderer>().enabled == true)
        {
            scanInteraction = true;
            controller.interactionText.SetActive(true);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        scanInteraction = false;
        controller.interactionText2.SetActive(true);
    }
}
