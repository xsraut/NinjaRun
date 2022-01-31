using System.Collections;
using UnityEngine;

public class Controller : MonoBehaviour
{

    public float startSpeed;
    public float startSpeedTemp;

    public float gravity = -9.18f;
    public float jumpHeight = 3f;
    public float slideTime = 1.5f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded = false;

    Animator animator;

    Vector3 velocity;


    CharacterController characterController;

    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode slide;

    //Initial Value Holders
    float characterControllerHight;
    Vector3 characterControllerCenter;


    private Vector3 forwardMove;
    public bool gameOver = false;

    private int rot = 1;

    int k = 1;
    float tempTime;
    public GameObject GameOverScreen;

    // Start is called before the first frame update

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        characterControllerHight = characterController.height;
        characterControllerCenter = characterController.center;

        gameOver = false;
        Time.timeScale = 1;

        startSpeed = startSpeedTemp;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!gameOver)
        {
            characterController.Move(forwardMove * Time.deltaTime);
        }



        switch (rot)
        {
            case 1:
                forwardMove = new Vector3(0, 0, startSpeed);
                transform.localEulerAngles = new Vector3(0, 0, 0);
                break;
            case 2:
                forwardMove = new Vector3(startSpeed, 0, 0);
                transform.localEulerAngles = new Vector3(0, 90, 0);
                break;
            case 3:
                forwardMove = new Vector3(0, 0, -startSpeed);
                transform.localEulerAngles = new Vector3(0, 180, 0);
                break;
            case 4:
                forwardMove = new Vector3(-startSpeed, 0, 0);
                transform.localEulerAngles = new Vector3(0, 270, 0);
                break;
        }


        if (Input.GetKeyDown(right) && rot == 1)
        {
            rot = 2;
        }
        else if (Input.GetKeyDown(right) && rot == 2)
        {
            rot = 3;
        }
        else if (Input.GetKeyDown(right) && rot == 3)
        {
            rot = 4;
        }
        else if (Input.GetKeyDown(right) && rot == 4)
        {
            rot = 1;
        }

        if (Input.GetKeyDown(left) && rot == 1)
        {
            rot = 4;
        }
        else if (Input.GetKeyDown(left) && rot == 2)
        {
            rot = 1;
        }
        else if (Input.GetKeyDown(left) && rot == 3)
        {
            rot = 2;
        }
        else if (Input.GetKeyDown(left) && rot == 4)
        {
            rot = 3;
        }



        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if((isGrounded && velocity.y < 0) && !gameOver)
        {
            velocity.y = -2f;
            animator.SetBool("jump", false);
        }
        
        //Velocity
        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

        //JUMP
        if (Input.GetKey(jump) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetBool("jump", true);
        }

        if (Input.GetKey(slide) && isGrounded)
        {
            animator.SetBool("slide", true);
            characterController.height = 0.4f;
            characterController.center = new Vector3(0, 0.43f, 0);
            StartCoroutine(slideWait());
        }


        if ((Time.time - tempTime >= 30 * k)) //((Time.time >= 30 * k) && k < 8)
        {
            startSpeed = startSpeedTemp + k;
            k++;
        }
        Debug.Log(Time.time);
        Debug.Log(k);
        Debug.Log(startSpeed);

        if (transform.position.y < -1)
        {
            gameOver = true;
        }

        if (gameOver)
        {
            tempTime = Time.time;
            k = 1;
            Debug.Log("game over");
            startSpeed = startSpeedTemp;

            //characterController.center = new Vector3(0, 1.5f, 0);
            animator.SetBool("gameover", true);

            Time.timeScale = 0.25f;

            GameOverScreen.SetActive(true);
        }
    }

    IEnumerator slideWait()
    {
        yield return new WaitForSeconds(slideTime);
        characterController.height = characterControllerHight;
        characterController.center = characterControllerCenter;
        animator.SetBool("slide", false);
    }

    IEnumerator jumpWait()
    {
        yield return new WaitForSeconds(slideTime);
        characterController.height = characterControllerHight;
        characterController.center = characterControllerCenter;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.collider.tag == "Obstacle")
        {
            gameOver = true;
        }
    }
}
