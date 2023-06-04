using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 手写输入 : MonoBehaviour
{
    TouchScreenKeyboard keyboard;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openKeyboard()
    {
        keyboard = TouchScreenKeyboard.Open("Enter your name...", TouchScreenKeyboardType.Default, true, true);
        Debug.Log("open keyboard: "+keyboard.active);
        Debug.Log("keyboard visable: " + TouchScreenKeyboard.visible);
        Debug.Log("keboard area: " + TouchScreenKeyboard.area);
    }

    public void getName()
    {
        string name = keyboard.text;

    }

}
