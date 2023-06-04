using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 音效控制 : MonoBehaviour
{
    //public static float volumeBase=0.3f;
    static AudioSource hitAudio;

    private void Awake()
    {
        hitAudio = gameObject.GetComponent<AudioSource>();
        hitAudio.volume = Setting.touchVolum;
        //Debug.Log("touch volume: " + hitAudio.volume);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void playsoud()
    {
        hitAudio.Play();
    }

}
