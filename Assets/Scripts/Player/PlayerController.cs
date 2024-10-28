using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //toc do
    [SerializeField] public float speed;
    [SerializeField] private float jumpPower;
    [SerializeField]private LayerMask groundLayer;
    [SerializeField] private int extraJumps;
    private int jumpCounter;
    [SerializeField] private float coyoteTime; //How much time the player can hang in the air before jumping
    private float coyoteCounter; //How much time passed since the player ran off the edge
    //thanh phan
    private PlayerInputAction playerController;
    private Rigidbody2D body;
    private BoxCollider2D boxCollider;
    private Animator anim; 
    //movement
    private Vector2 move;
    private bool isLookRight;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        playerController = new PlayerInputAction();
        playerController.Enable();
        isLookRight = true;
    }
    // Update is called once per frame
    void Update()
    {
        move = playerController.Player.Move.ReadValue<Vector2>();
        body.linearVelocityX = move.x * speed ;
        if((move.x>0 && !isLookRight) || (move.x<0 && isLookRight))
        {
            Flip();
        }
        anim.SetBool("run",move.x !=0);
        anim.SetBool("grounded",isGrounded());
        if(playerController.Player.Jump.triggered)
        {
                Jump();
        }
        if (isGrounded())
        {
            coyoteCounter = coyoteTime; //Reset coyote counter when on the ground
            jumpCounter = extraJumps; //Reset jump counter to extra jump value
        }
        else
            coyoteCounter -= Time.deltaTime; //Start decreasing coyote counter when not on the ground
        if (playerController.Player.Jump.triggered && body.linearVelocity.y > 0)
            body.linearVelocity = new Vector2(body.linearVelocity.x, body.linearVelocity.y / 2);
    }

    public void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
        isLookRight = !isLookRight;
    }
    void Jump()
    {
        if (coyoteCounter <= 0  && jumpCounter <= 0) return; 
        if (isGrounded())
                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
            else
            {
                //If not on the ground and coyote counter bigger than 0 do a normal jump
                if (coyoteCounter > 0)
                    body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
                else
                {
                    if (jumpCounter > 0) //If we have extra jumps then jump and decrease the jump counter
                    {
                        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
                        jumpCounter--;
                    }
                }
             }
    }
    bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,Vector2.down,0.2f,groundLayer);
        return raycastHit.collider != null;
    }
    public bool canAttack()
    {
        return move.x == 0 && isGrounded() ;
    }
}
