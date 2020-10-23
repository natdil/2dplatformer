using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;
    public Text winText;
    public Text lives;
    public Text loseText;

    private int scoreValue = 0;
    private int livesNumber = 3;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;
    private bool facingRight = true;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        scoreValue = 0;
        winText.text = "";
        loseText.text = "";
        lives.text = livesNumber.ToString();
        anim = GetComponent<Animator>();
    }
     void update ()
     {
          if (Input.GetKeyDown(KeyCode.W))
        {
          musicSource.clip = musicClipOne;
          musicSource.Play();
        }
        
     }
void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
         
        if (Input.GetKey("escape"))
            {
                Application.Quit();
            }

        if (facingRight == false && hozMovement > 0)
            {
                 Flip();
            }
        else if (facingRight == true && hozMovement < 0)
            {
                 Flip();
            }
            if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetInteger("State", 0);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
          if (scoreValue == 8)
        {
            winText.text = "You did it! Game by Natalie Dilbeck";
            musicSource.clip = musicClipTwo;
            musicSource.Play();
        }
        if (collision.collider.tag == "Enemy")
        {
            livesNumber -= 1;
            lives.text = livesNumber.ToString();
            Destroy(collision.collider.gameObject);
        }
        if (livesNumber == 0)
        {
            loseText.text = "You lost! Game by Natalie Dilbeck";
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }

}