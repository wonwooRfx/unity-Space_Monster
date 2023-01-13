using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CsEnemy : MonoBehaviour
{
    public enum MonsterState // ������ ���� ������ �ִ� Enumerable ���� ����
    {
        idle, trace, attack, die
    };
    // ������ ���� ������ ������ Enum ����
    public MonsterState monsterState = MonsterState.idle;

    public float traceDist = 10.0f; // ���� �����Ÿ�
    public float attackDist = 2.0f; // ���� �����Ÿ�
    private bool isDie = false; //���� �������
    public GameObject bloodEffect;
    public GameObject bloodDecal;

    int hp = 100;

    // �ӵ� ����� ���� ���� ������Ʈ ���� �Ҵ�
    private Transform monsterTr;
    private Transform playerTr;
    NavMeshAgent agent;

    Animator animator;

    private GameUI gameUI; //GameUI ��ũ��Ʈ �ҷ����� �Լ�
    // Start is called before the first frame update
    void Start()
    {
        monsterTr = this.gameObject.GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        gameUI = GameObject.Find("GameUI").GetComponent<GameUI>();

        //agent.destination = playerTr.position; //���� ����� ��ġ�� �����ϸ� �ٷ� ���� ����, �÷��̾��� �ʱ� ��ġ ���� �÷��̾��� ��ġ�� ���󰡷��� �÷��̾��� ��ġ�� ������ �����

        StartCoroutine(CheckMonsterState()); //������ �������� ������ �ൿ ���¸� üũ�ϴ� �ڷ�ƾ �Լ� ����
        StartCoroutine(MonsterAction()); // ������ ���¿� ���� �����ϴ� ��ƾ�� �����ϴ� �ڷ�ƾ �Լ� ����

        
    }

    IEnumerator CheckMonsterState()
    {
        while(!isDie)
        {
            yield return new WaitForSeconds(0.2f);//0.2�� ���� ��ٷȴٰ� �������� �Ѿ
           float dist = Vector3.Distance(monsterTr.position, playerTr.position);// ���Ϳ� �÷��̾� ������ �Ÿ� ����
            
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

        Vector3 decalPos = monsterTr.position + (Vector3.up * 0.05f);// ��Į ������ġ �ٴڿ��� ���� �ø� ��ġ ����
        Quaternion decalRot = Quaternion.Euler(90, 0, Random.Range(0, 360)); // ��Į�� ȸ������ �������� ����
        GameObject blood2 = Instantiate(bloodDecal, decalPos, decalRot);
        float scale = Random.Range(1.5f, 3.5f); // ��Į ũ�� �ұ�Ģ��
        blood2.transform.localScale = Vector3.one * scale;
        Destroy(blood2, 5.0f);
    }

    void OnDamage(object[] _params) //���Ͱ� ���̿� �¾��� �� ȣ��Ǵ� �Լ�
    {
        Debug.Log(string.Format("Hit ray {0} : {1}", _params[0], _params[1]));
        CreateBloodEffect((Vector3)_params[0]);
        hp -= (int)_params[1]; // ���� �Ѿ��� Damage�� ������ ����hp����
        if(hp <= 0)
        {
            MonsterDie();
        }

        animator.SetTrigger("isHit");// Ʈ���Ÿ� �߻���Ű�� Any State���� GoHit�� ���̵�
    }
   void MonsterDie()
   {
        gameObject.tag = "Untagged"; // ����� ������ ��ũ�� �ٲ�
        StopAllCoroutines(); // ��� �ڷ�Ų�� ����()

        isDie = true;
        monsterState = MonsterState.die;
        //agent.Stop();
        animator.SetTrigger("isDie");

        gameObject.GetComponentInChildren<CapsuleCollider>().enabled = false; //�ݶ��̴� ��Ȱ��ȭ

        foreach(Collider coll in gameObject.GetComponentsInChildren<SphereCollider>())
        {
            coll.enabled = false;
        }

       
        gameUI.DispScore(50); // GameUI�� ���ھ� ������ ���ھ� ǥ�� �Լ� DispScore ȣ��
       

        
        

   }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        

    }

    void OnPlayerDie()
    {
        StopAllCoroutines(); //������ ���¸� üũ�ϴ� �ڷ�ƾ�� �Լ��� ��� ������Ŵ
        //agent.Stop();
        animator.SetTrigger("IsPlayerDie");
    }
}
