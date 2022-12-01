using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    
    [SerializeField]
    Rigidbody zrb;
    Animator zani;
    [SerializeField]
    private PlayerMove player;
    [SerializeField]
    private CamShake camShake;
    [SerializeField]
    private GameObject Particle;
    [SerializeField]
    private GameObject HitParticle;
    private ItemSpawner itemspawner;
    public AudioSource audioSource;
    


    
    private ZombieDieColor zombieDieColor; 

       public float slowFactor = 0.5f;

       


    bool isdie = false;
    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        zrb = GetComponent<Rigidbody>();
        zani = GetComponent<Animator>();
        player = FindObjectOfType<PlayerMove>();
        camShake = FindObjectOfType<CamShake>();
        itemspawner = FindObjectOfType<ItemSpawner>();
        zombieDieColor = GetComponentInChildren<ZombieDieColor>();
       
    }
    //좀비가 죽고 오브젝트풀에서 다시 나올때 상태를 초기화 해줌
    void OnEnable()
    {
        //죽을때 풀어줬던 축값을 다시 묶어줌
        zrb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        //죽음처리했떤 bool값을 초기화해줌
        isdie = false;
               
        //좀비가 죽었을때 빨간색으로 표시된걸 다신 원래의 색깔로 바꿔줌
        zombieDieColor.skinnedMeshRenderer.materials[0].color = Color.white;
        zombieDieColor.skinnedMeshRenderer.materials[1].color = Color.white;
        //좀비에 상속객체인 Hit파티클을 사용전 상태로 만들어줌
        HitParticle.SetActive(false);
        
        //좀비의 기본 상태인 MOVE로 코루틴 실행.
        nextState(STATE.MOVE);

    }

    private void Update()
    {   
        
        BoundaryCheck();
    }
    // 플레이어와 좀비가 일정거리 이상 떨어졌을때 체크해주는 함수
    void BoundaryCheck()
    {
        if (player.transform.position.x - transform.position.x > 25) // 플레이어의 x좌표와 맵의 좌표를 뺐을때 맵의 절반보다 클때 // 오른쪽으로 갈때
        {
            SetZombie(2);
        }
        if (player.transform.position.x - transform.position.x < -25) // 왼쪽으로 갈때
        {
            SetZombie(0);

        }
        if (player.transform.position.z - transform.position.z > 25) // 위로 갈때
        {
            SetZombie(1);

        }
        if (player.transform.position.z - transform.position.z < -25) // 아래로 갈때
        {
            SetZombie(3);
        }
    }

    //  BoundaryCheck로 플레이어의 방향과 거리를 확인하여 좀비 위치를 변환한다.
    void SetZombie(int dir)
    {
        switch (dir)
        {

            case 0:
                { transform.position += Vector3.left * 50; }
                break;
            case 1:
                { transform.position += Vector3.forward * 50; }
                break;
            case 2:
                { transform.position += Vector3.right * 50; }
                break;
            case 3:
                { transform.position += Vector3.back * 50; }
                break;

        }
    }

    



    //enum을 사용한 코루틴.
    Coroutine prevCoroutine = null;
    public enum STATE
    {
        NONE,   // 아무것도 아닌 상태
        MOVE,
        DEATH,
        
    }
    public STATE curState = STATE.NONE;
    

   
    // 코루틴의 STATE가 변경될때 코루틴이 실행되는 함수
    void nextState(STATE newState) 
    {
        if(newState == curState) // 기존상태랑 같으면 리턴
            return;
        if (prevCoroutine != null)
            StopCoroutine(prevCoroutine);

        //새로운상태로 변환
        curState = newState;
        prevCoroutine = StartCoroutine(newState.ToString());

    }

    
    IEnumerator MOVE()
    {
        
        while (!isdie)
        {
            
            //좀비는 플레이어 방향으로 앞으로 전진한다.
            Vector3 GO = (transform.forward * Time.deltaTime);
            zrb.MovePosition(zrb.position + GO);
            //좀비가 플레이어 방향으로 이동한다.
            transform.LookAt(player.transform);
            //플레이어 애니메이션 무브 작동.
            zani.SetFloat("MoveSpeed", Vector3.forward.z);


            yield return null;

        }
    }
    
    IEnumerator DEATH()
    {
        //좀비가 죽었을때 충격음 실행
        audioSource.Play();

        
        //좀비의 파티클 생성/ 오브젝트 풀에서 가져옴
        Vector3 transformY = new Vector3(0,0.5f,0);
        Particle = ParticlePool.Inst.CreateParticle(transform.position + transformY);
        //파티클이 비활성화 될때 setparent에 넣어줌
        Particle.transform.SetParent(transform);
        
        //파티클 비활성화를 0.5f초뒤에 실행
        StartCoroutine(HideParticle());
        //Hit파티클을 활성화합니다.
        HitParticle.SetActive(true);
        //좀비 Dead 애니메이션 출력
        zani.SetTrigger("Dead");
        //오브젝트풀 각각의 좀비와 파티클 넣기
        StartCoroutine(HideObj());
        
        isdie = true;
        // 리지드바디 축고정 풀기
        zrb.constraints = RigidbodyConstraints.None;

       
        //zrb.useGravity = false;

        //스코어 값을 UI매니저로 보내줌
        UIManager.Inst.Score(100);
        // 카메라 흔들기
        camShake.camShake();
        // 좀비가 죽으면 색깔을 바꿔줌
        zombieDieColor.Materia();
        // 슬로우모션
        SlowTime();
        // 슬로우모션으로 느려진 타임스케일을 원래대로 돌림
        StartCoroutine(JustTime());


        yield return null;
    }
    void SlowTime()
    {
        Time.timeScale = slowFactor;
        Time.fixedDeltaTime = 0.02f;
    }

    IEnumerator HideParticle()
    {

        yield return new WaitForSecondsRealtime(0.5f);
        
        
        itemspawner.Spawn(transform.position);
        
    }   

    IEnumerator HideObj()
    {

        yield return new WaitForSeconds(2f);
        
        ObjectPool.Instance.DestroyPrefab(gameObject);
        ParticlePool.Inst.DestroyParticle(Particle);
    }

    IEnumerator JustTime()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        if(Time.timeScale <= 1f)
        {
            Time.timeScale = 1f;
        }
    }
    

    void OnCollisionEnter(Collision collision)
    {
        //플레이어와 충돌했을때
        if (collision.gameObject.tag == "Player" )
        {
         // 살아있는 좀비를 속도 2로 충돌했을때 속도 -1
            if (player.speed > 2 && !isdie)
            {
                player.speed -= 1f;
            }
         // 살아있는 좀비를 속도5로 충돌했을때 좀비가 죽음
            if (player.speed > 5 && !isdie)
            {
            nextState(STATE.DEATH);

            }
            

        }

    }
}
