using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Ui : MonoBehaviour
{
    public Text scoreText;
    bool gameOver;
    int score = 0;
    int k = 0;
    float speed = 0;

    public bool gameStart = false;
    public Animator animator;
    public Animator ButtonAnimator;

    public GameObject scoreGameObject;

    public GameObject GameOverScreen;
    public GameObject KeyBinidings;

    float temptime;
    public float tempSpeed;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<CharacterController>().enabled = false;
        scoreGameObject.SetActive(false);
        GameOverScreen.SetActive(false);
        score = 0;

        k = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart)
        {
            gameOver = FindObjectOfType<Controller>().gameOver;
            speed = FindObjectOfType<Controller>().startSpeed;

            if (!gameOver)
            {
                score = (int)((Time.time-temptime)* speed);
                scoreText.text = score.ToString();
            }
        }
        if (!gameStart)
        {
            temptime = Time.time;
        }

    }

    public void statrGame()
    {
        FindObjectOfType<CharacterController>().enabled = true;
        FindObjectOfType<Controller>().startSpeed = tempSpeed;

        animator.SetBool("gameStart", true);
        gameStart = true;
        ButtonAnimator.SetBool("startGame", true);
        scoreGameObject.SetActive(true);
        KeyBinidings.SetActive(false);
    }

    public void retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);

    }

    public void Quit()
    {
        Application.Quit();
    }
}
