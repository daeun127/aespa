using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caMonsterCtrl : MonoBehaviour
{
    public float speed; // ���� �̵� �ӵ�
    Vector3 deltaPos;   // ���� ��ġ

    void Start()
    {
        collMonster.isDie = false;

        InvokeRepeating("SetrandPos", 1.0f, 2.0f);     // 2�ʸ��� �Լ� ȣ��
    }

    void SetrandPos()   // ���� ���� ����
    {
        deltaPos.x = Random.Range(-0.2f, 0.2f);
        deltaPos.y = 0;
        deltaPos.z = Random.Range(-0.2f, 0.2f);
    }

    void Update()
    {
        transform.Translate(deltaPos * speed * Time.deltaTime, Space.World);    // ��ġ �̵�
    }
}
