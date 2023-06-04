using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class 发射器 : MonoBehaviour
{
    public GameObject judgepoint;
    // Start is called before the first frame update


    public GameObject blockGenerate(GameObject blockType,float speed)
    {
        GameObject blockTemp= Instantiate(blockType, transform.position, transform.rotation,this.transform) as GameObject;
        blockTemp.transform.localScale = new Vector3(2f, 2f, 1);
        blockTemp.GetComponent<blockmove>().setTarget(this.judgepoint, speed);
        //blockTemp.transform.parent = this.transform;
        //print("Generate blcok Type" + blockType);
        //传递给判定点的队列
        if(!blockTemp.GetComponent<blockmove>().islap&&!blockTemp.GetComponent<blockmove>().isSlide)
            this.judgepoint.GetComponent<触摸判定>().blockQueue.Enqueue(blockTemp);
        return blockTemp;
    }

}
