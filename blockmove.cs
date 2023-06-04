using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockmove : MonoBehaviour
{
    public bool isTouched = false;
    public bool isTogether = false;
    public bool isSlide = false;
    public bool islap = false;//是不是长按
    public GameObject brother;//兄弟块儿


    Vector3 scaleTemp;
    float moveSpeed=1;
    float desInit = 0;
    float des = 0;
    int start = 0;
    GameObject moveTarget=null;
    bool isDestroy = false;
    Color colorTemp;
    bool genSlapAnima = false;
    // Start is called before the first frame update
    private void Awake()
    {
        scaleTemp = transform.localScale;
        //ansform.localScale = scaleTemp*0;
         colorTemp = new Color(
                    this.GetComponent<Renderer>().material.color.r,
                    this.GetComponent<Renderer>().material.color.g,
                    this.GetComponent<Renderer>().material.color.b,
                    this.GetComponent<Renderer>().material.color.a
                    );
        this.GetComponent<Renderer>().material.color = new Color(
                    this.GetComponent<Renderer>().material.color.r,
                    this.GetComponent<Renderer>().material.color.g,
                    this.GetComponent<Renderer>().material.color.b,
                    0
                    );

    }
    private void Start()
    {
        //Debug.Log("moveTarget position:" + moveTarget.transform.position + "  block position:" + this.transform.position);
        transform.right = moveTarget.transform.right;
        this.GetComponent<Renderer>().material.color = colorTemp;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouched&&!isTogether)
        {
            GameObject.Destroy(gameObject);
        }
        if (游戏总控.gameStop)
        {
            GameObject.Destroy(gameObject);
        }
        Transform result = null;
        result = moveTarget.transform.Find("长按动画(Clone)");
        if (result == null)
        {
            genSlapAnima = true;
        }
        if (start==1&&!游戏总控.gameStop)
        {
            des = Vector3.Distance(this.transform.position, moveTarget.transform.position);
            //Debug.Log("des:" + des);
            if (des > 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, moveTarget.transform.position, moveSpeed*Time.deltaTime);
                //Debug.Log("current.z:" + transform.position.z + "moveTarget.z:" + moveTarget.transform.position.z);
            }
            else
            {
                /*
                Vector3 temp = this.transform.localScale;
                this.transform.localScale = temp * 1.005f;
                this.GetComponent<Renderer>().material.color = new Color(
                    this.GetComponent<Renderer>().material.color.r,
                    this.GetComponent<Renderer>().material.color.g,
                    this.GetComponent<Renderer>().material.color.b,
                    this.GetComponent<Renderer>().material.color.a - 100f / 255f * Time.deltaTime
                    );
                */
                if (isTogether)
                {
                    if (isTouched)
                    {
                        GameObject temp = moveTarget.GetComponent<触摸判定>().blockQueue.Peek();
                        if(temp == gameObject)
                        {
                            if (!brother.GetComponent<blockmove>().isTouched)
                            {
                                moveTarget.GetComponent<触摸判定>().blockQueue.Dequeue();
                                判定文字.setText(moveTarget.GetComponent<触摸判定>().textPoint, "miss");
                                new判定文字.改变判定文字(3);
                                combo设置.combo数目置0();
                                Destroy(gameObject);
                            }
                        }    
                    }else
                    {
                        GameObject temp = moveTarget.GetComponent<触摸判定>().blockQueue.Peek();
                        if (temp == gameObject)
                        {                    
                            moveTarget.GetComponent<触摸判定>().blockQueue.Dequeue();
                            判定文字.setText(moveTarget.GetComponent<触摸判定>().textPoint, "miss");
                            new判定文字.改变判定文字(3);
                            combo设置.combo数目置0();
                            Destroy(gameObject);
                            
                        }
                    }

                }
                else if(islap)
                    {
                        if (触摸判定.slapTouch(moveTarget))
                        {
                            this.isTouched = true;
                            if (genSlapAnima)
                            {
                                设置判定距离.genSlapAnimation(moveTarget);
                            }

                            //判定文字.setText(moveTarget.GetComponent<触摸判定>().textPoint, "perfect");
                            new判定文字.改变判定文字(0);
                            //combo设置.增加combo数目();
                            GameObject.Find("score").GetComponent<分数设置>().addScore(分数设置.perfectSlap);
                            GameObject.Destroy(gameObject);
                            gameObject.SetActive(false);
                        }
                        else
                        {
                            //判定文字.setText(moveTarget.GetComponent<触摸判定>().textPoint, "miss");
                            new判定文字.改变判定文字(3);
                            combo设置.combo数目置0();
                            Destroy(gameObject);
                            gameObject.SetActive(false);
                        }
                    }
                    else if (isSlide)
                    {
                        if (触摸判定.slapTouch(moveTarget))
                        {
                            音效控制.playsoud();
                            设置判定距离.genTouchAnimation(moveTarget);
                            //判定文字.setText(moveTarget.GetComponent<触摸判定>().textPoint, "perfect");
                            new判定文字.改变判定文字(0);
                            combo设置.增加combo数目();
                            GameObject.Find("score").GetComponent<分数设置>().addScore(分数设置.perfectSlap);

                            GameObject.Destroy(gameObject);
                            gameObject.SetActive(false);
                            this.isTouched = true;

                        }
                        else
                        {
                            //判定文字.setText(moveTarget.GetComponent<触摸判定>().textPoint, "miss");
                            new判定文字.改变判定文字(3);
                            combo设置.combo数目置0();
                            Destroy(gameObject);
                            gameObject.SetActive(false);
                        }
                    }
                    else
                {
                    if(!isTouched && !isDestroy && !islap)
                    {
                        GameObject temp = moveTarget.GetComponent<触摸判定>().blockQueue.Peek();
                        if (temp == gameObject)
                        {
                            moveTarget.GetComponent<触摸判定>().blockQueue.Dequeue();
                            判定文字.setText(moveTarget.GetComponent<触摸判定>().textPoint, "miss");
                            new判定文字.改变判定文字(3);
                            combo设置.combo数目置0();
                            Destroy(gameObject);
                        }
                    }
                }

                   
            }
        }
    }

    public void setTarget(GameObject targetIn,float speedIn)
    {
       
        moveTarget = targetIn;
        moveSpeed = speedIn;
        start = 1;
        desInit = Vector3.Distance(this.transform.position, moveTarget.transform.position);
    }
}
