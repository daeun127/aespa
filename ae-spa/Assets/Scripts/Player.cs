using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject itemObj;  // ������
    public Space mySpace;       // �̵� ����
    Vector3 prePos;             // ���콺 ���� ��ġ

    void Start()
    {
        float randTime = Random.Range(5.0f, 10.0f);         // ���� �ð� ��������
        InvokeRepeating("SpawnItem", randTime, randTime);   // ������ �ݺ� ����
    }

    void Update()
    {
        // ���콺 ��ư�� ������ �ʰ� �¿� ȸ��
        Vector3 deltaPos = Input.mousePosition - prePos;
        deltaPos *= (Time.deltaTime * 10);                  
        transform.Rotate(0, -deltaPos.x, 0, Space.World);       // y�� ���� ȸ��

        // ���콺 ���� ��ư Ŭ�� �� ��鿡�� �̵�
        if (Input.GetMouseButton(0))
        {
            deltaPos *= (Time.deltaTime * 0.1f);
            transform.Translate(deltaPos.x, 0, deltaPos.y, Space.World);
        }
        prePos = Input.mousePosition;
    }

    void SpawnItem()    // ������ ����
    {
        GameObject item = Instantiate(itemObj);     // ����

        Vector3 randPos;                            // ���� ��ġ
        randPos.x = Random.Range(-0.2f, 0.2f);
        randPos.y = 0;
        randPos.z = Random.Range(-0.2f, 0.2f);

        item.transform.position = transform.position + randPos;     // �÷��̾� ��ġ �������� ���� �� ����
        Destroy(item, 5.0f);    // 5�� �� �����
    }
}
