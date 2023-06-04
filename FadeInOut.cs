using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Sprites;

public class FadeInOut : MonoBehaviour
{
	public bool isStage;
	public GameObject[] otherTextNeed;
	public GameObject tour;
	public GameObject kuang;
	public GameObject score;

	SpriteRenderer tourS; 
	SpriteRenderer kuangS;
	private float fadeSpeed = 1.7f;
	public bool sceneStarting = true;
	private RawImage backImage;
	private TextMesh scoreT;

	static bool sceneEnding = false;
	void Start()
	{
		backImage = this.GetComponent<RawImage>();
		backImage.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        if (isStage)
        {
			tourS = tour.GetComponent<SpriteRenderer>();
			kuangS = kuang.GetComponent<SpriteRenderer>();
			scoreT = score.GetComponent<TextMesh>();
		}
	}

	void Update()
	{
		if (sceneStarting)
		{
			StartScene();
		}
		if (sceneEnding)
		{
			EndScene();
		}
	}
	// 渐现
	private void FadeToClear()
	{
		backImage.color = Color.Lerp(backImage.color, Color.clear, fadeSpeed * Time.deltaTime);
        if (otherTextNeed.Length>0)
        {
			foreach(var text in otherTextNeed)
            {
				text.GetComponent<TextMesh>().color= Color.Lerp(text.GetComponent<TextMesh>().color, Color.white, fadeSpeed * Time.deltaTime);
			}
        }
        if (isStage)
        {
			if(backImage.color.a < 0.5f)
            {
				tourS.color = Color.Lerp(tourS.color, Color.white, fadeSpeed * Time.deltaTime);
			}
			kuangS.color = Color.Lerp(kuangS.color, Color.white, fadeSpeed * Time.deltaTime);
			scoreT.color = Color.Lerp(scoreT.color, Color.white, fadeSpeed * Time.deltaTime);
		}
	}
	// 渐隐
	private void FadeToBlack()
	{
		backImage.color = Color.Lerp(backImage.color, Color.black, fadeSpeed * Time.deltaTime);
		if (otherTextNeed.Length > 0)
		{
			foreach (var text in otherTextNeed)
			{
				text.GetComponent<TextMesh>().color = Color.Lerp(text.GetComponent<TextMesh>().color, Color.black, fadeSpeed * Time.deltaTime);
			}
		}
		if (isStage)
		{
			tourS.color = Color.Lerp(tourS.color, Color.black, 2*fadeSpeed * Time.deltaTime);
			kuangS.color = Color.Lerp(kuangS.color, Color.black, fadeSpeed * Time.deltaTime);
			scoreT.color = Color.Lerp(scoreT.color, Color.black, fadeSpeed * Time.deltaTime);
		}
	}
	// 初始化时调用
	private void StartScene()
	{
		backImage.enabled = true;
		FadeToClear();
		if (backImage.color.a <= 0.01f)
		{
			backImage.color = Color.clear;
			backImage.enabled = false;
			sceneStarting = false;
		}
	}
	// 结束时调用
	public void EndScene()
	{
		backImage.enabled = true;
		FadeToBlack();
	}

}