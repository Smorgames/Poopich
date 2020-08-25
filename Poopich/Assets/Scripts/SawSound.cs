using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawSound : MonoBehaviour
{
    public AudioClip sawSound;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(sawSound);
            print("dsd");
        }
    }
}
