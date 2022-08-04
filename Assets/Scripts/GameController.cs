using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Handness
{
    Left, Right
}
public class GameController : MonoBehaviour
{
    [SerializeField] private Handness m_hand1 = Handness.Right;
    [SerializeField] private Handness m_hand2 = Handness.Left;
    public float m_playerHealth = 100f;
    public float m_playerOxygen = 100f;
    public float m_scannedCount = 0f;
    public float m_scannedCount2 = 0f;
    public float m_materialCount = 0f;
    public float m_materialCount2 = 0f;
    public float m_shards = 0f;
    public float m_materials = 0f;
    [SerializeField] public TextMeshProUGUI m_playerHealthCanvas;
    [SerializeField] public TextMeshProUGUI m_playerOxygenCanvas;
    [SerializeField] public TextMeshProUGUI m_objectsScannedCount;
    [SerializeField] public TextMeshProUGUI m_objectsScannedCount2;
    [SerializeField] public TextMeshProUGUI m_objectCollectedCount;
    [SerializeField] public TextMeshProUGUI m_objectCollectedCount2;
    [SerializeField] public TextMeshProUGUI m_playerShards;
    private string m_primary;
    private string m_secondary;
    private string m_primary2;
    private string m_secondary2;
    private string m_trigger;
    private string m_trigger2;
    public bool menuOpened;
    public GameObject menuCanvas;
    public float timeSecond = 3f;
    public float timeSecond2 = 3f;
    public bool missionSelector;
    public bool missionComplete = false;
    public bool missionComplete2 = false;
    public bool shardChange = false;
    public bool materialChange = false;
    public GameObject missionActivator;
    public GameObject objectTracker;
    public GameObject objectTracker2;
    public GameObject materialTracker;
    public GameObject materialTracker2;
    public GameObject interactionText;
    public GameObject interactionText2;
    public Transform myPlayer;
    public GameObject mission1Canvas;
    public GameObject mission1Intro;
    public GameObject mission1Outro;
    public GameObject mission2Canvas;
    public GameObject mission2Intro;
    public GameObject mission2Outro;
    [SerializeField] public Transform mission1;
    [SerializeField] public Transform mission2;
    [SerializeField] public Transform mission3;
    [SerializeField] public Transform mission4;
    public ScienceMission scienceCheck;
    public GeologyMission geologycheck;
    public Jetpack jetpack;
    public List<GameObject> scanObjects = new List<GameObject>();
    public List<GameObject> scanObjects2 = new List<GameObject>();
    public List<GameObject> materialObjects = new List<GameObject>();
    public List<GameObject> materialObjects2 = new List<GameObject>();

