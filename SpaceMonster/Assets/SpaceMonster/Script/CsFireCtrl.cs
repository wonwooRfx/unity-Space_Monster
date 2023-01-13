using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsFireCtrl : MonoBehaviour
{
    //[RequireComponent(typeof(AudioSource))] ������Ʈ���� ����
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
        Debug.DrawRay(firePos.position, firePos.forward * 10f, Color.green); // ���̸� �ð������� ǥ���ϱ� ���� ���

        if(Input.GetButtonDown("Fire1"))
        {
            Fire();
            // muzzle.SetActive(true);

            RaycastHit hit;
            //���̸� �߻��� ���� ���ӿ�����Ʈ�� ���� �� true�� ��ȯ
            if(Physics.Raycast(firePos.position, firePos.forward, out hit, 10f))
            {
                if(hit.collider.tag == "Monster")
                {
                    //SendMessege�� �̿��� ������ ���ڸ� �迭�� ����
                    // ��絥���� ������ ������Ʈ�� ����� �ޱ� ������
                    // ��絥���� �������� ��ȯ����
                    object[] _params = new object[2];
                    _params[0] = hit.point; // ��Ȯ�� ��ġ��(Vector3)
                    _params[1] = 20; // ���Ϳ� ������ ������ �Լ� ȣ��

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
