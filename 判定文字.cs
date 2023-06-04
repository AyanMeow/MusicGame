using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 判定文字 : MonoBehaviour
{
    public GameObject _perfectText, _goodText, _badText, _missText;

    GameObject[] judgeTextPoints;
    static Hashtable judgeSet;
    static Hashtable textSet;
    static bool TextOn = 游戏总控.getTextOnOff();
    private void Awake()
    {
        judgeTextPoints = GameObject.FindGameObjectsWithTag("judgeText");
        judgeSet = new Hashtable();
        foreach(GameObject temp in judgeTextPoints)
        {
            judgeSet.Add(temp, null);
        }
        textSet = new Hashtable();
        textSet.Add("perfect", _perfectText);
        textSet.Add("good", _goodText);
        textSet.Add("bad", _badText);
        textSet.Add("miss", _missText);
    }

    // Start is called before the first frame update

    public static void setText(GameObject _targetPosition,string textType)
    {
        if (!TextOn)
        {
            return;
        }
        if (_targetPosition!=null) 
        {
            //获取已有的判定文字并销毁
            GameObject tempJudge = judgeSet[_targetPosition] as GameObject;
            if (tempJudge != null)
            {
                Destroy(tempJudge);
            }
            //获取判定点位置并向圆心偏移
            Vector3 position = _targetPosition.transform.position;
            //position.y += 1.05f;
            //position.x += 1.05f;
            //position.z -= 0.05f;
            //实例化判定文字并调整方向
            GameObject tempText = Instantiate(textSet[textType] as GameObject, position, _targetPosition.transform.rotation,_targetPosition.transform) as GameObject;
            tempText.transform.right = _targetPosition.transform.right;
            tempText.transform.localScale = new Vector3(4f, 4f, 1f);
            //判定文字记录
            judgeSet[_targetPosition] = tempText;
            delayDestory(judgeSet[_targetPosition] as GameObject);
        }
        else
        {
            Debug.LogError("_targetPosition is null");
        }
    }

    static void delayDestory(GameObject gameObject)
    {
        Destroy(gameObject, 0.5f);
    }
}
