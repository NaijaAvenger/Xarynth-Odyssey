using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMaterials : MonoBehaviour
{
    [SerializeField] private Handness m_hand1 = Handness.Right;
    [SerializeField] private Handness m_hand2 = Handness.Left;
    private string m_grip;
    private string m_grip2;
    public bool collectInteraction = false;
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
        if (collectInteraction == true && Input.GetButtonDown(m_grip) || collectInteraction == true && Input.GetButtonDown(m_grip2))
        {
            collectInteraction = false;
            controller.interactionText2.SetActive(false);
            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<MeshCollider>().enabled = false;
            gameObject.GetComponentInChildren<ParticleSystem>().Stop();
            zapSource.PlayOneShot(zapAudio);

            if (controller.geologycheck.worldCollectObjects)
            {
                controller.m_materialCount2++;
            }
            else if (!controller.geologycheck.worldCollectObjects)
            {
                controller.m_materialCount++;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameObject.activeInHierarchy == true && gameObject.GetComponent<Renderer>().enabled == true)
        {
            collectInteraction = true;
            controller.interactionText2.SetActive(true);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        collectInteraction = false;
        controller.interactionText2.SetActive(false);
    }
}
