using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 设置判定距离 : MonoBehaviour
{
    public GameObject shooter;
    public float delay = 0;
    public GameObject tA;
    public GameObject sA;

    private static GameObject touchAnimation;
    private static GameObject slapAnimation;
    private static GameObject me;
    private float baseSpeed = 200f;
    private float disPerfect = 15;
    private float disNull = 120;
    private float disBad = 100;
    private float disGood = 60;

    public float DisNull { get => disNull; set => disNull = value; }
    public float DisBad { get => disBad; set => disBad = value; }
    public float DisGood { get => disGood; set => disGood = value; }
    public float DisPerfect { get => disPerfect; set => disPerfect = value; }



    // Start is called before the first frame update
    void Start()
    {
        float speed = shooter.GetComponent<发射总控>().getMoveSpeed();
        this.DisPerfect = 5 + delay;
        this.disGood = 25 * speed / baseSpeed+delay;
        this.disBad = 50 * speed / baseSpeed+delay;
        this.disNull = 100 * speed / baseSpeed+delay;
        me = gameObject;
        touchAnimation = tA;
        slapAnimation = sA;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void genTouchAnimation(GameObject judgepoint)
    {
        GameObject.Instantiate(touchAnimation, judgepoint.transform.position, judgepoint.transform.rotation, me.transform);
    }

    public static void genSlapAnimation(GameObject judgepoint)
    {
        Transform result = null;
        result = judgepoint.transform.Find("长按动画(Clone)");
        //Debug.Log("长按子物体：" + result);
        if(!result)
        {
            //GameObject.Instantiate(slapAnimation, judgepoint.transform.position, judgepoint.transform.rotation, me.transform);
        }
    }
}
