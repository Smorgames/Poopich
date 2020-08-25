using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appear : MonoBehaviour
{
    public GameObject canvas, video;
    void Start()
    {
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(60f);
        canvas.SetActive(true);
        video.SetActive(false);
    }
}
