using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IDKeyDetector : MonoBehaviour
{
    [SerializeField] TextMeshPro playerTextOutput;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        var key = other.GetComponentInChildren<TextMeshPro>();
        if (key != null)
        {
            if (other.gameObject.GetComponent<IDKeys>().keyCanBeHitAgain)
            {
                if (key.text == "SPACE")
                {
                    playerTextOutput.text += " ";
                }
                else if (key.text == "<-")
                {
                    playerTextOutput.text = playerTextOutput.text.Substring(0, playerTextOutput.text.Length - 1);
                }
                else
                {
                    playerTextOutput.text += key.text;
                }
                var IDKeys = other.gameObject.GetComponent<IDKeys>();
                IDKeys.keyHit = true;
            }
        }

    }
}
