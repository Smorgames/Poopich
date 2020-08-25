using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAlive : MonoBehaviour
{
    void Update()
    {
        StartCoroutine(Death());
    }
    IEnumerator Death()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
