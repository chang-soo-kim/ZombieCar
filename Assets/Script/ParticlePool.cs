using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
 
    #region 싱글톤
    private static ParticlePool instance = null; //static으로 싱글톤 클래스 유일 선언
    public static ParticlePool Inst //프로퍼티 선언
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<ParticlePool>();

                if (instance == null)
                {
                    instance = new GameObject().AddComponent<ParticlePool>();
                }
            }
            return instance;// 인스턴스의 값이 null이 아니라면 SingletonClass 오브젝트를 찾아 _instance에 할당합니다.
        }
    }
    #endregion

    [SerializeField] GameObject ParticlePrefab;

    Queue<GameObject> pool = new Queue<GameObject>();
    
    
    public GameObject CreateParticle(Vector3 pos)
    {


        GameObject instParticle;
        // 처음에는 아무것도 없으니 생성하자
        if (pool.Count == 0)
        {
            // 인스턴시레이트로 생성 파티클 프리팹 생성
            // 로드한 프리팹을 이용해서 인스턴트 객체 한개를 만든다.

            instParticle = Instantiate(ParticlePrefab, pos, Quaternion.identity);

            return instParticle; //GameObject 타입으로 반환
        }



        // 기존의 목록 중에서 비활성화 된 것을 찾는다.
        // 지금 큐에서는 맨 앞에 있는 것 하나를 전달해 주면
        instParticle = pool.Dequeue();
        instParticle.transform.parent = null;
        instParticle.transform.SetPositionAndRotation(pos, Quaternion.identity);
        instParticle.SetActive(true);
        return instParticle;
    }

    // 사용한 파티클을 오브젝트 풀로 가져온다.
    public void DestroyParticle(GameObject particle)
    {
        particle.transform.parent = this.transform; // 부모위치에 생성
        particle.gameObject.SetActive(false); // 
        pool.Enqueue(particle); // pool 에 1개 늘어난다.


    }

}
