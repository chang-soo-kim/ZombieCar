using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] items; //생성할 아이템
    int SpawnRand;




    // 아이템 생성
    public void Spawn(Vector3 zombie)
    {
        //좀비의 좌표를 가져온다.
        Vector3 spawnPosition = zombie;
                spawnPosition.y = 0;

        //아이템을 2/10 확률로 생성
        SpawnRand = Random.Range(0, 10);

        if (SpawnRand == 1 || SpawnRand == 5)
        {
            // 아이템 중 하나를 무작위로 골라 랜덤 위치에 생성
            GameObject selectedItem = items[Random.Range(0, items.Length)];

            ObjectPool.Instance.CreatePrefab(selectedItem, spawnPosition, Quaternion.identity, gameObject.transform);

            //Instantiate(selectedItem, spawnPosition, Quaternion.identity);
        }
    }

}
