using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsFireCtrl : MonoBehaviour
{
    //[RequireComponent(typeof(AudioSource))] 컴포넌트삭제 방지
    public GameObject bulletFactory;
   
    public Transform firePos;

   // public AudioClip
   // public AudioSource = source; 
   // public GameObject muzzle;
    public MeshRenderer muzzleFlash;
   
    // Start is called before the first frame update
    void Start()
    {
        muzzleFlash.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(firePos.position, firePos.forward * 10f, Color.green); // 레이를 시각적으로 표시하기 위해 사용

        if(Input.GetButtonDown("Fire1"))
        {
            Fire();
            // muzzle.SetActive(true);

            RaycastHit hit;
            //레이를 발사해 맞은 게임오브젝트가 있을 떄 true를 반환
            if(Physics.Raycast(firePos.position, firePos.forward, out hit, 10f))
            {
                if(hit.collider.tag == "Monster")
                {
                    //SendMessege를 이용해 전달한 인자를 배열에 담음
                    // 모든데이터 형식은 오브젝트의 상속을 받기 때문에
                    // 모든데이터 형식으로 변환가능
                    object[] _params = new object[2];
                    _params[0] = hit.point; // 정확한 위치값(Vector3)
                    _params[1] = 20; // 몬스터에 데미지 입히는 함수 호출

                    hit.collider.gameObject.SendMessage("OnDamage", _params, SendMessageOptions.DontRequireReceiver);
                }

                if(hit.collider.tag == "Barrel")
                {
                    object[] _params = new object[2];
                    _params[0] = firePos.position;
                    _params[1] = hit.point;

                    hit.collider.gameObject.SendMessage("OnDamage", _params, SendMessageOptions.DontRequireReceiver);
                }
            }
           

        }
    }
    
   

    void Fire()
    {
       
        
       
        

        //Instantiate(bulletFactory,firePos.position,firePos.rotation);
        StartCoroutine(this.ShowMuzzleFlash());
        //muzzle.transform.Rotate(Random.Range(0, 180), Random.Range(0, 180), Random.Range(0, 180));
        // muzzle.transform.localScale = new Vector3(Random.Range(0.1f, 2f), Random.Range(0.1f, 2f), Random.Range(0.1f, 2f));

    }

    IEnumerator ShowMuzzleFlash()
    {
        float scale = Random.Range(1.0f, 2.0f);
        muzzleFlash.transform.localScale = Vector3.one * scale;

        Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0, 360));
        muzzleFlash.transform.localRotation = rot;

        muzzleFlash.enabled = true; 

       yield return new WaitForSeconds(Random.Range(0.05f,0.3f));
        // muzzle.SetActive(false);
        muzzleFlash.enabled = false;
    }
   

    
}
