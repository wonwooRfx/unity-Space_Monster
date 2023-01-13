using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsFollowCam : MonoBehaviour
{
    public Transform targetTr;
    public float dist = 10f;
    public float height = 3f;
    //�ε巯�� ������ ���� ����
    public float damTrace = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3.Lerp(Vector3 ������ġ, Vector3 ������ġ, float �ð�)
        transform.position = Vector3.Lerp(transform.position, targetTr.position - (targetTr.forward * dist) + (Vector3.up * height), Time.deltaTime * damTrace);
        transform.LookAt(targetTr.position);
    }
}
