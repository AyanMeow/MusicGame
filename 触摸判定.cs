using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class 触摸判定 : MonoBehaviour
{
    public Queue<GameObject> blockQueue;

    public GameObject[] judgeTexts;

    public GameObject textPoint;

    public GameObject GameControl;

    private float disNull, disBad, disGood, disPerfect;

    static bool isDemonstration ;

    bool judgeLock;

    Touch touch;

    bool hasTouched;

    static Vector2 centerV2;

    static int touchC;

    // Start is called before the first frame update
    private void Awake()
    {
        blockQueue = new Queue<GameObject>();
        this.disPerfect = gameObject.GetComponentInParent<设置判定距离>().DisPerfect;
        this.disGood = gameObject.GetComponentInParent<设置判定距离>().DisGood;
        this.disBad = gameObject.GetComponentInParent<设置判定距离>().DisBad;
        this.disNull = gameObject.GetComponentInParent<设置判定距离>().DisNull;
    }

    private void Start()
    {
        isDemonstration = GameControl.GetComponent<游戏总控>().setDemonstration;
        hasTouched = true;
        judgeLock = false;
        //Debug.Log(Application.platform);
        //Debug.Log(" touch judeg isDemonstration:" + isDemonstration);
        Vector3 temp = Camera.main.WorldToScreenPoint(GameControl.transform.position);
        centerV2 = new Vector2(temp.x, temp.y);
        //Debug.Log("center: "+centerV2);
        touchC = 0;
    }
    // Update is called once per frame
    void Update()
    {
        var touchs = Input.touches;
        /*
        if (游戏总控.gameStop)
        {
            for(int i = 0; i < blockQueue.Count; i++)
            {
                var temp = blockQueue.Dequeue();
                GameObject.Destroy(temp);
            }
        }*/
        if (blockQueue.Count > 0)
        {
            if (blockQueue.Peek() == null)
            {
                blockQueue.Dequeue();
            }
            
        }

        if (!isDemonstration &&!游戏总控.gameStop && (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.WindowsEditor||Application.platform==RuntimePlatform.IPhonePlayer))
        {
            //Debug.Log("now isDemonstration == false");
            if (Input.touchCount > 0)
            {
                //var touchs = Input.touches;
                if (!hasTouched)
                {
                    hasTouched = true;
                    //var touchs = Input.touches;
                    for (int i = 0; i < touchs.Length; i++)
                    {
                        //touch = Input.GetTouch(i);
                        touch = touchs[i];
                        Vector2 touchTest = touch.position;
                        Vector3 t = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                        Vector2 judgeTest = new Vector2(t.x, t.y);
                        Vector2 centerTotouch = centerV2 - touchTest;
                        Vector2 centerTojudge = centerV2 - judgeTest;
                        //Debug.Log("touch:" + touch.position);
                        var angle = Vector2.Angle(centerTojudge, centerTotouch)%180;
                        var disCJ = Vector2.Distance(centerV2, judgeTest);
                        var disCT = Vector2.Distance(centerV2, touchTest);
                        if (angle < 14 && angle > -14 && disCT >= (disCJ - 100f) && disCT <= (disCJ + 250f))
                        {
                            touchC++;
                        }
                    }
                    
                    //if (angle<15&&angle>-15&&disCT>=(disCJ-50f))
                    //    Debug.Log(gameObject.name+" Angle: " + angle);
                    //var bounds = gameObject.GetComponent<BoxCollider2D>().bounds;
                    //var bmax = Camera.main.WorldToScreenPoint(bounds.max);
                    //var bmin = Camera.main.WorldToScreenPoint(bounds.min);
                    //Debug.Log(judgePoints[j].name+" bounds:" + a + "," + b);
                    //if (touch.position.x >= bmin.x && touch.position.x <= bmax.x && touch.position.y >= bmin.y && touch.position.y <= bmax.y)
                    for (;touchC>0;touchC--) 
                    {
                        //if (angle<12&&angle>-12&&disCT>=(disCJ-80f)&&disCT<=(disCJ+200f))
                        {
                            音效控制.playsoud();
                            //设置判定距离.genTouchAnimation(gameObject);
                            //Debug.Log("hit " + gameObject.name);
                       
                            if (!judgeLock & blockQueue.Count > 0)
                            {
                                judgeLock = true;
                                GameObject tempBlock = blockQueue.Peek();
                                float distance = Vector3.Distance(tempBlock.transform.position, this.transform.position);

                                                 
                                if (distance > disNull)
                                {
                                    //过远点击，什么都不发生
                                    judgeLock = false;
                                    //音效控制.playsoud();
                                }
                                else if (distance >= disBad)
                                {
                                    //音效控制.playsoud();                              
                                    if(tempBlock.GetComponent<blockmove>().isTogether)
                                    {
                                        tempBlock.GetComponent<blockmove>().isTouched = true;
                                        if (tempBlock.GetComponent<blockmove>().brother.GetComponent<blockmove>().isTouched)
                                        {
                                            GameObject.Find("score").GetComponent<分数设置>().addScore(分数设置.badScore);
                                            GameObject.Find("score").GetComponent<分数设置>().addScore(分数设置.badScore);
                                            //判定文字.setText(textPoint, "bad");
                                            new判定文字.改变判定文字(2);
                                            blockQueue.Dequeue();
                                            Destroy(tempBlock);
                                            Destroy(tempBlock.GetComponent<blockmove>().brother);
                                        }
                                        combo设置.增加combo数目();
                                        judgeLock = false;
                                    }
                                    else
                                    {
                                        设置判定距离.genTouchAnimation(gameObject);
                                        GameObject.Find("score").GetComponent<分数设置>().addScore(分数设置.badScore);
                                        tempBlock.GetComponent<blockmove>().isTouched = true;
                                        blockQueue.Dequeue();
                                        Destroy(tempBlock);
                                        //判定文字.setText(textPoint, "bad");
                                        new判定文字.改变判定文字(2);
                                        combo设置.增加combo数目();
                                        judgeLock = false;
                                    }
                                    
                                }
                                else if (distance >= disGood)
                                {
                                    //音效控制.playsoud();
                                    if (tempBlock.GetComponent<blockmove>().isTogether)
                                    {
                                        tempBlock.GetComponent<blockmove>().isTouched = true;
                                        if (tempBlock.GetComponent<blockmove>().brother.GetComponent<blockmove>().isTouched)
                                        {
                                            GameObject.Find("score").GetComponent<分数设置>().addScore(分数设置.goodScore);
                                            GameObject.Find("score").GetComponent<分数设置>().addScore(分数设置.goodScore);
                                            //判定文字.setText(textPoint, "good");
                                            new判定文字.改变判定文字(1);
                                            blockQueue.Dequeue();
                                            Destroy(tempBlock);
                                            Destroy(tempBlock.GetComponent<blockmove>().brother);
                                        }
                                        combo设置.增加combo数目();
                                        judgeLock = false;
                                    }
                                    else
                                    {
                                        设置判定距离.genTouchAnimation(gameObject);
                                        GameObject.Find("score").GetComponent<分数设置>().addScore(分数设置.goodScore);                             
                                        tempBlock.GetComponent<blockmove>().isTouched = true;
                                        blockQueue.Dequeue();
                                        Destroy(tempBlock);
                                        //判定文字.setText(textPoint, "good");
                                        new判定文字.改变判定文字(1);
                                        combo设置.增加combo数目();
                                        judgeLock = false;
                                    }
                                    
                                }
                                else if (distance >= disPerfect)
                                {
                                    //音效控制.playsoud();
                                    if (tempBlock.GetComponent<blockmove>().isTogether)
                                    {
                                        tempBlock.GetComponent<blockmove>().isTouched = true;
                                        if (tempBlock.GetComponent<blockmove>().brother.GetComponent<blockmove>().isTouched)
                                        {
                                            GameObject.Find("score").GetComponent<分数设置>().addScore(分数设置.perfectScore);
                                            GameObject.Find("score").GetComponent<分数设置>().addScore(分数设置.perfectScore);
                                            //判定文字.setText(textPoint, "perfect");
                                            new判定文字.改变判定文字(0);
                                            blockQueue.Dequeue();
                                            Destroy(tempBlock);
                                            Destroy(tempBlock.GetComponent<blockmove>().brother);
                                        }
                                        combo设置.增加combo数目();
                                        judgeLock = false;
                                    }
                                    else
                                    {
                                        设置判定距离.genTouchAnimation(gameObject);
                                        GameObject.Find("score").GetComponent<分数设置>().addScore(分数设置.perfectScore);
                                        tempBlock.GetComponent<blockmove>().isTouched = true;
                                        blockQueue.Dequeue();
                                        Destroy(tempBlock);
                                        //判定文字.setText(textPoint, "perfect");
                                        new判定文字.改变判定文字(0);
                                        combo设置.增加combo数目();
                                        judgeLock = false;
                                    }
                                   
                                }
                                else if (distance > 0.5f)
                                {
                                    //音效控制.playsoud();
                                    if (tempBlock.GetComponent<blockmove>().isTogether)
                                    {
                                        tempBlock.GetComponent<blockmove>().isTouched = true;
                                        if (tempBlock.GetComponent<blockmove>().brother.GetComponent<blockmove>().isTouched)
                                        {
                                            GameObject.Find("score").GetComponent<分数设置>().addScore(分数设置.badScore);
                                            GameObject.Find("score").GetComponent<分数设置>().addScore(分数设置.badScore);
                                            //判定文字.setText(textPoint, "bad");
                                            new判定文字.改变判定文字(2);
                                            blockQueue.Dequeue();
                                            Destroy(tempBlock);
                                            Destroy(tempBlock.GetComponent<blockmove>().brother);
                                        }
                                        combo设置.增加combo数目();
                                        judgeLock = false;
                                    }
                                    else
                                    {
                                        设置判定距离.genTouchAnimation(gameObject);
                                        GameObject.Find("score").GetComponent<分数设置>().addScore(分数设置.perfectScore);
                                        tempBlock.GetComponent<blockmove>().isTouched = true;
                                        blockQueue.Dequeue();
                                        Destroy(tempBlock);
                                        //判定文字.setText(textPoint, "bad");
                                        new判定文字.改变判定文字(0);
                                        combo设置.增加combo数目();
                                        judgeLock = false;
                                    }
                                }
                                if (tempBlock != null && tempBlock.GetComponent<blockmove>().isTogether)
                                {
                                    if (tempBlock.GetComponent<blockmove>().isTouched)
                                    {
                                        tempBlock.GetComponent<blockmove>().isTouched = false;
                                    }
                            
                                    judgeLock = false;
                                }

                                tempBlock = null;
                            }
                        }
                        
                    }
                }
                //hasTouched = false;
            }
            else
            {
                hasTouched = false;
            }
        }
        
        if (isDemonstration)
        {
            if (blockQueue.Count > 0)
            {
                //检查队列中块的距离
                GameObject tempBlock = blockQueue.Peek();
                float distance = Vector3.Distance(tempBlock.transform.position, this.transform.position);
                if(distance >=disPerfect && distance < (disGood+disPerfect)/2)
                {
                    音效控制.playsoud();
                    设置判定距离.genTouchAnimation(gameObject);
                    GameObject.Find("score").GetComponent<分数设置>().addScore(分数设置.perfectScore);
                    tempBlock.GetComponent<blockmove>().isTouched = true;
                    blockQueue.Dequeue();
                    Destroy(tempBlock);
                    判定文字.setText(textPoint, "perfect");
                    new判定文字.改变判定文字(0);
                    combo设置.增加combo数目();
                    judgeLock = false;
                    
                }
            }
        }
    }


    public static bool slapTouch(GameObject judgePoint)
    {
        if (isDemonstration)
        {
            return true;
        }
        if (Input.touchCount <= 0)
        {
            return false;
        }
        if (touchC > 0)
        {
            return true;
        }
        var touchs = Input.touches;
        for (int i = 0; i < touchs.Length; i++)
        {
            var touch = touchs[i];
            Vector2 touchTest = touch.position;
            Vector3 t = Camera.main.WorldToScreenPoint(judgePoint.transform.position);
            Vector2 judgeTest = new Vector2(t.x, t.y);
            Vector2 centerTotouch = centerV2 - touchTest;
            Vector2 centerTojudge = centerV2 - judgeTest;
            //Debug.Log("touch:" + touch.position);
            var angle = Vector2.Angle(centerTojudge, centerTotouch);
            var disCJ = Vector2.Distance(centerV2, judgeTest);
            var disCT = Vector2.Distance(centerV2, touchTest);
            //if (angle<15&&angle>-15&&disCT>=(disCJ-50f))
            //    Debug.Log(gameObject.name+" Angle: " + angle);
            //var bounds = gameObject.GetComponent<BoxCollider2D>().bounds;
            //var bmax = Camera.main.WorldToScreenPoint(bounds.max);
            //var bmin = Camera.main.WorldToScreenPoint(bounds.min);
            //Debug.Log(judgePoints[j].name+" bounds:" + a + "," + b);
            //if (touch.position.x >= bmin.x && touch.position.x <= bmax.x && touch.position.y >= bmin.y && touch.position.y <= bmax.y)
            if (angle < 12 && angle > -12 && disCT >= (disCJ - 80f) && disCT <= (disCJ + 200f))
            {
                //if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Ended || touch.phase==TouchPhase.Moved )
                    return true;
            }
        }
        return false;
    }

}
