using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    //[SerializeField]
    //public Zombie zombiePrefab;
    [SerializeField]
    private PlayerMove player;
    float timeAfterSpawn;
    float spawnRate;

    [SerializeField] GameObject zombie;
    
    void Start()
    {
        // 좀비 스폰 랜덤시간
        spawnRate = Random.Range(0.6f, 1f);
    }

    


    private void Update()
    {
        //좀비를 생성한다.
        CreateZombie();
    }

    
    private void CreateZombie()
    {
        // 스폰 랜덤 좌표
        float Spawn_x = Random.Range(-5f,5f), Spawn_z= Random.Range(-5f, 5f);
        Vector3 SpawnPoint = new Vector3(Spawn_x,0,Spawn_z)+ player.transform.position;

        
        
        
        //timeAfterSpawn 갱신
        timeAfterSpawn += Time.deltaTime;

        //최근 생성 시점에서부터 누적된 시간이 생성 주기보다 크거나 같다면
        if (timeAfterSpawn >= spawnRate)
        {
            //timeAfterSpawn 초기화
            timeAfterSpawn = 0f;
            //오브젝트 풀에서 플레이어 주위랜덤 스폰 좀비 생성
            ObjectPool.Instance.CreatePrefab(zombie, SpawnPoint, Quaternion.identity, gameObject.transform);
            //생성주기 랜덤으로 다시주기.
            spawnRate = Random.Range(0.6f,1f);
        }
        
    }
}