    public AudioClip coinAudio;
    public AudioSource coinSource;
    // Start is called before the first frame update 
    public void Start()
    {
        materialTracker2.SetActive(false);  
        m_primary = "XRI_" + m_hand1 + "_PrimaryButton";
        m_secondary = "XRI_" + m_hand1 + "_SecondaryButton";
        m_primary2 = "XRI_" + m_hand2 + "_PrimaryButton";
        m_secondary2 = "XRI_" + m_hand2 + "_SecondaryButton";
        m_trigger = "XRI_" + m_hand1 + "_TriggerButton";
        m_trigger2 = "XRI_" + m_hand2 + "_TriggerButton";
        missionSelector = false;
        menuOpened = true;

        transform.position = myPlayer.position;

        for (int i = 0; i < scanObjects.Count; i++)
        {
            scanObjects[i].gameObject.SetActive(false);
        }

        interactionText.gameObject.SetActive(false);
        mission1Outro.gameObject.SetActive(false);
        interactionText2.gameObject.SetActive(false);
        mission2Outro.gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void Update()
    {
        ScienceMission1();
        GeologyMission2();

        if (Input.GetButtonDown(m_primary) || Input.GetButtonDown(m_primary2))
        {
            menuOpened = !menuOpened;
        }

        if (Input.GetButtonDown(m_secondary) || Input.GetButtonDown(m_secondary2))
        {
            missionSelector = !missionSelector;
        }

        if (menuOpened)
        {
            menuCanvas.gameObject.SetActive(true);
        }
        if (!menuOpened)
        {
            menuCanvas.gameObject.SetActive(false);
        }

        if (missionSelector)
        {
            missionActivator.SetActive(true);
        }
        else if (!missionSelector)
        {
            missionActivator.SetActive(false);
        }

        if (Time.time >= timeSecond)
        {
            m_playerOxygen -= 1f;
            timeSecond += 3f;
        }

        if (Time.time >= timeSecond2 && geologycheck.jetpackActive && jetpack.slowFall)
        {
            m_materialCount2 -= 1f;
            timeSecond2 += 1f;
        }

        m_playerHealthCanvas.text = "Health: " + m_playerHealth.ToString();
        m_playerOxygenCanvas.text = "Oxygen Level: " + m_playerOxygen.ToString();
        m_objectsScannedCount.text = "Objects Scanned: " + m_scannedCount.ToString();
        m_objectsScannedCount2.text = "Objects Scanned: " + m_scannedCount2.ToString();
        m_playerShards.text = "Shards: " + m_shards.ToString();
        m_objectCollectedCount.text = "Materials Collected: " + m_materialCount.ToString();
        m_objectCollectedCount2.text = "Materials Collected: " + m_materialCount2.ToString();
    }

    public void GeologyMission2()
    {
        if (geologycheck.geologyMissionStart)
        {
            mission2Canvas.SetActive(true);
        }
        else if (!geologycheck.geologyMissionStart)
        {
            mission2Canvas.SetActive(false);
        }

        if (geologycheck.collectableObjects)
        {
            materialTracker.SetActive(true);
            for (int i = 0; i < materialObjects.Count; i++)
            {
                materialObjects[i].gameObject.SetActive(true);
            }
        }
        else if (!geologycheck.collectableObjects)
        {
            materialTracker.SetActive(false);
            for (int i = 0; i < scanObjects.Count; i++)
            {
                materialObjects[i].gameObject.SetActive(false);
            }
        }

        if (geologycheck.mission2IntroTxt == true)
        {
            mission2Intro.SetActive(true);
        }
        else if (geologycheck.mission2IntroTxt == false)
        {
            mission2Intro.SetActive(false);
        }

        if (m_materialCount == 11 && !missionComplete2)
        {
            missionComplete2 = true;
        }

        if (m_materialCount == 11 && missionComplete2)
        {
            mission2Outro.SetActive(true);
        }
        else
        {
            mission2Outro.SetActive(false);
        }

        if (geologycheck.worldCollectObjects)
        {
            materialTracker2.SetActive(true);

            for (int i = 0; i < materialObjects2.Count; i++)
            {
                materialObjects2[i].gameObject.SetActive(true);
            }
        }
        else if (!geologycheck.worldCollectObjects)
        {
            materialTracker2.SetActive(false);

            for (int i = 0; i < materialObjects2.Count; i++)
            {
                materialObjects2[i].gameObject.SetActive(false);
            }
        }
    }

    public void ScienceMission1()
    {
        if (scienceCheck.scienceMissionStart)
        {
            mission1Canvas.SetActive(true);
        }
        else if (!scienceCheck.scienceMissionStart)
        {
            mission1Canvas.SetActive(false);
        }

        if (scienceCheck.scannableObjects)
        {
            objectTracker.SetActive(true);
            for (int i = 0; i < scanObjects.Count; i++)
            {
                scanObjects[i].gameObject.SetActive(true);
            }
        }
        else if (!scienceCheck.scannableObjects)
        {
            objectTracker.SetActive(false);
            for (int i = 0; i < scanObjects.Count; i++)
            {
                scanObjects[i].gameObject.SetActive(false);
            }
        }

        if (scienceCheck.missionIntroTxt == true)
        {
            mission1Intro.SetActive(true);
        }
        else if (scienceCheck.missionIntroTxt == false)
        {
            mission1Intro.SetActive(false);
        }

        if (m_scannedCount == 11 && !missionComplete)
        {
            missionComplete = true;
        }

        if (m_scannedCount == 11 && missionComplete)
        {
            mission1Outro.SetActive(true);
        }
        else
        {
            mission1Outro.SetActive(false);
        }

        if (scienceCheck.worldScanObjects)
        {
            objectTracker2.SetActive(true);

            for (int i = 0; i < scanObjects2.Count; i++)
            {
                scanObjects2[i].gameObject.SetActive(true);
            }
        }
        else if (!scienceCheck.worldScanObjects)
        {
            objectTracker2.SetActive(false);

            for (int i = 0; i < scanObjects2.Count; i++)
            {
                scanObjects2[i].gameObject.SetActive(false);
            }
        }
    }

    public void Mission1()
    {
        myPlayer.position = new Vector3(mission1.transform.position.x, mission1.transform.position.y, mission1.transform.position.z);
    }
    public void Mission2()
    {
        myPlayer.position = new Vector3(mission2.transform.position.x, mission2.transform.position.y, mission2.transform.position.z);
    }
    public void Mission3()
    {
        myPlayer.position = new Vector3(mission3.transform.position.x, mission3.transform.position.y, mission3.transform.position.z);
    }
    public void Mission4()
    {
        myPlayer.position = new Vector3(mission4.transform.position.x, mission4.transform.position.y, mission4.transform.position.z);
    }

    public void CloseMenu()
    {
        menuOpened = false;
    }

    public void CloseSelector()
    {
        missionSelector = false;
    }

    public void mission1OutroClose()
    {
        m_scannedCount = 0;
        m_scannedCount2 = 0;
    }

    public void mission2OutroClose()
    {
        m_materialCount = 0;
        m_materialCount2 = 11;
    }

    public void shardsInventory(float shardValue)
    {
        coinSource.PlayOneShot(coinAudio);
        m_shards += shardValue;
    }

    public void materialsInventory(float materialValue)
    {
        coinSource.PlayOneShot(coinAudio);
        m_materials += materialValue;
    }
}
