using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class new判定文字 : MonoBehaviour
{
    // Start is called before the first frame update


    static TextMesh judgeText;
    static string[] text = { "完美", "不错", "遗憾", "失误" };
    static Color[] colorPre = { Color.green, Color.blue, Color.gray, Color.black };
    static Animator anim;
    void Start()
    {
        judgeText = gameObject.GetComponent<TextMesh>();
        anim = gameObject.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo animCurrent = anim.GetCurrentAnimatorStateInfo(0);
        //Debug.Log("anim: " + animCurrent.IsName("kong"));
        if(animCurrent.IsName("kong"))
        {
            anim.SetInteger("states", 0);
        }
    }
    public static void 改变判定文字(int num)
    {
        num = num % 4;
        judgeText.text = text[num];
        //judgeText.color = colorPre[num];
        anim.SetInteger("states",1);
    }
}
