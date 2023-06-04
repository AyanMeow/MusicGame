using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class onDrag : MonoBehaviour
{
    public RectTransform viewport;
    public RectTransform[] elements;
    public RectTransform centerPoint;
    public RectTransform content;

    public GameObject stageNameObject;
    public AudioClip[] musics;
    public GameObject blackImage;
    public GameObject Trangle;
    public Texture[] textureT;
    public GameObject Squad;
    public Texture[] textureS;
    public GameObject rightArrow;
    public GameObject leftArrow;

    public static string stageSceneLoad;


    private Vector2 newPos;
    private float distanceBetweenEles=1041f;
    private float[] distanceToCenter;
    private int minEleNum;
    private bool isDragging = false;
    private bool fade = false;
    private TextMesh stageName;

    private string[] stageText = {"造\n天\n造\n地",
                                  "安\n营\n扎\n寨"
    };
    private string[] stageScene = {"关卡1",
                                  "关卡2"
    };

    AudioSource Asource;
    float musicfadespeed=1f;
    bool minChange = false;
    bool buttonMove = false;
    // Use this for initialization
    void Start()
    {

        int elelength = elements.Length;
        distanceToCenter = new float[elelength];
        //获得元素间隔距离
        //distanceBetweenEles = Mathf.Abs(elements[1].rect.x - elements[0].rect.x);
         //Debug.Log("distance: " + distanceBetweenEles);

        stageName = stageNameObject.GetComponent<TextMesh>();
        Asource = gameObject.GetComponent<AudioSource>();
        Asource.clip = musics[0];
        Asource.Stop();
        
    }


    void Update()
    {
        var blackEnable = blackImage.GetComponent<RawImage>().enabled;
        //Debug.Log("minE: " + minEleNum);
        //当前位置跟计算得到吸附位置接近的时候不再进行计算最小位置
        if (buttonMove)
        {
            //Debug.Log("mnum: " + minEleNum);
            isDragging = false;
            minChange = true;
            if (Vector2.Distance(content.anchoredPosition,newPos)<=5f)
            {
                buttonMove = false;
                minChange = true;
            }
        }
        else
        {
            if (Vector2.Distance(content.anchoredPosition, newPos) > 5f && isDragging)
            {
                for (int i = 0; i < elements.Length; i++)
                {
                    //得到每个元素到中心点的距离
                    distanceToCenter[i] = Mathf.Abs(centerPoint.transform.position.x - elements[i].transform.position.x);
                }
                //获得最近距离
                float minDist = Mathf.Min(distanceToCenter);

                for (int i = 0; i < elements.Length; i++)
                {
                    //找到最小距离的元素索引
                    if (minDist == distanceToCenter[i])
                    {
                        var temp = minEleNum;
                        minEleNum = i;
                        if (minEleNum != temp)
                            minChange = true;
                    }
                }
            }
        }
        if (minEleNum == 0)
        {
            rightArrow.SetActive(true);
            leftArrow.SetActive(false);
        }else if (minEleNum == elements.Length - 1)
        {
            rightArrow.SetActive(false);
            leftArrow.SetActive(true);
        }
        else
        {
            rightArrow.SetActive(true);
            leftArrow.SetActive(true);
        }

        if (isDragging&&!blackEnable)
        {
            Asource.volume = Mathf.Lerp(Asource.volume, 0f, musicfadespeed * Time.deltaTime);
            //Debug.Log("volume : " + Asource.volume);
            if (Asource.volume <= 0.4)
            {
                Asource.Stop();
                Asource.clip = musics[minEleNum];
                if (minChange)
                {
                    minChange = false;
                }
            }
        }
        if (!isDragging)
        {
            //当前没有拖拽，自动吸附居中
            newPos = new Vector2(-520f+(minEleNum-1) * -distanceBetweenEles, viewport.anchoredPosition.y);
            //Debug.Log("new pos: " + newPos);
            //Debug.Log("minE: " + minEleNum);
            content.anchoredPosition = Vector2.Lerp(content.anchoredPosition, newPos, Time.deltaTime * 2f);
            stageName.text = stageText[minEleNum];
            stageSceneLoad = stageScene[minEleNum];
            Trangle.GetComponent<RawImage>().texture = textureT[minEleNum];
            Squad.GetComponent<RawImage>().texture = textureS[minEleNum];

            if (minChange)
            {
                Asource.Stop();
                Asource.clip = musics[minEleNum];
                minChange = false;
            }
            if (!Asource.isPlaying&&!blackEnable)
            {
                Asource.Play();
                Asource.volume = 0f;
            }
            if (!blackEnable && !fade)
            {
                Asource.volume = Mathf.Lerp(Asource.volume, 1.0f, musicfadespeed/3 * Time.deltaTime);
            }
                

            if (Asource.time / Asource.clip.length > 0.1)
            {
                fade = true;
                Asource.volume = Mathf.Lerp(Asource.volume, 0.0f, musicfadespeed * Time.deltaTime);
                if (Asource.volume <= 0.1)
                {
                    Asource.Stop();
                    fade = false;
                }
            }

        }
        if (blackEnable)
        {
            Asource.volume = Mathf.Lerp(Asource.volume, 0.0f, musicfadespeed * Time.deltaTime);
            if (Asource.volume <= 0.1)
            {
                Asource.Stop();
            }
        }
    }

    public void ButtonSweep(int towords)
    {
        //Debug.Log("button move");
        if (towords == 1)
        {
            minEleNum++;
        }
        else if(towords == 0){
            minEleNum--;
        }
        buttonMove = true;
    }


    public void StartDrag()
    {
        isDragging = true;
    }
    public void EndDrag()
    {
        isDragging = false;
    }




}