using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public float cooldown = 3;
    float timeAtLastSpawn = 0;
    [SerializeField] GameObject[] obj = null;
    void Update()
    {
        if ((Input.GetMouseButtonDown(0)) && (Time.time - timeAtLastSpawn) > cooldown)
        {
            timeAtLastSpawn = Time.time;
            GameObject instance = (GameObject)Instantiate(obj[Random.Range(0, obj.GetLength(0))], Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
            instance.transform.parent = this.transform;
        }else if(Input.touchCount > 0 && (Time.time - timeAtLastSpawn) > cooldown)
        {
            timeAtLastSpawn = Time.time;
            GameObject instance = (GameObject)Instantiate(obj[Random.Range(0, obj.GetLength(0))], Camera.main.ScreenToWorldPoint(Input.touches[0].position), Quaternion.identity);
            instance.transform.parent = this.transform;
        }
    }
}
