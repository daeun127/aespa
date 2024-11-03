using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class cyMonsterBullet : MonoBehaviour
{
    public GameObject cybulletObj;

    void Start()
    {
        InvokeRepeating("cyMonShot", 0f, 1.0f);    // 1�ʸ��� �Ѿ� �߻�
    }

    void cyMonShot()
    {
        if(collMonster.isDie == false)  // ��� ���¿��� ���� ����
        {
            GameObject obj = Instantiate(cybulletObj);
            Vector3 shotPos = transform.position + transform.up * 0.05f;

            // ���� �������� �Ѿ� �߻�
            Vector3 dir;
            dir.x = Random.Range(-0.2f, 0.2f);
            dir.y = 0;
            dir.z = Random.Range(-0.2f, 0.2f);

            obj.GetComponent<BulletMove>().SetPosDir(shotPos, dir);
            Destroy(obj, 5);    // 5�� �� ����
        }
    }
}
