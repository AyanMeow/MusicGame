using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timeBug : MonoBehaviour
{
    public GameObject Tb;
    
    private Text timeText;
    AudioSource musicT;
    // Start is called before the first frame update
    void Start()
    {
        timeText = Tb.GetComponent<Text>();
        musicT = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        var c = Input.touchCount;
        timeText.text = "now touch: " + c.ToString();

    }
}
