using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 触摸事件 : MonoBehaviour
{

    GameObject []judgePoints;

    Touch touch;
    bool hasTouched;

    private void Awake()
    {
        judgePoints = GameObject.FindGameObjectsWithTag("judgePoint");
        Debug.Log("judge:" + judgePoints.Length);
    }
    // Start is called before the first frame update
    void Start()
    {
        hasTouched = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (!hasTouched)
            {
                hasTouched = true;
                for(int i = 0; i < Input.touchCount; i++)
                {
                    touch = Input.GetTouch(i);
                    for (int j = 0; j < judgePoints.Length; j++)
                    {
                        Bounds bounds = judgePoints[j].GetComponent<BoxCollider2D>().bounds;
                        //Debug.Log("touch:" + touch.position);
                        var bmax = Camera.main.WorldToScreenPoint(bounds.max);
                        var bmin = Camera.main.WorldToScreenPoint(bounds.min);
                        //Debug.Log(judgePoints[j].name+" bounds:" + a + "," + b);
                        if (touch.position.x >= bmin.x && touch.position.x <= bmax.x && touch.position.y >= bmin.y && touch.position.y <= bmax.y)
                        {
                            Debug.Log("hit " + judgePoints[j]);
                        }
                    }
                }
            }
        }
        else
        {
            hasTouched = false;
        }
    }

    public static bool slapTouch(GameObject judgePoint)
    {
        if (Input.touchCount <= 0)
        {
            return false;
        }
        var bounds = judgePoint.GetComponent<BoxCollider2D>().bounds;
        var bmax = Camera.main.WorldToScreenPoint(bounds.max);
        var bmin = Camera.main.WorldToScreenPoint(bounds.min);
        for(int i = 0; i < Input.touchCount; i++)
        {
            var touch = Input.GetTouch(i);
            if (touch.position.x >= bmin.x && touch.position.x <= bmax.x && touch.position.y >= bmin.y && touch.position.y <= bmax.y)
            {
                if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Ended)
                    return true;
            }
        }
        return false;
    }
}
