using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 velocity;
    public Animator animator;

    private bool walk, walkRight, walkLeft, jump;
    
    [SerializeField]
    public bool isGrounded = false;
    
    Rigidbody2D RB;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerInput();

        UpdatePlayerPosition();  
    }

    private void UpdatePlayerPosition() {
        Vector3 pos = transform.localPosition;
        Vector3 scale = transform.localScale;

        if (walk) {
            if (walkLeft) {
                pos.x -= velocity.x * Time.deltaTime;
                scale.x = -1;
                animator.SetFloat("Speed", velocity.x);
            }
            if (walkRight) {
                pos.x += velocity.x * Time.deltaTime;
                scale.x = 1;
                animator.SetFloat("Speed", velocity.x);
            }
        } else {
            animator.SetFloat("Speed", 0);
        }

        if (jump && isGrounded) {
                RB.velocity = Vector2.up * velocity.y;
                isGrounded = false;
                animator.SetBool("IsJumping", true);
        }

        transform.localPosition = pos;
        transform.localScale = scale;
    }

    private void CheckPlayerInput() {
        bool inputLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        bool inputRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        bool inputSpace = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        walk = inputLeft || inputRight;

        walkLeft = inputLeft && !inputRight;
        walkRight = !inputLeft && inputRight;
        
        jump = inputSpace;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("ground"))
        {
            if(!isGrounded)
            {
                animator.SetBool("IsJumping", false);
                isGrounded = true;
            }
        }
    }
}
