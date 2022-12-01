using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    //맵 배열 선언 
    [SerializeField] private GameObject[] LandArray;
    //플레이어 // 플레이어 좌표값을 받아오기위해서 가져온다 
    [SerializeField] private Transform player;
    //타일 크기 
    readonly float LandSize = 50f;

  

    
    private void Update()
    {
        // 맵의 타일 위치와 방향을 체크
        BoundaryCheck();
    }

    //타일의 어디로 옮길지 체크
    void BoundaryCheck()
    {   // 플레이어의 x좌표와 맵의 좌표를 뺐을때 맵의 절반보다 클때 // 오른쪽으로 갈때
        if (player.position.x - transform.position.x > LandSize / 2 ) 
        {
            MoveLand(2);
        }// 왼쪽으로 갈때
        if (player.position.x - transform.position.x < -LandSize / 2) 
        {
            MoveLand(0);

        } // 위로 갈때
        if (player.position.z - transform.position.z > LandSize / 2)
        {
            MoveLand(1);

        }// 아래로 갈때
        if (player.position.z - transform.position.z < -LandSize / 2) 
        {
            MoveLand(3);
        }
    }
    
    void MoveLand (int dir)
    {
        //기존 배열을 복사. LandArray는  새로운배열, _LandArray는 기존 배열을 뜻한다.
        GameObject[] _LandArray = new GameObject[9];
        System.Array.Copy(LandArray, _LandArray, 9);

        switch(dir)
        {
            case 0:
                {
                    transform.position += Vector3.left * LandSize; // 9개의좌표가들어있는 월드맵의 좌표를 50만큼이동함


                    _LandArray[0] = LandArray[2];
                    _LandArray[1] = LandArray[0];
                    _LandArray[2] = LandArray[1];
                    _LandArray[3] = LandArray[5];
                    _LandArray[4] = LandArray[3];
                    _LandArray[5] = LandArray[4];
                    _LandArray[6] = LandArray[8];
                    _LandArray[7] = LandArray[6];
                    _LandArray[8] = LandArray[7];

                    LandArray[2].transform.position += Vector3.left * 150;
                    LandArray[5].transform.position += Vector3.left * 150;
                    LandArray[8].transform.position += Vector3.left * 150;

                    System.Array.Copy(_LandArray, LandArray, 9);


                }
                break;
            case 1:
                {
                    transform.position += Vector3.forward * LandSize;

                    _LandArray[0] = LandArray[6];
                    _LandArray[1] = LandArray[7];
                    _LandArray[2] = LandArray[8];
                    _LandArray[3] = LandArray[0];
                    _LandArray[4] = LandArray[1];
                    _LandArray[5] = LandArray[2];
                    _LandArray[6] = LandArray[3];
                    _LandArray[7] = LandArray[4];
                    _LandArray[8] = LandArray[5];

                    LandArray[6].transform.position += Vector3.forward * 150;
                    LandArray[7].transform.position += Vector3.forward * 150;
                    LandArray[8].transform.position += Vector3.forward * 150;

                    System.Array.Copy(_LandArray, LandArray, 9);
                }
                break;
            case 2:
                {
                    transform.position += Vector3.right * LandSize;

                    _LandArray[0] = LandArray[1];
                    _LandArray[1] = LandArray[2];
                    _LandArray[2] = LandArray[0];
                    _LandArray[3] = LandArray[4];
                    _LandArray[4] = LandArray[5];
                    _LandArray[5] = LandArray[3];
                    _LandArray[6] = LandArray[7];
                    _LandArray[7] = LandArray[8];
                    _LandArray[8] = LandArray[6];

                    LandArray[0].transform.position += Vector3.right * 150;
                    LandArray[3].transform.position += Vector3.right * 150;
                    LandArray[6].transform.position += Vector3.right * 150;

                    System.Array.Copy(_LandArray, LandArray, 9);
                }
                break;
            case 3:
                {
                    transform.position += Vector3.back * LandSize;

                    _LandArray[0] = LandArray[3];
                    _LandArray[1] = LandArray[4];
                    _LandArray[2] = LandArray[5];
                    _LandArray[3] = LandArray[6];
                    _LandArray[4] = LandArray[7];
                    _LandArray[5] = LandArray[8];
                    _LandArray[6] = LandArray[0];
                    _LandArray[7] = LandArray[1];
                    _LandArray[8] = LandArray[2];

                    LandArray[0].transform.position += Vector3.back * 150;
                    LandArray[1].transform.position += Vector3.back * 150;
                    LandArray[2].transform.position += Vector3.back * 150;

                    System.Array.Copy(_LandArray, LandArray, 9);
                }
                break;

        }
    }

    }

   



