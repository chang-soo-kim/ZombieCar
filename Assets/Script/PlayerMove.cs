using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    readonly float rotateSpeed = 180f; 
    [SerializeField]
    private Rigidbody prb;
    [SerializeField]
    private GameObject playerparticle;
    public float speed;

    
    public AudioClip NormalSpped;
    

    AudioSource audioSource;



    public ZombieSpawner zombieSpawner;

    public float energyPlus = 0;
    
    public float InputRotate { get; private set; }
    


    void Start()
    {
        prb = GetComponent<Rigidbody>();

        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        InputRotate = Input.GetAxis("Horizontal");
        

        // 시간이 지날수록 가속도 올리기 / 무브스피드가 10일때 가속하지않음.
        if (speed < 10)
        {
            speed += Time.deltaTime;
            playerparticle.SetActive(false);
        }

        if (speed > 10)
        {
            playerparticle.SetActive(true);
            
        }
        
        Vector3 foward = transform.forward * speed * Time.deltaTime;
        prb.MovePosition(prb.position + foward);

        //상대적으로 회전할 수치 계산
        float turn = InputRotate * rotateSpeed * Time.deltaTime;
        //리지드바디를 이용해 게임 오브젝트 회전 변경
        prb.rotation *= Quaternion.Euler(0f, turn, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
            //충돌한 상대방으로부터 IItem 컴포넌트 가져오기 시도
            IItem item = other.GetComponent<IItem>();

            //충돌한 상대방으로부터 IItem 컴포넌트를 가져오는 데 성공했다면
            if (item != null)
            {
                // Use 메서드를 실행하여 아이템 사용
                item.Use(gameObject);
                UIManager.Inst.energy += energyPlus;
                energyPlus = 0f;
             }
    }


}
