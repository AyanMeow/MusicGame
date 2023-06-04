using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public class 分数设置 : MonoBehaviour
{
 
    
    public static uint badScore = 500;
    public static uint goodScore = 2000;
    public static uint perfectScore = 3000;
    public static uint perfectSlap = 100;
    
    /*
    public static uint badScore = 1;
    public static uint goodScore = 1;
    public static uint perfectScore = 1;
    public static uint perfectSlap = 1;
    */
    public static List<string> _LocalScoreList;
    public static List<string> _LocalScoreName;

    public static string scoreCache;

    public static string scoreFilePath = "/userdata/score.txt";
    public static string nameFilePath = "/userdata/name.txt";

    Queue<uint> addations;
    uint score=0;

    private void Awake()
    {
        _LocalScoreList = new List<string>();
        _LocalScoreName = new List<string>();
        addations = new Queue<uint>();
        score = 0;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.addations.Count > 0)
        {
            score = score + addations.Dequeue();
            string scoreTemp = score.ToString();
            scoreTemp = scoreTemp.PadLeft(8, '0');
            //Debug.Log("score length:" + scoreTemp.Length);
           // Debug.Log("score string:"+scoreTemp);
            setScore(scoreTemp);
        }
        scoreCache = gameObject.GetComponent<TextMesh>().text;
    }

    public string getScore()
    {
        return this.GetComponent<TextMesh>().text;
    }

    public void setScore(string newScore)
    {
        this.GetComponent<TextMesh>().text = newScore;
    }

    public void addScore(uint additaion)
    {
        //Debug.Log("分数入队");
        this.addations.Enqueue(additaion);
    }

    public void test(uint testData)
    {
        Debug.Log("test:" + testData);
    }

    public static void localDataLoad()
    {
        string[] scores = null;
        string[] names = null;
        try
        {
            if (File.Exists(Application.persistentDataPath + scoreFilePath))
            {
                scores = File.ReadAllLines(Application.persistentDataPath + scoreFilePath);
            }

            if (File.Exists(Application.persistentDataPath + nameFilePath))
            {
                names = File.ReadAllLines(Application.persistentDataPath + nameFilePath);
            }
        }catch(Exception e)
        {
            Debug.Log("read score error:" + e.Message);
        }
       

        if (scores!=null&& names!=null)
        {
            for(int i = 0; i < scores.Length; i++)
            {
                _LocalScoreList.Add(scores[i]);
                _LocalScoreName.Add(names[i]);
            }
        }

    }

    public static void updateScore(string name)
    {
        string score = scoreCache;
        for(int i = 0; i < _LocalScoreList.Count; i++)
        {
            string temp = _LocalScoreList[i];
            if (score.CompareTo(temp) >= 0)
            {
                _LocalScoreList.Insert(i, score);
                _LocalScoreName.Insert(i, name);
                break;
            }
        }
    }

    public static void localDataSave()
    {
        string[] score = new string[10];
        string[] name = new string[10];

        for(int i = 0; i < _LocalScoreList.Count && i < 10; i++)
        {
            score[i] = _LocalScoreList[i].ToString();
            name[i] = _LocalScoreName[i].ToString();
        }
        try
        {
            File.WriteAllLines(Application.persistentDataPath + scoreFilePath, score);
            File.WriteAllLines(Application.persistentDataPath + nameFilePath, name);
        }catch(Exception e)
        {
            Debug.Log("write score error:" + e.Message);
        }

    }
}


