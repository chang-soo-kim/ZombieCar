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
        

        // �ð��� �������� ���ӵ� �ø��� / ���꽺�ǵ尡 10�϶� ������������.
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

        //��������� ȸ���� ��ġ ���
        float turn = InputRotate * rotateSpeed * Time.deltaTime;
        //������ٵ� �̿��� ���� ������Ʈ ȸ�� ����
        prb.rotation *= Quaternion.Euler(0f, turn, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
            //�浹�� �������κ��� IItem ������Ʈ �������� �õ�
            IItem item = other.GetComponent<IItem>();

            //�浹�� �������κ��� IItem ������Ʈ�� �������� �� �����ߴٸ�
            if (item != null)
            {
                // Use �޼��带 �����Ͽ� ������ ���
                item.Use(gameObject);
                UIManager.Inst.energy += energyPlus;
                energyPlus = 0f;
             }
    }


}