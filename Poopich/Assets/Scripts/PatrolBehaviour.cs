using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaviour : MonoBehaviour
{
    private bool canGo;
    private float speed = 4f;
    public Transform pointOne, pointTwo;
    void Start()
    {
        transform.position = new Vector3(pointOne.transform.position.x, pointOne.transform.position.y, transform.position.z);
        canGo = true;
    }
    void Update()
    {
        if(canGo)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointTwo.transform.position, speed * Time.deltaTime);
            if (transform.position == pointTwo.transform.position)
            {
                Transform point = pointOne;
                pointOne = pointTwo;
                pointTwo = point;
            }
        }
    }
}