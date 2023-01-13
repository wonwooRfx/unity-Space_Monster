using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    LineRenderer line;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();

        line.useWorldSpace = false; // 로컬좌표를 기준으로 변경
        line.enabled = false; // 초기에 비활성화
        line.SetWidth(0.3f, 0.0f); // 라인의 시작폭과 종료폭 설정
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position + (Vector3.up * 0.02f), transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue);

        if(Input.GetMouseButtonDown(0))
        {
            line.SetPosition(0, transform.InverseTransformPoint(ray.origin));

            if(Physics.Raycast(ray, out hit, 100f))
            {
                line.SetPosition(1, transform.InverseTransformPoint(hit.point));
            }
            else
            {
                line.SetPosition(1, transform.InverseTransformPoint(ray.GetPoint(100f)));
            }
            StartCoroutine(this.ShowLaserBeam());
        }
    }

    IEnumerator ShowLaserBeam()
    {
        line.enabled = true;
        yield return new WaitForSeconds(Random.Range(0.01f, 0.2f));
        line.enabled = false;
    }
}
