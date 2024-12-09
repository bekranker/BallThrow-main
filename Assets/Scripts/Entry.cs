using System.Collections;
using System.Collections.Generic;
//using CrazyGames;
using UnityEngine;
using UnityEngine.SceneManagement;
//using CoolMathSDK;

public class Entry : MonoBehaviour
{
    public GameObject SettingsCanvas;

    public void Play()
    {
        // GameObject.Find("CoolMath").GetComponent<CoolMathApiScript>().StartGame();
        //  GameObject.Find("CoolMath").GetComponent<CoolMathApiScript>().StartLevel(PlayerPrefs.GetInt("LastLevel", 1));
//        CrazyEvents.Instance.GameplayStart();
        SceneManager.LoadScene(PlayerPrefs.GetInt("LastLevel" , 1));

    }
    public void Levels()
    {
        //GameObject.Find("CoolMath").GetComponent<CoolMathApiScript>().StartGame();
      //  CrazyEvents.Instance.GameplayStart();
        SceneManager.LoadScene(31);
    }
    public void SettingBtn()
    {
        SettingsCanvas.SetActive(true);
    }
    public void SettingBtnExit()
    {
        SettingsCanvas.SetActive(false);
    }
}
