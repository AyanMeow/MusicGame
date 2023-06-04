using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;
using UnityEngine.UI;

public class 游戏总控 : MonoBehaviour
{
    public bool setDemonstration;
    public static bool setTextOnOff = false;

    public GameObject videoCt;
    public GameObject shootManger;
    public GameObject musicCt;
    public GameObject blackImage;
    public GameObject pasuePanel;
    public GameObject pasueButton;

    public GameObject[] textsNeed;
    public GameObject[] spriteNeed;

    public static bool gameStop = false;

    string sceneName = "选择歌曲";
    RawImage black;
    AudioSource musicPlaying;
    AudioClip musicSong;
    int timeCount = 3;
    float wait = 2f;
    float count_w = 0f;
    bool startEnd = false;
    // Start is called before the first frame update

    private void Awake()
    {
        setDemonstration = Setting.openDemo;
    }
    void Start()
    {
        gameStop = false;
        black = blackImage.GetComponent<RawImage>();
        musicPlaying = musicCt.GetComponent<AudioSource>();
        musicSong = musicPlaying.clip;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStop)
        {
            //Debug.Log("游戏应该要停止了！");
            shootManger.GetComponent<AudioSource>().Stop();
            musicCt.GetComponent<AudioSource>().Stop();
            videoCt.GetComponent<UnityEngine.Video.VideoPlayer>().Stop();
        }

        float playDegree = musicPlaying.time / musicSong.length;
        if (playDegree >= 0.99)
        {
            startEnd = true;
        }
        if (startEnd&&count_w<wait)
        {
            //Debug.Log("准备衰弱了！:"+count_w);
            count_w += 1f * Time.deltaTime;
        }
        if (count_w>=wait)
        {
            //Debug.Log("正在衰减");
            black.enabled = true;
            black.color = Color.Lerp(black.color, Color.black, 2f * Time.deltaTime);
            if (spriteNeed.Length > 0)
            {
                foreach(var s in spriteNeed)
                {
                    s.GetComponent<SpriteRenderer>().color= Color.Lerp(s.GetComponent<SpriteRenderer>().color, Color.black, 4f * Time.deltaTime);
                }
            }
            if (spriteNeed.Length > 0)
            {
                foreach (var s in textsNeed)
                {
                    s.GetComponent<TextMesh>().color = Color.Lerp(s.GetComponent<TextMesh>().color, Color.black, 4f * Time.deltaTime);
                }
            }
            if (black.color.a > 0.98)
            {
                Debug.Log("准备加载场景：" + sceneName);
                场景切换控制.GoToSceneByName(sceneName);
            }
        }

    }

    public static bool getTextOnOff()
    {
        return setTextOnOff;
    }

    public void pause()
    {
        shootManger.GetComponent<AudioSource>().Pause();
        musicCt.GetComponent<AudioSource>().Pause();
        videoCt.GetComponent<UnityEngine.Video.VideoPlayer>().Pause();
        Time.timeScale = 0;
        pasueButton.SetActive(false);
        pasuePanel.SetActive(true);
        //List<Koreography> Kore_list = new List<Koreography>();
        //shootManger.GetComponent<Koreographer>().GetKoreographyAtIndex(0);
        this.timeCount = 3;

    }

    public void reverse()
    {
        pasuePanel.SetActive(false);
        //ChangeTime();
        afterPause();
    }

    private IEnumerator ChangeTime()
    {

        while (timeCount > 0)
        {
            yield return new WaitForSeconds(1);// 每次 自减1，等待 1 秒
            timeCount--;
            Debug.Log("timeCount:" + timeCount);
        }

        afterPause();
    }

    private void afterPause()
    {
        shootManger.GetComponent<AudioSource>().Play();
        musicCt.GetComponent<AudioSource>().Play();
        //List<Koreography> Kore_list = new List<Koreography>();
        //shootManger.GetComponent<Koreographer>().GetKoreographyAtIndex(0);
        videoCt.GetComponent<UnityEngine.Video.VideoPlayer>().Play();
        Time.timeScale = 1;
        pasueButton.SetActive(true);
    }
    
    public static void setGameStop(bool status)
    {
        gameStop = status;
    }
}



