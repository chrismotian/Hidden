using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollector : MonoBehaviour
{
    [SerializeField] float cooldown = 3;
    float timeAlive = 0;
    void Update()
    {
        timeAlive = timeAlive + Time.deltaTime;
        if (timeAlive > cooldown)
        {
            Destroy(this.gameObject);
        }
    }
}
