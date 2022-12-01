using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScenes : MonoBehaviour
{
    //타이틀씬 스타트 게임 시작
    static void StartGame()
    {
        GameObject.Find("START").GetComponentInChildren<Text>().text = "START";
    }
    
    static void QuitGame()
    {
        GameObject.Find("Quit_Button").GetComponentInChildren<Text>().text = "Quit";
    }
    // 플레이를 누르면 씬을 게임씬을 로드한다.
    static public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }

    static public void Quit()
    {
        Application.Quit();
    }
    
  }