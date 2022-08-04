using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KeyboardButton : MonoBehaviour
{
    KeyBoard keyboard;
    TextMeshProUGUI buttonText;

    // Start is called before the first frame update
    void Start()
    {
        keyboard = GetComponentInParent<KeyBoard>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText.text.Length == 1)
        {
            NameToButtonText();
            GetComponentInChildren<Button>().onClick.AddListener( delegate { keyboard.insertChar(buttonText.text); } );
        }

    }

    public void NameToButtonText()
    {
        buttonText.text = gameObject.name;
    }
}
