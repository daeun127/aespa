using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speedBullet;       // �Ѿ� �ӵ�
    Vector3 direction;          // �Ѿ� ����

    void Update()
    {
        Vector3 deltaPos = direction * speedBullet * Time.deltaTime;        // ��ġ �� = ���� * �ӵ� * �̵� �ð�
        transform.Translate(deltaPos);                                      // �Ѿ� �̵�
    }

    public void SetPosDir(Vector3 pos, Vector3 dir)         // �Ѿ� �߻� ��ġ, ���� �޾ƿ��� �Լ�
    {
        transform.position = pos;           // �Ѿ� ��ġ ����
        direction = dir;                    // �Ѿ� ���� ����

    }
}
