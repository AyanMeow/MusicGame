using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject settingPanel;
    public GameObject textTohid;
    public GameObject toggle;
    public GameObject musicSlider;
    public GameObject touchSlider;

    public static bool openDemo = false;
    public static float musicVolum = 1f;
    public static float touchVolum = 0.7f;
    void Awake()
    {
        toggle.GetComponent<Toggle>().isOn = openDemo;
        musicSlider.GetComponent<Slider>().value = musicVolum;
        touchSlider.GetComponent<Slider>().value = touchVolum;
    }

    private void Update()
    {
        openDemo = toggle.GetComponent<Toggle>().isOn;
        musicVolum = musicSlider.GetComponent<Slider>().value;
        touchVolum = touchSlider.GetComponent<Slider>().value;
    }


    public void panelCt()
    {
        if (settingPanel.activeSelf)
        {
            settingPanel.SetActive(false);
            textTohid.SetActive(true);
        }
        else
        {
            settingPanel.SetActive(true);
            textTohid.SetActive(false);
        }
    }

}
