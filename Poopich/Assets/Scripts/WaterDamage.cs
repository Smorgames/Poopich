using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDamage : MonoBehaviour
{
    public float amountOfTimeToDie;
    private float timer;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            timer += Time.deltaTime;
            if (timer >= amountOfTimeToDie)
            {
                collision.GetComponent<PlayerBehaviour>().RecountHP(1);
                timer = 0;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            timer = 0;
        }
    }
}
