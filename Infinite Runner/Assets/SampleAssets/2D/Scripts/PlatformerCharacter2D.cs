﻿using UnityEngine;

namespace UnitySampleAssets._2D
{

    public class PlatformerCharacter2D : MonoBehaviour
    {


        private bool facingRight = true; // For determining which way the player is currently facing.

        [SerializeField]
        private float maxSpeed = 10f; // The fastest the player can travel in the x axis.
        [SerializeField]
        private float jumpForce = 400f; // Amount of force added when the player jumps.	

        [Range(0, 1)]
        [SerializeField]
        private float crouchSpeed = .36f;
        // Amount of maxSpeed applied to crouching movement. 1 = 100%

        [SerializeField]
        private bool airControl = false; // Whether or not a player can steer while jumping;
        [SerializeField]
        private LayerMask whatIsGround; // A mask determining what is ground to the character

        private Transform groundCheck; // A position marking where to check if the player is grounded.
        private float groundedRadius = .02f; // Radius of the overlap circle to determine if grounded
        private bool grounded = false; // Whether or not the player is grounded.
        private Transform ceilingCheck; // A position marking where to check for ceilings
        private float ceilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator anim; // Reference to the player's animator component.

        bool doubleJump = false;


        bool idle = true;
        bool standShootH = false;
        bool runShotH = false;
        bool Run = false;
        [SerializeField]
        private bool Jump = false;
        bool boostJump = false;

        private void Awake()
        {
            // Setting up references.
            groundCheck = transform.Find("GroundCheck");
            ceilingCheck = transform.Find("CeilingCheck");
            anim = GetComponent<Animator>();
        }
        private void Update()
        {
            anim.SetBool("Running", Run);
        }

        private void FixedUpdate()
        {
            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);

            if (grounded)
            {
                Jump = false;
                doubleJump = false;
            }
            if (!grounded)
                Jump = true;
            if (Input.GetAxis("Horizontal") != 0)
                Run = true;
            else
                Run = false;
            //anim.ResetTrigger("Jumping");
            anim.SetBool("DoubleJump", doubleJump);

            anim.SetBool("Jumping", Jump);
        }


        public void Move(float move, bool jump)
        {
            //Jump = jump;
            //only control the player if grounded or airControl is turned on
            if (grounded || airControl)
            {
                // Move the character
                GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
                  // If the input is moving the player right and the player is facing left...
                  if (move > 0 && !facingRight)
                      // ... flip the player.
                      Flip();
                      // Otherwise if the input is moving the player left and the player is facing right...
                  else if (move < 0 && facingRight)
                      // ... flip the player.
                      Flip();
            }
            // If the player should jump...
            if ((grounded || !doubleJump) && jump)
            {
                Jump = jump;
                anim.SetBool("Jumping", Jump);
               // anim.ResetTrigger("Jumping");
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0f);
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
                if (!grounded)
                {
                    doubleJump = true;
                    anim.SetBool("DoubleJump", doubleJump);
                }
            }

        }


         private void Flip()
         {
             // Switch the way the player is labelled as facing.
             facingRight = !facingRight;
       
             // Multiply the player's x local scale by -1.
             Vector3 theScale = transform.localScale;
             theScale.x *= -1;
             transform.localScale = theScale;
         }
        public void SetRun(bool boolean)
        {
            Run = boolean;
        }
        public void SetJump(bool boolean)
        {
            Jump = boolean;
        }
    }
}