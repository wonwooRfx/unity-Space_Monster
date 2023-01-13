using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable] //인스펙터 뷰에 노출
public class Anim
{
    public AnimationClip idle;
    public AnimationClip runForward;
    public AnimationClip runBackward;
    public AnimationClip runRight;
    public AnimationClip runLeft;
}
public class CsPlayerCtrl : MonoBehaviour
{
    public float speed = 10;
    public float angleSpeed = 100;
    public int hp = 100;
    
    public Image  imgHpbar;
    private int initHp;

    public Anim anim;
    public Animation _animation;

    private GameUI gameUI;
    // Start is called before the first frame update
    void Start()
    {
        _animation = GetComponentInChildren<Animation>();
        //클립을 지정
        _animation.clip = anim.idle;
        // 애니메이션 실행
        _animation.Play();

        gameUI = GameObject.Find("GameUI").GetComponent<GameUI>();

        initHp = hp; // 생명 초기값 설정
       
    }
    private void Awake()
    {
        Cursor.visible = false;               // 마우스 커서를 보이지 않게
        Cursor.lockState = CursorLockMode.Locked;   // 마우스 커서 위치 고정

    }
    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        float mouseAngle = Input.GetAxis("Mouse X");

        //h = h * speed * Time.deltaTime;
        //v = v * speed * Time.deltaTime;
        transform.Translate(moveDir * Time.deltaTime * speed, Space.Self);
        //mouseAngle = mouseAngle * Time.deltaTime * angleSpeed;



        //transform.Translate(Vector3.right * h);
       // transform.Translate(Vector3.forward * v);
        transform.Rotate(Vector3.up * mouseAngle * Time.deltaTime * angleSpeed);

        if(v >= 0.1f)
        {
            //CrossFade : 애니메이션의 변가를 부드럽게 해주는 블렌딩 함수
            _animation.CrossFade(anim.runForward.name, 0.4f);
        }
        else if(v <= -0.1f)
        {
            _animation.CrossFade(anim.runBackward.name, 0.3f);
        }
        else if (h >= 0.1f)
        {
            _animation.CrossFade(anim.runRight.name, 0.3f);
        }
        else if (h <= -0.1f)
        {
            _animation.CrossFade(anim.runLeft.name, 0.3f);
        }
        else
        {
            _animation.CrossFade(anim.idle.name, 0.3f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Punch")
        {
            hp -= 10;
            Debug.Log("Player HP = " + hp.ToString());

            imgHpbar.fillAmount = (float)hp / (float)initHp; // 생명게이지 값 조절

            if (hp <= 0)
            {
                PlayerDie();
                SceneManager.LoadScene("Start"); // 시작씬으로 돌아가라
            }
        }
        
    }
    void PlayerDie()
    {
        Debug.Log("Player Die!!");
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        foreach (GameObject monster in monsters) // 모든 몬스터의 OnPlayerDie 함수를 순차적으로 호출
        {
            monster.SendMessage("OnPlayerDie()", SendMessageOptions.DontRequireReceiver); // private 함수 호출 방법
        }
        
    }

}
