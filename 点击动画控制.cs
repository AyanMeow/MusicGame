using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 点击动画控制 : MonoBehaviour
{
    // Start is called before the first frame update
    Animator thisAnimation;
    void Start()
    {
        
    }
    private void Awake()
    {
        thisAnimation = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo info = thisAnimation.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= 1)
        {
            GameObject.Destroy(gameObject);
        }
    }

}
