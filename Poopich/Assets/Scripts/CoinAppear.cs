using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAppear : MonoBehaviour
{
    public GameObject coin1, coin2, platform;
    private int jumpCount;
    
    private void Start()
    {
        jumpCount = 0;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            jumpCount += 1;
            Debug.Log(jumpCount);
        }
    }
    private void Update()
    {
        if(jumpCount == 9)
        {
            coin1.SetActive(true);
            platform.SetActive(true);
            jumpCount = 10;
        }
        if (jumpCount == 13)
        {
            coin2.SetActive(true);
            platform.SetActive(true);
            jumpCount = 20;
        }
        if (jumpCount == 21)
        {
            platform.SetActive(false);
            jumpCount = 0;
            enabled = false;
        }
    }
}
