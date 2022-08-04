using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanDownloader : MonoBehaviour
{
    public GameController controller;
    public GameObject scanAcceptText;
    public GameObject downloadInteraction;
    [SerializeField] private Handness m_hand1 = Handness.Right;
    [SerializeField] private Handness m_hand2 = Handness.Left;
    private string m_grip;
    private string m_grip2;
    public bool scanDownloaderInteraction = false;

    public AudioClip downloadAudio;
    public AudioSource downloadSource;
    // Start is called before the first frame update
    void Start()
    {
        scanAcceptText.SetActive(false);
        downloadInteraction.SetActive(false);
        m_grip = "XRI_" + m_hand1 + "_GripButton";
        m_grip2 = "XRI_" + m_hand2 + "_GripButton";
    }

    // Update is called once per frame
    void Update()
    {
        if (scanDownloaderInteraction && Input.GetButtonDown(m_grip) || scanDownloaderInteraction && Input.GetButtonDown(m_grip2))
        {
            downloadSource.PlayOneShot(downloadAudio);
            scanAcceptText.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && controller.scienceCheck.worldScanObjects)
        {
            scanDownloaderInteraction = true;
            downloadInteraction.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        scanDownloaderInteraction = false;
        downloadInteraction.SetActive(false);
    }

    public void ExchangeScans()
    {
            controller.m_shards += controller.m_scannedCount2;
            controller.m_scannedCount2 = 0;
    }
}
