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
        Vector3 firePos = (Vector3)_params[0]; //�߻���ġ
        Vector3 hitPos = (Vector3)_params[1]; // �巳�뿡 ���� hit ��ġ
        Vector3 incomVector = hitPos - firePos; // �Ի纤�� (Ray�� ����) = ���� ��ǥ - �߻� ����

        //�Ի簢 ����ȭ
        incomVector = incomVector.normalized;
        //������ ��Ʈ ��ǥ�� �Ի纤���� ������ ���� ����(�������� ������, �����ġ����)
        GetComponent<Rigidbody>().AddForceAtPosition(incomVector * 1000f, hitPos);
         
        // �Ѿ� ���� Ƚ���� ������Ű�� 3ȸ �̻��̸� ���� ó��
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

            // �Ѿ� ���� Ƚ���� ������Ű�� 3ȸ �̻��̸� ���� ó��
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

        //������ ������ �߽����� 10 �ݰ� ���� ���� �ִ� Collider ��ü ���� (����, �ݰ�)
        Collider[] colls = Physics.OverlapSphere(transform.position, 10f);

        //������ Collider ��ü�� ���߷� ����
        foreach (Collider coll in colls)
        {
            Rigidbody rbody = coll.GetComponent<Rigidbody>();
            if(rbody != null)
            {
                rbody.mass = 1f;
                //Rigidbody.AddExplosionForce(���߷�,����,�ݰ�,���� �ڱ�ġ�� ��)
                rbody.AddExplosionForce(1000f, transform.position, 10f, 300f);
            }
        }
        //5�� �Ŀ� �巳�� ����
        Destroy(this.gameObject, 5f);
       
    }
}
