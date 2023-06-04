using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class 音乐弹奏 : MonoBehaviour
{
    public string eventID;

    void Awake()
    {
        Koreographer.Instance.RegisterForEvents(eventID, musicEventRespon);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void musicEventRespon(KoreographyEvent musicEvent)
    {
        print("test.1");
    }
}
