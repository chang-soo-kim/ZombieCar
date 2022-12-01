using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPack : MonoBehaviour, IItem
{
    // 인터페이스를 사용해 아이템 만듬
    
    public float overspeed = 15f;//스피드팩을 먹었을시 속력
    public void Use(GameObject taget)
    { 
        PlayerMove playermove = taget.GetComponent<PlayerMove>();
        //플레이어 스피드를 오버스피드로 만들어준다.
        playermove.speed = overspeed;
        // 오브젝트 파괴.
        Destroy(gameObject);
    }
   
}
