using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caMonsterBullet : MonoBehaviour
{
    public GameObject cabulletObj;  // �Ѿ� ������Ʈ
    GameObject player;

    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("caMonShot", 0f, 1.0f);   // 1�ʸ��� �Ѿ� �߻�
    }

    void caMonShot()
    {
        if(collMonster.isDie == false)  // ��� ���¿��� ���� ����
        {
            GameObject obj = Instantiate(cabulletObj);  // �Ѿ� ����

            Vector3 shotPos = transform.position + transform.up * 0.05f;    // �߻� ��ġ
            Vector3 moveDir = player.transform.position- transform.position;   // �÷��̾ ���� ���� ����
            moveDir.y = 0;

            obj.GetComponent<BulletMove>().SetPosDir(shotPos, moveDir);     // �Ѿ� �߻�
            Destroy(obj, 5);    // 5�� �� ����
        }
    }
}
