using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace UnitySampleAssets._2D
{
    [RequireComponent(typeof(PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D character;
        private bool jump;
        private float runDirection;
        private Animator anim; // Reference to the player's animator component.
    
        private void Awake()
        {
            character = GetComponent<PlatformerCharacter2D>();
            anim = GetComponent<Animator>();
        }
        private void Update()
        {
            if (!jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }
        private void FixedUpdate()
        {
            // Read the inputs.
            //bool crouch = Input.GetKey(KeyCode.LeftControl);
            //float h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            runDirection = Input.GetAxis("Horizontal");
            character.Move(runDirection, jump);
            jump = false;
        }
    }
}