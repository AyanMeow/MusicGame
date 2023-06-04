using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class combo设置 : MonoBehaviour
{
    // Start is called before the first frame update

    public static TextMesh combo;
    static int comboNum;
    //static Animation comboAnim;
    void Start()
    {
        combo = gameObject.GetComponent<TextMesh>();
        comboNum = 0;
        //comboAnim = gameObject.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void 增加combo数目()
    {
        comboNum += 1;
        string textNow = comboNum.ToString();
        textNow=textNow.Insert(0, "x");
        //comboAnim.Play();
        combo.text = textNow;
        
    }

    public static void combo数目置0()
    {
        comboNum = 0 ;
        string textNow = comboNum.ToString();
        textNow=textNow.Insert(0, "x");
        //comboAnim.Play();
        combo.text = textNow;
    }
}
