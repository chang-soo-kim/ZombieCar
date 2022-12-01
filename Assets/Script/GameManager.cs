using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //----------------------------------------------------------------------
    #region ╫л╠шео
    static GameManager instance = null;
    public static GameManager Inst
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    instance = new GameObject("GameMgr").AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }
    #endregion
    //----------------------------------------------------------------------
    [SerializeField]
    private Text GameOver;
    [SerializeField]
    private Image GameOverImage;

    private void Start()
    {
        GameOverImage.gameObject.SetActive(false);
    }

    private void Update()
    {
        
        if (UIManager.Inst.isGameover == true)
        {
            Time.timeScale = 0;
            GameOver.text = "GAME OVER\n\nYOUR SCOER\n\n" + UIManager.Inst.score + "\n\n RETRY PRESS\n\"R\" KEY ";
            GameOverImage.gameObject.SetActive(true);
            if (Input.GetKeyUp(KeyCode.R))
            {
                SceneManager.LoadScene("SampleScene");
                Time.timeScale = 1;

            }
        }
       
    }

    
}