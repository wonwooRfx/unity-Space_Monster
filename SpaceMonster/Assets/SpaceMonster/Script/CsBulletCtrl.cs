using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsBulletCtrl : MonoBehaviour
{
    public int damage = 20;
    public float speed = 1000f;
    public int hitDamage = 0;
    // Start is called before the first frame update
    void Start()
    {
        //transform.forward = Camera.main.transform.forward;
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

   


}
