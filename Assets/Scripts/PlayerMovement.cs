using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 velocity;
    public float jumpVelocity;
    public float gravity;

    public Animator animator;
    public LayerMask wallMask;
    public LayerMask floorMask;
    public GameObject voidFallDetector;
    public AudioSource jumpSound;

    private SpriteRenderer spriteRenderer;
    
    private bool walk, walkRight, walkLeft, jump;

    public Player player;

    public enum PlayerState {
        jumping,
        idle,
        walking
    }

    private PlayerState playerState = PlayerState.idle;
    
    private bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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

            pos = CheckWallRays(pos, scale.x);

        } else {
            animator.SetFloat("Speed", 0);
        }

        if (jump && playerState != PlayerState.jumping) {
            playerState = PlayerState.jumping;
            velocity = new Vector2(velocity.x, jumpVelocity);
            animator.SetBool("IsJumping", true);
            jumpSound.Play();
        }

        //if (playerState == PlayerState.jumping) {
        if(velocity.y > 0) {
            pos.y += velocity.y * Time.deltaTime;
            velocity.y -= gravity * Time.deltaTime;
        }

        if(velocity.y <= 0) {
            pos = CheckFloorRays(pos);
            animator.SetBool("IsJumping", false);
        }

        if(velocity.y > 0) {
            pos = CheckCeilingRays(pos);
        }

        transform.localPosition = pos;
        transform.localScale = scale;
    }

    private void CheckPlayerInput() {
        bool inputLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        bool inputRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        bool inputSpace = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);

        walk = inputLeft || inputRight;

        walkLeft = inputLeft && !inputRight;
        walkRight = !inputLeft && inputRight;
        
        jump = inputSpace;
    }

    Vector3 CheckWallRays(Vector3 pos, float direction) {
        Vector2 originTop = new Vector2(pos.x + direction *.4f, pos.y + 0.5f - 0.2f);
        Vector2 originMiddle = new Vector2(pos.x + direction *.4f, pos.y);
        Vector2 originBottom = new Vector2(pos.x + direction *.4f, pos.y - 0.5f + 0.2f);

        RaycastHit2D wallTop = Physics2D.Raycast(originTop, new Vector2(direction, 0), velocity.x * Time.deltaTime, wallMask);
        RaycastHit2D wallMiddle = Physics2D.Raycast(originMiddle, new Vector2(direction, 0), velocity.x * Time.deltaTime, wallMask);
        RaycastHit2D wallBottom = Physics2D.Raycast(originBottom, new Vector2(direction, 0), velocity.x * Time.deltaTime, wallMask);

        if(wallTop.collider != null || wallMiddle.collider != null || wallBottom.collider != null) {
            pos.x -= velocity.x * Time.deltaTime * direction;
        }

        return pos;
    }

    Vector3 CheckFloorRays (Vector3 pos) {
        Vector2 originLeft = new Vector2(pos.x - 0.5f + 0.2f, pos.y - 0.6f);
        Vector2 originMiddle = new Vector2(pos.x, pos.y - 0.6f);
        Vector2 originRight = new Vector2(pos.x + 0.5f - 0.2f, pos.y - 0.6f);

        RaycastHit2D floorLeft = Physics2D.Raycast(originLeft, Vector2.down, velocity.y * Time.deltaTime, floorMask);
        RaycastHit2D floorMiddle = Physics2D.Raycast(originMiddle, Vector2.down, velocity.y * Time.deltaTime, floorMask);
        RaycastHit2D floorRight = Physics2D.Raycast(originRight, Vector2.down, velocity.y * Time.deltaTime, floorMask);

        if(floorLeft.collider != null || floorMiddle.collider != null || floorRight.collider != null) {
            RaycastHit2D hitRay = floorRight;

            if(floorLeft) {
                hitRay = floorLeft;
            } else if(floorMiddle) {
                hitRay = floorMiddle;
            } else if(floorRight) {
                hitRay = floorRight;
            }
        
            playerState = PlayerState.idle;
            isGrounded = true;
            if(velocity.y > 0) {
                pos.y = hitRay.collider.bounds.center.y + hitRay.collider.bounds.size.y / 2 + 0.5f;
            }
            velocity.y = 0;
            
            
        } else {
            if(playerState != PlayerState.jumping) {
                Fall();
            }
        }

        return pos;
    }

    Vector3 CheckCeilingRays(Vector3 pos) {
        Vector2 originLeft = new Vector2(pos.x - 0.5f + 0.2f, pos.y + 0.6f);
        Vector2 originMiddle = new Vector2(pos.x, pos.y + 0.5f);
        Vector2 originRight = new Vector2(pos.x + 0.5f - 0.2f, pos.y + 0.6f);

        RaycastHit2D ceilLeft = Physics2D.Raycast(originLeft, Vector2.up, velocity.y * Time.deltaTime, floorMask);
        RaycastHit2D ceilMiddle = Physics2D.Raycast(originMiddle, Vector2.up, velocity.y * Time.deltaTime, floorMask);
        RaycastHit2D ceilRight = Physics2D.Raycast(originRight, Vector2.up, velocity.y * Time.deltaTime, floorMask);

        if(ceilLeft.collider != null || ceilMiddle.collider != null || ceilRight.collider != null) {
            RaycastHit2D hitRay = ceilLeft;

            if(ceilLeft) {
                hitRay = ceilLeft;
            } else if(ceilMiddle) {
                hitRay = ceilMiddle;
            } else if(ceilRight) {
                hitRay = ceilRight;
            }

            
            pos.y = hitRay.collider.bounds.center.y - hitRay.collider.bounds.size.y / 2 - 0.5f;
            Fall();
        }
        return pos;
    }

    void Fall() {
        velocity.y = 0;
        playerState = PlayerState.jumping;
        Debug.Log("Fall method");
        isGrounded = false;
    }
}
