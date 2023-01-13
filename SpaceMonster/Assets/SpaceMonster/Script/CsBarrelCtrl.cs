using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsBarrelCtrl : MonoBehaviour
{
    public GameObject expEffect;
    public Texture[] textures;

    int hitCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        hitCounter = 0;

        int idx = Random.Range(0, textures.Length);
        GetComponentInChildren<MeshRenderer>().material.mainTexture = textures[idx];
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDamage(object[]_params)
    {
        Vector3 firePos = (Vector3)_params[0]; //발사위치
        Vector3 hitPos = (Vector3)_params[1]; // 드럼통에 맞은 hit 위치
        Vector3 incomVector = hitPos - firePos; // 입사벡터 (Ray의 각도) = 맞은 좌표 - 발사 원점

        //입사각 정규화
        incomVector = incomVector.normalized;
        //레이의 히트 좌표에 입사벡터의 각도로 힘을 생성(가해지는 힘방향, 어느위치에서)
        GetComponent<Rigidbody>().AddForceAtPosition(incomVector * 1000f, hitPos);
         
        // 총알 맞은 횟수를 증가시키고 3회 이상이면 폭발 처리
        if (++hitCounter >= 3)
        {
            ExpBarrel();
        }
    }

    /*private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == "Bullet")
        {
            Destroy(coll.gameObject);
            hitCounter = hitCounter + 1;

            // 총알 맞은 횟수를 증가시키고 3회 이상이면 폭발 처리
            if (hitCounter >= 3)
            {
                ExpBarrel();
            }
        }
        
    }*/

    void ExpBarrel()
    {
        
        GameObject exEff = Instantiate(expEffect, transform.position, Quaternion.identity);
        Destroy(exEff, 1f);

        //지정한 원점을 중심으로 10 반경 내에 들어와 있는 Collider 객체 추출 (원점, 반경)
        Collider[] colls = Physics.OverlapSphere(transform.position, 10f);

        //추출한 Collider 객체에 폭발력 전당
        foreach (Collider coll in colls)
        {
            Rigidbody rbody = coll.GetComponent<Rigidbody>();
            if(rbody != null)
            {
                rbody.mass = 1f;
                //Rigidbody.AddExplosionForce(폭발력,원점,반경,위로 솟구치는 힘)
                rbody.AddExplosionForce(1000f, transform.position, 10f, 300f);
            }
        }
        //5초 후에 드럼통 제거
        Destroy(this.gameObject, 5f);
       
    }
}
