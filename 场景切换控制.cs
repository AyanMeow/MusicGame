using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public static class 场景切换控制
{
    // 场景列表，这里填写的都是你已经存在的scene 的场景名
    public static List<string> sceneNames = new List<string>() {
        "初始界面",
        "关卡1",
        "关卡2",
        "主界面ui",
        "结束界面",
        "选择歌曲"
    };
    // 自定义loading名，在这里使用它是因为别的需求，如果你的需求比较简单可以直接写死
    public static string LoadingSceneName = "loading";
    // 定义一个回调事件  当完成任何场景切换时触发
    public static Action AfterGoToScene;

    // 即将切换场景的序号
    public static int sceneIndex = -1;
    // 根据场景顺序，切换到下一个场景的方法
    public static void GoToNextScene()
    {
        if (场景切换控制.sceneIndex < 场景切换控制.sceneNames.Count - 1)
        {
            场景切换控制.sceneIndex++;
            GoToSceneByIndex(场景切换控制.sceneIndex);
        }
    }
    // 根据场景顺序，切换到上一个场景
    public static void GoToPrevScene()
    {
        if (场景切换控制.sceneIndex > 0)
        {
            场景切换控制.sceneIndex--;
            GoToSceneByIndex(场景切换控制.sceneIndex);
        }
    }

    // 根据场景名称（scene name） 切换到目标场景 （scene index会跟着变动）
    /// <summary>
    /// goto the scene if finded name in scenelist,atherwase reopen current scene 
    /// </summary>
    /// <param name="sceneName"></param>
    public static void GoToSceneByName(string sceneName)
    {
        if (场景切换控制.sceneNames.Contains(sceneName))
        {
            GoToSceneByIndex(场景切换控制.sceneNames.IndexOf(sceneName));
        }
    }
    //根据 场景序号切换到目标场景
    public static void GoToSceneByIndex(int sceneIndex)
    {
        if (sceneIndex >= 0 && sceneIndex < 场景切换控制.sceneNames.Count)
        {
            场景切换控制.sceneIndex = sceneIndex;
            SceneManager.LoadScene(LoadingSceneName);
            AfterGoToScene?.Invoke();
        }
    }
    // 根据目标场景名称 注销目标场景，这个只是为了统一管理，因为unity早就内置了方法
    public static void UnLoadSceneByName(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
    // 根据目标场景序号 注销目标场景
    public static void UnLoadSceneById(int sceneID)
    {
        SceneManager.UnloadSceneAsync((string)场景切换控制.sceneNames[sceneID]);
    }

    //更新场景队列-加入

    public static void pushNewScene(string sceneName)
    {
        sceneName.Insert(0, sceneName);
    }
}

