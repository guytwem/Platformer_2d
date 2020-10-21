using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerController; // calling players rigidbody
    public Animator animator;

    [SerializeField]
    private float speed; // players speed
    [SerializeField]
    private float jumpForce; // players jump force

    public GameObject start; // where the player starts
    public GameObject gameOver; // game over text
    public GameObject restartButton; // restart button
    public GameObject enemy;
 
    private bool isGrounded; // is player on ground or not
    public Transform groundCheck; // is this object touching the ground
    public float checkRadius;
    public LayerMask whatIsGround; // what layers are the ground
    public LayerMask coins; // what layers are coins

    private int extraJumps; // how many jumps
    public int extraJumpsValue;
    

    public GameObject coin; // what gameobject is the coin

    private bool facingRight; // where is the player facing

    public Text jumpsTextUI;

    public int killValue = 1;

    public float animatorSpeed = 0f;

    [System.Serializable]
    public struct KeybindInputs
    {
        
       //public KeyCode Left;
       // public KeyCode Right;
       // public KeyCode Jump;
        
    }
    public KeybindInputs keybindInput;
    


    // Start is called before the first frame update
    void Start()
    {
        extraJumps = extraJumpsValue;
        playerController = GetComponent<Rigidbody2D>();

        gameOver.SetActive(false);
        restartButton.SetActive(false);
        Time.timeScale = 1;

      // keybindInput.Left = KeybindManager.keys["Left"];
        //keybindInput.Right = KeybindManager.keys["Right"];
        //keybindInput.Jump = KeybindManager.keys["Jump"];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround); // checking if the player is touching the ground


        float horizontal = Input.GetAxis("Horizontal"); // get horizontal input for player movement
        Movement(horizontal); // calling movement method

        Flip(horizontal); // calling player flip method


    }

    private void Update()
    {

        animatorSpeed = Input.GetAxisRaw("Horizontal"); // sets animator speed to the horizontal input

        animator.SetFloat("Speed",Mathf.Abs(animatorSpeed)); // makes animator swap idle to run when player moves. Mathf Abs makes the animatorspeed stay above 0.

        if (isGrounded == true) // if player is on the ground
        {
            extraJumps = extraJumpsValue; // reset the jumps
            jumpsTextUI.text = Convert.ToString(extraJumps); // update jump text
        }
        if (Input.GetButton("Jump") && extraJumps > 0) // if player jumps and has jumps left
        {
            //JUMP!
            playerController.velocity = Vector2.up * jumpForce;
            extraJumps--;
            jumpsTextUI.text = Convert.ToString(extraJumps);
        }
        else if (Input.GetButton("Jump") && extraJumps == 0 && isGrounded == true)
        {
            playerController.velocity = Vector2.up * jumpForce;
        }
        if (playerController.transform.position.y < -20) // if player falls then death screen
        {
            Debug.Log("Player Has Died");
            gameOver.SetActive(true);
            restartButton.SetActive(true);
            Pause();

        }


    }
    private void Movement(float horizontal)
    {
        playerController.velocity = new Vector2(horizontal * speed, playerController.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            gameOver.SetActive(true);
            restartButton.SetActive(true);
            Pause();
        }

        if(other.gameObject.CompareTag("Head"))
        {
            Destroy(enemy);
            ScoreManager.instance.KillCounter(killValue);
        }
    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }



    
}
