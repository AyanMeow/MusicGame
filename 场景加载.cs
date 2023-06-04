using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class 场景加载 : MonoBehaviour
{
    public GameObject blackimage;
    public GameObject textOtherNeed;

    RawImage black;
    bool sceneEnding = false;
    TextMesh textOther=null;
    // 目标异步场景
    private AsyncOperation asyncOperation;
    float timeCount = 2;

    void Start()
    {
        black = blackimage.GetComponent<RawImage>();
        if (textOtherNeed != null)
        {
            textOther = textOtherNeed.GetComponent<TextMesh>();
        }
        // 协程启动异步加载
        StartCoroutine(this.AsyncLoading());
   
    }

    private void Update()
    {
        if (!black.enabled&&timeCount > 0)
        {
            timeCount -= 1f * Time.deltaTime;
        }
        if (timeCount <= 0)
        {
            black.enabled = true;
            sceneEnding = true;
        }
        if (sceneEnding)
        {
            //Debug.Log("alpha change:" + black.color.a);
            black.color = Color.Lerp(black.color, Color.black, 1.7f * Time.deltaTime);
            if (textOther != null)
            {
                textOther.color = Color.Lerp(textOther.color, Color.clear, 1.7f * Time.deltaTime);
            }
            if (black.color.a > 0.98)
            {
                this.asyncOperation.allowSceneActivation = true;
            }
        }
    }

    IEnumerator AsyncLoading()
    {
        UnityEngine.Debug.Log("异步加载：" + (string)场景切换控制.sceneNames[场景切换控制.sceneIndex]);
        this.asyncOperation = SceneManager.LoadSceneAsync((string)场景切换控制.sceneNames[场景切换控制.sceneIndex]);
        //终止自动切换场景
        this.asyncOperation.allowSceneActivation = false;
        yield return asyncOperation;
    }

}
