using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsSpwan : MonoBehaviour
{
    public GameObject _barrel; //생성할 프리팹
    // public GameObject[] _barrel;
    private BoxCollider area; //박스 콜라이더의 사이즈를 가져오기 위한 변수
    public int count = 15; // 찍어낼 게임 오브젝트 갯수
    // private List<GameObject> bGameObject = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        area = GetComponent<BoxCollider>();
       
        for(int i =0; i<count; ++i)
        {
            Spawn(); //생성 + 스폰위치를 포함하는 함수
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
        Vector3 spawmPos = GetRandomPosition();// 랜덤위치 함수
        GameObject instaance = Instantiate(_barrel, spawmPos, Quaternion.identity);
        //bGameObject.Add(instance);
    }
}
