using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScienceMission : MonoBehaviour
{
    [SerializeField] private Handness m_hand1 = Handness.Right;
    [SerializeField] private Handness m_hand2 = Handness.Left;
    public GameController controller;
    public GameObject mission1ExitText;
    public bool scienceMissionStart = false;
    public bool missionComplete1 = false;
    public bool scannableObjects = false;
    public bool missionIntroTxt = false;
    public bool worldScanObjects = false;
    private string m_grip;
    private string m_grip2;

    // Start is called before the first frame update
    void Start()
    {
        mission1ExitText.SetActive(false);
        m_grip = "XRI_" + m_hand1 + "_GripButton";
        m_grip2 = "XRI_" + m_hand2 + "_GripButton";
    }

    // Update is called once per frame
    public void Update()
    {
        if (scienceMissionStart == true && Input.GetButtonDown(m_grip) || scienceMissionStart == true && Input.GetButtonDown(m_grip2))
        {
            scannableObjects = true;
            missionIntroTxt = true;
            scienceMissionStart = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
       if(other.CompareTag("Player") && !controller.missionComplete && !scannableObjects)
        {
            scienceMissionStart = true;
        }

        if (other.CompareTag("Player") && controller.missionComplete && !missionComplete1)
        {
            worldScanObjects = true;
            scannableObjects = false;
            mission1ExitText.SetActive(true);
            controller.shardsInventory(11);
            missionComplete1 = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        
    }

    public void CloseIntro()
    {
        missionIntroTxt = false;
    }

    public void mission1Closing()
    {
        mission1ExitText.SetActive(false);
    }
}
