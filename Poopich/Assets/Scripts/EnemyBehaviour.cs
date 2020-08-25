using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && !Input.GetKeyDown(KeyCode.Space))
        {
            collision.gameObject.GetComponent<PlayerBehaviour>().RecountHP(1);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 15, ForceMode2D.Impulse);
        }

        if (collision.gameObject.tag == "Player" && gameObject.tag == "Mace")
        {
            Destroy(gameObject);
        }
    }
}
