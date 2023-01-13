using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsSpwan : MonoBehaviour
{
    public GameObject _barrel; //������ ������
    // public GameObject[] _barrel;
    private BoxCollider area; //�ڽ� �ݶ��̴��� ����� �������� ���� ����
    public int count = 15; // �� ���� ������Ʈ ����
    // private List<GameObject> bGameObject = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        area = GetComponent<BoxCollider>();
       
        for(int i =0; i<count; ++i)
        {
            Spawn(); //���� + ������ġ�� �����ϴ� �Լ�
        }
        area.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 GetRandomPosition()
    {
        
        Vector3 basePosition = transform.position; // 0,0,0
        Vector3 size = area.size; // 50,0,50

        float posX = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);
        float posY = basePosition.y; // +  Random.Range(-size.y / 2f, size.y / 2f);
        float posZ = basePosition.z + Random.Range(-size.z / 2f, size.z / 2f);

        Vector3 spawnPos = new Vector3(posX, posY, posZ);
        return spawnPos;
    }

    private void Spawn()
    {
        //int selection = Random.Range(0, prefabs.Length);
        //GameObject selectedPrefab = prefabs[selection];
        Vector3 spawmPos = GetRandomPosition();// ������ġ �Լ�
        GameObject instaance = Instantiate(_barrel, spawmPos, Quaternion.identity);
        //bGameObject.Add(instance);
    }
}
