using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0, Time.deltaTime * 50, 0, Space.World);        // 제자리에서 y축 기준 회전
    }
}
