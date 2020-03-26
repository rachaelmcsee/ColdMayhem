using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
//I learned this code from the youtuber Brackeys
//by Andrew Mabry (Drew)
public class Movement : MonoBehaviour
{
    //making the variables for movement
    public CharacterController thisController;
    public float moveSpeed = 10f;
    //making the variables for falling/jumping
    public float gravity = -10f;
    Vector3 velocity;

    //making variables to check if the player is on the ground
    public Transform bottom;
    public LayerMask groundMask;
    bool isGrounded;
    public float groundDistance = .3f;
    public float jumpHeight = 5f;
    //making a variable for the throw code
    Throw thisThrow;
    
    //getting the hp from the player so you can tell if you are dead so you freeze
    HP HP;
    float currentHP;
    public bool isDead = false;

    private void Start()
    {
        //getting a refference to the HP code
        HP = GetComponent<HP>();
        //getting a refference to the Throw code
        thisThrow = GetComponent<Throw>();

    }
    // Update is called once per frame
    void Update()
    {
        //getting the value of your current HP
        currentHP = HP.currentHP;

        //if the HP is 0 or less then the player is set to the dead state
        if (currentHP <= 0)
            isDead = true;

        //checking to see if the player is close to the ground so that if they are their volocity is not accelerating
        isGrounded = Physics.CheckSphere(bottom.position, groundDistance, groundMask);

        //if they are on the ground it sets their volocity to a low number just to make sure it doesn't increase but also that their feet look on the ground
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        //getting the users input and storing them in a float variable
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        //uses the input to determine what direction they go depending on where they are facing
        Vector3 move = transform.right * x + transform.forward * z;

        //if the player is dead they can't move
        if (!isDead)
        {
            //reducing speed based on if they are charging the
            if (thisThrow.isCharging)
            {
                thisController.Move(move * moveSpeed / 2 * Time.deltaTime);
            }
            else
            {
                if(Input.GetButton("Sprint"))
                {
                    //this uses the characterController and the information above to move the character using the move Method
                    thisController.Move(move * moveSpeed * 1.5f * Time.deltaTime);
                }
                else
                {
                    //this uses the characterController and the information above to move the character using the move Method
                    thisController.Move(move * moveSpeed * Time.deltaTime);
                }
               
            }
        }


        velocity.y += gravity * Time.deltaTime;

        thisController.Move(velocity * Time.deltaTime);
        
        

        //making an if statement for jumping
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

    }
}
