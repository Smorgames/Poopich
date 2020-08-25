using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainMacePatrol : MonoBehaviour
{
    public Transform[] points;
    public float speed;
    private int i = 0;
    private bool canGo;
    void Start()
    {
        transform.position = points[1].position;
        canGo = true; 
    }

    void Update()
    {
        if(canGo)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
            if(transform.position == points[i].position)
            {
                i++;
                if(points.Length - i < 1)
                {
                    i = 0;
                }
                canGo = false;
                StartCoroutine(Wait());
            }

        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
        canGo = true;
    }
}
