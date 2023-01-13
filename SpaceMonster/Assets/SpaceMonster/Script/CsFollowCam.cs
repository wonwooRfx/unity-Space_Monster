using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsFollowCam : MonoBehaviour
{
    public Transform targetTr;
    public float dist = 10f;
    public float height = 3f;
    //부드러운 추적을 위한 변수
    public float damTrace = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3.Lerp(Vector3 시작위치, Vector3 종료위치, float 시간)
        transform.position = Vector3.Lerp(transform.position, targetTr.position - (targetTr.forward * dist) + (Vector3.up * height), Time.deltaTime * damTrace);
        transform.LookAt(targetTr.position);
    }
}
