using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAnimation : MonoBehaviour
{
    private float timer;
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 0.5f)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        if (timer >= 1f)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            timer = 0;
        }
    }
}
