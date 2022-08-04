using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDKeys : MonoBehaviour
{
    private IDKeySounds soundHandler;
    public bool keyHit = false;
    public bool keyCanBeHitAgain = false;

    private float originalPosition;
    // Start is called before the first frame update
    void Start()
    {
        soundHandler = GameObject.FindGameObjectWithTag("SoundHandler").GetComponent<IDKeySounds>();
        originalPosition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(keyHit)
        {
            soundHandler.PlayKeyClick();
            keyCanBeHitAgain = false;
            keyHit = false;
            transform.position += new Vector3(0, -0.03f, 0);
        }
        if (transform.position.y < originalPosition)
        {
            transform.position += new Vector3(0, 0.005f, 0);
        }
        else
        {
            keyCanBeHitAgain = true;
        }
    }
}
