using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class KeyBoard : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject normalButton;
    public GameObject capsButton;
    public bool caps;

    // Start is called before the first frame update
    void Start()
    {
        caps = false;
    }

    // Update is called once per frame
    public void insertChar(string c)
    {
        inputField.text += c;
    }

    public void deleteChar(string c)
    {
        if (inputField.text.Length > 0)
        {
            inputField.text =inputField.text.Substring(0, inputField.text.Length - 1); 
        }
    }

    public void insertSpace()
    {
        inputField.text += " ";
    }

    public void CapsPressed()
    {
        if(!caps)
        {
            normalButton.SetActive(true);
            capsButton.SetActive(false);
            caps = true;
        }
        else
        {
            capsButton.SetActive(true);
            normalButton.SetActive(false);
            caps = false;
        }
    }
}
