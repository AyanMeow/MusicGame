using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.SceneManagement;

public class 按钮功能 : MonoBehaviour
{
    public GameObject blackimage;
    public GameObject touchAime;
    public GameObject[] textOtherNeed;

    public GameObject[] spriteContor;
    public bool needLoading = true;

    RawImage black;
    bool sceneEnding = false;
    string Scene;

    // Start is called before the first frame update
    private void Start()
    {
        black = blackimage.GetComponent<RawImage>();
    }

    private void Update()
    {
        if (sceneEnding)
        {
            //Debug.Log("alpha change:" + black.color.a);
            black.color = Color.Lerp(black.color, Color.black, 1.7f * Time.deltaTime);
            if (textOtherNeed.Length > 0)
            {
                foreach (var text in textOtherNeed)
                {
                    text.GetComponent<TextMesh>().color = Color.Lerp(text.GetComponent<TextMesh>().color, Color.black, 3f * Time.deltaTime);
                }
            }
            if (spriteContor.Length > 0)
            {
                for(int i = 0; i < spriteContor.Length; i++)
                {
                    var temp = spriteContor[i].GetComponent<SpriteRenderer>().color;
                    spriteContor[i].GetComponent<SpriteRenderer>().color= Color.Lerp(temp, Color.black, 3.7f * Time.deltaTime);
                }
            }
            if (black.color.a > 0.98)
            {
                Debug.Log("准备加载场景：" + Scene);
                if (needLoading)
                {
                    场景切换控制.GoToSceneByName(Scene);
                }
                else
                {
                    SceneManager.LoadScene(Scene);
                }
            }
        }
    }

    public void ChangeScene(string Scene)
    {
        Time.timeScale = 1;
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            var pw = Camera.main.ScreenToWorldPoint(touch.position);
            pw =new Vector3(pw.x, pw.y, 0);
            var anim=GameObject.Instantiate(touchAime, pw,gameObject.transform.rotation);
            anim.transform.localScale = new Vector3(0.3f, 0.3f, 1);
            var colortemp = anim.GetComponent<SpriteRenderer>().color;
            anim.GetComponent<SpriteRenderer>().color = new Color(colortemp.r, colortemp.g, colortemp.b, 0.5f);

        }
        blackimage.GetComponent<FadeInOut>().sceneStarting = false;
        this.Scene = Scene;
        black.enabled = true;
        sceneEnding = true;
        
    }

    public void selectStage()
    {
        //Debug.Log("static scene name: " + onDrag.stageSceneLoad);
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            var pw = Camera.main.ScreenToWorldPoint(touch.position);
            pw = new Vector3(pw.x, pw.y, 0);
            var anim = GameObject.Instantiate(touchAime, pw, gameObject.transform.rotation);
            anim.transform.localScale = new Vector3(0.3f, 0.3f, 1);
            var colortemp = anim.GetComponent<SpriteRenderer>().color;
            anim.GetComponent<SpriteRenderer>().color = new Color(colortemp.r, colortemp.g, colortemp.b, 0.5f);

        }
        blackimage.GetComponent<FadeInOut>().sceneStarting = false;
        this.Scene = onDrag.stageSceneLoad;
        black.enabled = true;
        sceneEnding = true;
    }

    public void changeSceneInStage(string Scene)
    {
        游戏总控.setGameStop(true);
        Time.timeScale = 1;
        var list = gameObject.transform.parent.gameObject.GetComponentsInChildren<Image>();
        foreach(var e in list){
            e.color = Color.clear;
        }
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            var pw = Camera.main.ScreenToWorldPoint(touch.position);
            pw = new Vector3(pw.x, pw.y, 0);
            var anim = GameObject.Instantiate(touchAime, pw, gameObject.transform.rotation);
            anim.transform.localScale = new Vector3(0.3f, 0.3f, 1);
            var colortemp = anim.GetComponent<SpriteRenderer>().color;
            anim.GetComponent<SpriteRenderer>().color = new Color(colortemp.r, colortemp.g, colortemp.b, 0.5f);

        }
        blackimage.GetComponent<FadeInOut>().sceneStarting = false;
        this.Scene = Scene;
        black.enabled = true;
        sceneEnding = true;
    }
}
