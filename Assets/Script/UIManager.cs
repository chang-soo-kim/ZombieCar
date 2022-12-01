using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    //---------------------------------------------------------
    #region 싱글톤
    private UIManager() { }

    static UIManager instance;

    public static UIManager Inst
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();

                if (instance == null)
                {
                    instance = new GameObject("UIManager").AddComponent<UIManager>();
                }
            }
            return instance;
        }
    }
    #endregion
    //-------------------------------------------------------------------


    // 게임 오버 상태
    public bool isGameover; //{ get; set; } 

    public float energy = 100f;

    public float UISpeed;

    public int score;
    
    private PlayerMove player;
    [SerializeField]
    private Image EnergyUI;
    [SerializeField]
    private Image SpeedUI;
    [SerializeField]
    private Image OverSpeedUI;
    [SerializeField]
    private Text ScoreUI;
    [SerializeField]
    private Text SpeedText;

    private Text TimeText;

    private void Start()
    {
        player = FindObjectOfType<PlayerMove>();
    }

    void Update()
    {

        Energy();
        Speed();

    }


    //연료 게이지 // 시간으로 감
    public void Energy()
    {
        energy -= Time.deltaTime;

        EnergyUI.fillAmount = energy / 100F;

        if (energy <= 0)
        {
            isGameover = true;
        }
    }


    //속도 게이지 // 플레이어 speed로 계산
    public void Speed()
    {
        //플레이어 속도를 UI속도에 넣어줌
        UISpeed = player.speed;
        //제한속도를 넘어선 속도를 OverUi에 넣어줌
        float OverUISpeed = player.speed - 10f;
        //스피드 UI
        SpeedUI.fillAmount = UISpeed / 10f;
        if(UISpeed > 9f)
        {
            OverSpeedUI.fillAmount = OverUISpeed / 5f;
        }
        // 속도게이지안에 속도 텍스트 표시
        SpeedText.text = "SPEED\n" + (int)player.speed;
    }

    //스코어 값 받아오고 스코어 표시
    public void Score(int dir)
    {
        score += dir;
        ScoreUI.text = "Score : " + score;
    }

 
   


}
