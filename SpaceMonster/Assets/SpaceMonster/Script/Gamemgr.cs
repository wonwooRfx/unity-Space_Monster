using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemgr : MonoBehaviour
{
    public GameObject monsterPrefab;
    public float createTime = 2; //���� �߻��ֱ�
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
        while(!isGameOver) //��������� ���� ���ѷ���
        {
            int monsterCount = (int)GameObject.FindGameObjectsWithTag("Monster").Length;//������ ���� ����

            //������ �ִ���� �������� ���� ���� ���� ����
            if(monsterCount < maxMonster)
            {
                yield return new WaitForSeconds(createTime);
                //�ұ�Ģ���� ��ġ ����
                int idx = Random.Range(0, points.Length);
                //���� ����
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
