using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField]
    private int lives = 3;
    [SerializeField]

    private string name = "Mario";

    [SerializeField]
    private float jumpHeight=5, speed=5;

    private bool hasKey;

    private bool isOnGround;

    private Rigidbody2D rigidbody2DInstance;

    private float horizontalInput;
	// Use this for initialization
	void Start ()
    {
        rigidbody2DInstance = GetComponent<Rigidbody2D>();
        Debug.Log("Rawr");  //How to print to the console in Unity
        string pizza = "yum";
        Debug.Log(pizza);

        rigidbody2DInstance.gravityScale = 5;

        /*if(hasKey)
        {
            float openTimer = 5;

            for (int i = 0; i < openTimer; i++)
            {
                Debug.Log(i);
            }
            Debug.Log("Open the door after " + openTimer + " Seconds");
        }*/
    }
	
	// Update is called once per frame
	private void Update ()
    {
        //transform.Translate(0, -.01f, 0); don't use because of physics
        GetInput();
        Move();

    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }
    private void Move()
    {
        rigidbody2DInstance.velocity = new Vector2(horizontalInput, 0);
    }
    private void MoveRight()
    {
        rigidbody2DInstance.velocity = new Vector2(5, 0);
    }
}
