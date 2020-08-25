using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombGenerator : MonoBehaviour
{
    public Vector3[] points;
    public GameObject bomb;
    private Vector3 pointPosition;
    private bool canSpawn = true;
    void Update()
    {
        if(canSpawn == true)
        {
            pointPosition = points[Random.Range(0, points.Length)];
            Instantiate(bomb, pointPosition, Quaternion.identity);
            StartCoroutine(BombSpawn());
        }
    }
    IEnumerator BombSpawn()
    {
        canSpawn = false;
        yield return new WaitForSeconds(3f);
        canSpawn = true;
    }
}
