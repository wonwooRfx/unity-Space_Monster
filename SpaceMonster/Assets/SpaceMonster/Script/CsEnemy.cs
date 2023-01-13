using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CsEnemy : MonoBehaviour
{
    public enum MonsterState // 몬스터의 상태 정보가 있는 Enumerable 변수 선언
    {
        idle, trace, attack, die
    };
    // 몬스터의 상태 정보를 저장할 Enum 변수
    public MonsterState monsterState = MonsterState.idle;

    public float traceDist = 10.0f; // 추적 사정거리
    public float attackDist = 2.0f; // 공격 사정거리
    private bool isDie = false; //몬스터 사망여부
    public GameObject bloodEffect;
    public GameObject bloodDecal;

    int hp = 100;

    // 속도 향상을 위해 각종 컴포넌트 변수 할당
    private Transform monsterTr;
    private Transform playerTr;
    NavMeshAgent agent;

    Animator animator;

    private GameUI gameUI; //GameUI 스크립트 불러오는 함수
    // Start is called before the first frame update
    void Start()
    {
        monsterTr = this.gameObject.GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        gameUI = GameObject.Find("GameUI").GetComponent<GameUI>();

        //agent.destination = playerTr.position; //추적 대상의 위치를 설정하면 바로 추적 시작, 플레이어의 초기 위치 이후 플레이어의 위치를 따라가려면 플레이어의 위치를 갱신해 줘야함

        StartCoroutine(CheckMonsterState()); //일정한 간격으로 몬스터의 행동 상태를 체크하는 코루틴 함수 실행
        StartCoroutine(MonsterAction()); // 몬스터의 상태에 따라 동작하는 루틴을 실행하는 코루틴 함수 실행

        
    }

    IEnumerator CheckMonsterState()
    {
        while(!isDie)
        {
            yield return new WaitForSeconds(0.2f);//0.2초 동안 기다렸다가 다음으로 넘어감
           float dist = Vector3.Distance(monsterTr.position, playerTr.position);// 몬스터와 플레이어 사이의 거리 측정
            
            if(dist <= attackDist)
            {
               monsterState = MonsterState.attack;
                

            }

            else if(dist <= traceDist)
            {
                monsterState = MonsterState.trace;
               
            }
            else
            {
                monsterState = MonsterState.idle;
                
            }
            
            
        }
    }

    IEnumerator MonsterAction()
    {
        while (!isDie)
        {
            switch(monsterState)
            {
                case MonsterState.idle:
                    animator.SetBool("isTrace", false);
                    // agent.Stop();
                    break;

                case MonsterState.trace:
                    agent.destination = playerTr.position;
                    animator.SetBool("isTrace", true);
                    animator.SetBool("isAttack", false);
                    // agent.Resume();
                    break;
                case MonsterState.attack:
                    animator.SetBool("isAttack", true);
                    break;
            }
            yield return null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag =="Bullet")
        {

            CreateBloodEffect(coll.transform.position);
            animator.SetTrigger("isHit");
            Destroy(coll.gameObject);

            hp -= coll.gameObject.GetComponent<CsBulletCtrl>().damage;

            if(hp <= 0)
            {
                MonsterDie();
                
            }

            
        }
    }

    void CreateBloodEffect(Vector3 pos)
    {
        GameObject blood = Instantiate(bloodEffect, pos, Quaternion.identity);
        Destroy(blood, 0.2f);

        Vector3 decalPos = monsterTr.position + (Vector3.up * 0.05f);// 데칼 생성위치 바닥에서 조금 올린 위치 산출
        Quaternion decalRot = Quaternion.Euler(90, 0, Random.Range(0, 360)); // 데칼의 회전값을 무작위로 설정
        GameObject blood2 = Instantiate(bloodDecal, decalPos, decalRot);
        float scale = Random.Range(1.5f, 3.5f); // 데칼 크기 불규칙적
        blood2.transform.localScale = Vector3.one * scale;
        Destroy(blood2, 5.0f);
    }

    void OnDamage(object[] _params) //몬스터가 레이에 맞았을 때 호출되는 함수
    {
        Debug.Log(string.Format("Hit ray {0} : {1}", _params[0], _params[1]));
        CreateBloodEffect((Vector3)_params[0]);
        hp -= (int)_params[1]; // 맞은 총알의 Damage를 추출해 몬스터hp차감
        if(hp <= 0)
        {
            MonsterDie();
        }

        animator.SetTrigger("isHit");// 트리거를 발생시키면 Any State에서 GoHit로 전이됨
    }
   void MonsterDie()
   {
        gameObject.tag = "Untagged"; // 사망한 몬스터의 태크를 바꿈
        StopAllCoroutines(); // 모든 코루킨을 정지()

        isDie = true;
        monsterState = MonsterState.die;
        //agent.Stop();
        animator.SetTrigger("isDie");

        gameObject.GetComponentInChildren<CapsuleCollider>().enabled = false; //콜라이더 비활성화

        foreach(Collider coll in gameObject.GetComponentsInChildren<SphereCollider>())
        {
            coll.enabled = false;
        }

       
        gameUI.DispScore(50); // GameUI의 스코어 누적과 스코어 표시 함수 DispScore 호출
       

        
        

   }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        

    }

    void OnPlayerDie()
    {
        StopAllCoroutines(); //몬스터의 상태를 체크하는 코루틴을 함수를 모두 정지시킴
        //agent.Stop();
        animator.SetTrigger("IsPlayerDie");
    }
}
