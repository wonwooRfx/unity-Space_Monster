using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemgr : MonoBehaviour
{
    public GameObject monsterPrefab;
    public float createTime = 2; //몬스터 발생주기
    public int maxMonster = 2;
    public bool isGameOver = false;
    public Transform[] points;
    
    // Start is called before the first frame update
    void Start()
    {
        points = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();

        if(points.Length > 0)
        {
            StartCoroutine(CreateMonster());
        }
    }

    IEnumerator CreateMonster()
    {
        while(!isGameOver) //게임종료시 까지 무한루프
        {
            int monsterCount = (int)GameObject.FindGameObjectsWithTag("Monster").Length;//몬스터의 개수 산출

            //몬스터의 최대생성 개수보다 작을 때만 몬스터 생성
            if(monsterCount < maxMonster)
            {
                yield return new WaitForSeconds(createTime);
                //불규칙적인 위치 산출
                int idx = Random.Range(0, points.Length);
                //동적 생성
                Instantiate(monsterPrefab, points[idx].position, points[idx].rotation);
            }
            else
            {
                yield return null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
