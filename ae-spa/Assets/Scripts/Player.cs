using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject itemObj;  // 아이템
    public Space mySpace;       // 이동 기준
    Vector3 prePos;             // 마우스 기존 위치

    void Start()
    {
        float randTime = Random.Range(5.0f, 10.0f);         // 랜덤 시간 간격으로
        InvokeRepeating("SpawnItem", randTime, randTime);   // 아이템 반복 생성
    }

    void Update()
    {
        // 마우스 버튼을 누르지 않고 좌우 회전
        Vector3 deltaPos = Input.mousePosition - prePos;
        deltaPos *= (Time.deltaTime * 10);                  
        transform.Rotate(0, -deltaPos.x, 0, Space.World);       // y축 기준 회전

        // 마우스 좌측 버튼 클릭 시 평면에서 이동
        if (Input.GetMouseButton(0))
        {
            deltaPos *= (Time.deltaTime * 0.1f);
            transform.Translate(deltaPos.x, 0, deltaPos.y, Space.World);
        }
        prePos = Input.mousePosition;
    }

    void SpawnItem()    // 아이템 스폰
    {
        GameObject item = Instantiate(itemObj);     // 생성

        Vector3 randPos;                            // 랜덤 위치
        randPos.x = Random.Range(-0.2f, 0.2f);
        randPos.y = 0;
        randPos.z = Random.Range(-0.2f, 0.2f);

        item.transform.position = transform.position + randPos;     // 플레이어 위치 기준으로 랜덤 값 지정
        Destroy(item, 5.0f);    // 5초 후 사라짐
    }
}
