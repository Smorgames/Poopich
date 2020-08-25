using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    private bool isMoving, isGrounded, isYellowChestOpen = false, isPlayerTouchFinishFlag = false;
    public int score;
    public int amountOfHP, maxAmountOfHP;
    public float speed, jumpHeight, springPower;
    public bool yellowKey = false;
    public bool blueKey = false;
    Rigidbody2D rigidBody;
    Animator playerAnimator;
    public Transform groundCheck;
    GameObject door;
    public Text textAmountOfHP, coinAmount2, coinAmount, coinInformation;
    public GameObject blueKeyImage, yellowKeyImage, p1, p2, canvas, chestCoin, chestBlueKey, block, win;
    public GameObject hp1, hp2, hp3, hp4, hp5, hp6;
    public AudioClip[] audioEffects;
    void Start()
    {
        door = GameObject.FindWithTag("LockedDoor");
        playerAnimator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        score = 0;
        maxAmountOfHP = 6;
        amountOfHP = maxAmountOfHP;
    }

    void Update()
    {
        HPImageRecount();
        if (amountOfHP <= 0)
        {
            canvas.SetActive(true);
            rigidBody.bodyType = RigidbodyType2D.Static;
            enabled = false;
            gameObject.GetComponent<AudioSource>().PlayOneShot(audioEffects[5]);
        }
        coinInformation.text = score.ToString() + "/9";
        //textAmountOfHP.text = amountOfHP.ToString();
        coinAmount.text = score.ToString();
        coinAmount2.text = score.ToString() + "/9";
        GroundCheck();
        if (Input.GetAxis("Horizontal") != 0 && isGrounded)
        {
            Flip();
            playerAnimator.SetInteger("State", 1);
        }
        if (Input.GetAxis("Horizontal") == 0 && isGrounded)
        {
            playerAnimator.SetInteger("State", 0);
        }
        if (rigidBody.velocity.y > 0 && !isGrounded)
        {
            Flip();
            playerAnimator.SetInteger("State", 2);
        }
        if (rigidBody.velocity.y < 0 && !isGrounded)
        {
            Flip();
            playerAnimator.SetInteger("State", 3);
        }
    }

    void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rigidBody.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(audioEffects[11]);
            rigidBody.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
        }
    }

    public void RecountHP(int damage)
    {
        amountOfHP -= damage;
        gameObject.GetComponent<AudioSource>().PlayOneShot(audioEffects[6]);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Spring")
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
            rigidBody.AddForce(transform.up * springPower, ForceMode2D.Impulse);
            collision.GetComponent<Animator>().SetTrigger("Activation");
            gameObject.GetComponent<AudioSource>().PlayOneShot(audioEffects[0]);
        }
        if (collision.gameObject.tag == "YellowKey")
        {
            yellowKeyImage.SetActive(true);
            collision.gameObject.SetActive(false);
            yellowKey = true;
            gameObject.GetComponent<AudioSource>().PlayOneShot(audioEffects[2]);
        }
        if (collision.gameObject.tag == "YellowChest" && yellowKey == true && isYellowChestOpen == false)
        {
            collision.GetComponent<Animator>().SetBool("Open", true);
            gameObject.GetComponent<AudioSource>().PlayOneShot(audioEffects[8]);
            yellowKeyImage.SetActive(false);
            isYellowChestOpen = true;
            StartCoroutine(Wait());
        }
        if (collision.gameObject.tag == "BlueKey")
        {
            blueKeyImage.SetActive(true);
            collision.gameObject.SetActive(false);
            blueKey = true;
            gameObject.GetComponent<AudioSource>().PlayOneShot(audioEffects[2]);
        }
        if (collision.gameObject.tag == "Coin")
        {
            score += 1;
            collision.gameObject.SetActive(false);
            gameObject.GetComponent<AudioSource>().PlayOneShot(audioEffects[4]);
        }
        if (collision.gameObject.tag == "DeathZone")
        {
            amountOfHP = 0;
        }
        if (collision.gameObject.tag == "FinishFlag" && score == 9)
        {
            p1.SetActive(true);
            p2.SetActive(true);
            block.SetActive(true);
            if (isPlayerTouchFinishFlag == false)
            {
                gameObject.GetComponent<AudioSource>().PlayOneShot(audioEffects[10]);
                isPlayerTouchFinishFlag = true;
            }
            StartCoroutine(Win());
        }
        if (collision.gameObject.tag == "FinishFlag")
        {
            if (isPlayerTouchFinishFlag == false)
            {
                gameObject.GetComponent<AudioSource>().PlayOneShot(audioEffects[10]);
                isPlayerTouchFinishFlag = true;
            }
            block.SetActive(true);
            win.SetActive(true);
        }
        if (collision.gameObject.tag == "HPUp")
        {
            amountOfHP += 1;
            collision.gameObject.SetActive(false);
            gameObject.GetComponent<AudioSource>().PlayOneShot(audioEffects[3]);
        }
        if (collision.gameObject.tag == "Water")
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(audioEffects[9]);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "LockedDoor" && blueKey == true)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(audioEffects[12]);
            blueKeyImage.SetActive(false);
            door.GetComponent<Animator>().SetBool("IsOpen", true);
            door.GetComponent<Collider2D>().enabled = false;
        }
        if (collision.gameObject.tag == "DeathZone")
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(audioEffects[5]);
            canvas.SetActive(true);
            enabled = false;
        }
    }

    void Flip()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    void GroundCheck()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f);
        isGrounded = colliders.Length > 1;
    }

    public int CoinScore()
    {
        return score;
    }

    private void HPImageRecount()
    {
        if (amountOfHP > 6)
        {
            amountOfHP = 6;
        }
        if (amountOfHP >= 6)
        {
            hp6.GetComponent<Image>().color = new Color(1, 0, 0);
        }
        if (amountOfHP < 6)
        {
            hp6.GetComponent<Image>().color = new Color(0.2641509f, 0.2641509f, 0.2641509f); ;
        }
        if (amountOfHP >= 5)
        {
            hp5.GetComponent<Image>().color = new Color(1, 0, 0);
        }
        if (amountOfHP < 5)
        {
            hp5.GetComponent<Image>().color = new Color(0.2641509f, 0.2641509f, 0.2641509f); ;
        }
        if (amountOfHP >= 4)
        {
            hp4.GetComponent<Image>().color = new Color(1, 0, 0);
        }
        if (amountOfHP < 4)
        {
            hp4.GetComponent<Image>().color = new Color(0.2641509f, 0.2641509f, 0.2641509f); ;
        }
        if (amountOfHP >= 3)
        {
            hp3.GetComponent<Image>().color = new Color(1, 0, 0);
        }
        if (amountOfHP < 3)
        {
            hp3.GetComponent<Image>().color = new Color(0.2641509f, 0.2641509f, 0.2641509f); ;
        }
        if (amountOfHP >= 2)
        {
            hp2.GetComponent<Image>().color = new Color(1, 0, 0);
        }
        if (amountOfHP < 2)
        {
            hp2.GetComponent<Image>().color = new Color(0.2641509f, 0.2641509f, 0.2641509f); ;
        }
        if (amountOfHP >= 1)
        {
            hp1.GetComponent<Image>().color = new Color(1, 0, 0);
        }
        if (amountOfHP < 1)
        {
            hp1.GetComponent<Image>().color = new Color(0.2641509f, 0.2641509f, 0.2641509f); ;
        }
    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(5f);
        Application.LoadLevel("Win");
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        chestCoin.SetActive(true);
        chestBlueKey.SetActive(true);
    }
}
