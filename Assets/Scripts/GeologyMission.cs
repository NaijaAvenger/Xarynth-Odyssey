using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeologyMission : MonoBehaviour
{
    [SerializeField] private Handness m_hand1 = Handness.Right;
    [SerializeField] private Handness m_hand2 = Handness.Left;
    public GameController controller;
    public GameObject mission2ExitText;
    public GameObject jetpackIntro;
    public bool jetpackActive = false;
    public bool geologyMissionStart = false;
    public bool missionComplete2 = false;
    public bool collectableObjects = false;
    public bool mission2IntroTxt = false;
    public bool worldCollectObjects = false;
    private string m_grip;
    private string m_grip2;

    // Start is called before the first frame update
    void Start()
    {
        mission2ExitText.SetActive(false); 
        jetpackIntro.SetActive(false);
        m_grip = "XRI_" + m_hand1 + "_GripButton";
        m_grip2 = "XRI_" + m_hand2 + "_GripButton";
    }

    // Update is called once per frame
    public void Update()
    {
        if (geologyMissionStart == true && Input.GetButtonDown(m_grip) || geologyMissionStart == true && Input.GetButtonDown(m_grip2))
        {
            collectableObjects = true;
            mission2IntroTxt = true;
            geologyMissionStart = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !controller.missionComplete2 && !collectableObjects)
        {
            geologyMissionStart = true;
        }

        if (other.CompareTag("Player") && controller.missionComplete2 && !missionComplete2)
        {
            worldCollectObjects = true;
            collectableObjects = false;
            mission2ExitText.SetActive(true);
            controller.materialsInventory(11);
            missionComplete2 = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {

    }

    public void CloseIntro()
    {
        mission2IntroTxt = false;
    }

    public void mission2Closing()
    {
        mission2ExitText.SetActive(false);
    }

    public void JetpackActive()
    {
        jetpackActive = true;
    }
}

