using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;
using System.Collections;

public class PlayerMovementScript : MonoBehaviour {


    private bool jump;
    [SerializeField]
    float speed = 3f;

    [SerializeField]
    private float jumpForce = 400f;

     Animator anim;

    bool doubleJump = false;
    bool grounded = false;

    [SerializeField]
    private bool airControl = false; // Whether or not a player can steer while jumping;
    [SerializeField]
    private LayerMask whatIsGround; // A mask determining what is ground to the character
    private Transform groundCheck; // A position marking where to check if the player is grounded.
    private float groundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    

    // Use this for initialization
    void Start () {
        groundCheck = transform.Find("GroundCheck");
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!jump)
            jump = CrossPlatformInputManager.GetButtonDown("Jump");
        

    }

    void fixedUpdate()
    {
       // var move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
       // transform.position += move * speed * Time.deltaTime;
         Move(Input.GetAxis("Horizontal"), jump);
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
        if (grounded)
        {
            jump = false;
            doubleJump = false;
        }
    }

    public void Move(float move, bool jump)
    {
        //only control the player if grounded or airControl is turned on
        if (grounded || airControl)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(move * speed, GetComponent<Rigidbody2D>().velocity.y);
        }
        // If the player should jump...
        if ((grounded || !doubleJump) && jump /*&& anim.GetBool("Ground")*/)
        {
            // grounded = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0f);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
            if (!grounded)
                doubleJump = true;
        }
    }
}
