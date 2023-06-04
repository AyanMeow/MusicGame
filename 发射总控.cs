using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;
using UnityEngine.UI;

public class 发射总控 : MonoBehaviour
{
    public GameObject[] shooter;
    public string eventID;
    public float blockSpeed = 150;
    public GameObject[] blockPrefabs;
    public GameObject musicPanel;
    public float musicDelay_second;
    public Slider musicSlider;

    AudioSource audioControl, music;
    AudioClip currentMusic;

    AnimationCurve curveLast=null;
 
    void Awake()
    {
        Koreographer.Instance.RegisterForEvents(eventID, musicEventRespon);
        audioControl = GetComponent<AudioSource>();
        music = musicPanel.GetComponent<AudioSource>();
        //musicDelay_second = 1.5f * blockSpeed / 150;
    }
    // Start is called before the first frame update
    void Start()
    {
        audioControl.Play();
        music.PlayDelayed(musicDelay_second);
        music.volume = Setting.musicVolum;
        //Debug.Log("music volume: " + music.volume);
        currentMusic = music.clip;
    }

    // Update is called once per frame
    void Update()
    {
        if (music.isPlaying)
        {
            var temp = music.time / currentMusic.length;
            musicSlider.value = temp;
        }
    }

    void musicEventRespon(KoreographyEvent musicEvent)
    {

        if (musicEvent.HasFloatPayload())
        {
            float payload = musicEvent.GetFloatValue();
            int shooterNum = (int)payload;
            int blockType = (int)((payload - shooterNum) * 100);
            if (shooterNum < shooter.Length&&payload>=0) 
            {
                shooter[shooterNum].GetComponent<发射器>().blockGenerate(blockPrefabs[0], blockSpeed);
            }
        }
        if (musicEvent.HasCurvePayload())
        {
            var payload = musicEvent.GetCurveValue();
            int number = (int)payload.keys[0].value;
            //payload.RemoveKey(0);
            //Debug.Log("key length:" + payload.length);
            if (curveLast == null || curveLast != payload)
            {
                curveLast = payload;
                shooter[number].GetComponent<发射器>().blockGenerate(blockPrefabs[3], blockSpeed);
            }
            else
            {
                shooter[number].GetComponent<发射器>().blockGenerate(blockPrefabs[4], blockSpeed);
            }
        }
        if (musicEvent.HasIntPayload())
        {
            //Debug.Log("int payload");
            int payload = musicEvent.GetIntValue();
            int t1 = (int)payload % 100;
            int t2 = (int)payload / 100;
            GameObject b1 = shooter[t1].GetComponent<发射器>().blockGenerate(blockPrefabs[0], blockSpeed);
            GameObject b2 = shooter[t2].GetComponent<发射器>().blockGenerate(blockPrefabs[0], blockSpeed);
            //b1.GetComponent<blockmove>().brother = b2;
            //b2.GetComponent<blockmove>().brother = b1;
        }
        if (musicEvent.HasTextPayload())
        {
            string textPayload = musicEvent.GetTextValue();
            int target = 0;
            for(int i = 0; i < textPayload.Length; i++)
            {
                target *= 10;
                target += (int)(textPayload[i] - '0');
            }
            //Debug.Log("Text payload:" + target);
            if (target < shooter.Length && target >= 0)
            {
                shooter[target].GetComponent<发射器>().blockGenerate(blockPrefabs[2], blockSpeed);
            }
        }
    }

    public float getMoveSpeed()
    {
        return this.blockSpeed;
    }
}
