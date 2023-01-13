using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsWallCtrl : MonoBehaviour
{
    public GameObject sparkEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision coll)
    {
        if(coll.collider.tag == "Bullet")
        {
            GameObject spark = Instantiate(sparkEffect, coll.transform.position, Quaternion.identity);
            Destroy(spark, 0.2f);
            Destroy(coll.gameObject);
        }
    }
}
