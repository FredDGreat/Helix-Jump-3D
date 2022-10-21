using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool levelCompleted;
    public static bool mute = false;
    public static bool isGameStarted;

    public GameObject gameOverPanel;
    public GameObject levelCompletedPanel;
    public GameObject gamePlayPanel;
    public GameObject startMenuPanel;

    public GameObject player2;

    public GameObject camera1;
    public GameObject camera2;

    public static int currentLevelIndex;

    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI nextLevelText;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    public Slider gameProgressSlider;

    public readonly static string CURRENT_LEVEL_PREF = "CurrentLevelIndex";
    public readonly static string HIGH_SCORE_PREF = "HighScore";
    public readonly static string SELECTED_CHARACTER = "selected_character";

    public static int numberOfPassedRings;
    public static int score  = 0;
    public static int numberOfPlayers  = 2;

    private Player player;

    private void Awake()
    {
        //PlayerPrefs.DeleteKey(CURRENT_LEVEL_PREF);
        //PlayerPrefs.DeleteAll();
        currentLevelIndex = PlayerPrefs.GetInt(CURRENT_LEVEL_PREF, 1);
        numberOfPassedRings = 0;
        if(currentLevelIndex > 3)
        {
            player2.SetActive(true);
            camera1.SetActive(false);
            camera2.SetActive(true);
        }
        else
        {
            player2.SetActive(false);
            camera1.SetActive(true);
            camera2.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        //player2 = GameObject.Find("Player2").GetComponent<Player2>();
        Time.timeScale = 1;
        isGameStarted = gameOver = levelCompleted = false;
        highScoreText.text = "Best Score\n" + PlayerPrefs.GetInt(HIGH_SCORE_PREF, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //start the game when user clicks on the screen
        //for pc
        //if(Input.GetMouseButtonDown(0) && !isGameStarted)
        //for moibile and pc
        //if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !isGameStarted || Input.GetMouseButtonDown(0) && !isGameStarted)
        if(Input.GetMouseButtonDown(0) && !isGameStarted)//for pc
        {
            //make sure the user is not clicking on any game object
            //works for pc
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            //for mobile and pc
            /*if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return;*/
            StartGame();
        }
        //update ui
        currentLevelText.text = currentLevelIndex.ToString();
        nextLevelText.text = (currentLevelIndex+1).ToString();
        int progress = (int)(numberOfPassedRings * 100 / FindObjectOfType<HelixManager>().numberOfRings);
        gameProgressSlider.value = progress;
        scoreText.text = score.ToString();
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            if (Input.GetButtonDown("Fire1"))
            {
                if(score > PlayerPrefs.GetInt(HIGH_SCORE_PREF, 0))
                {
                    PlayerPrefs.SetInt(HIGH_SCORE_PREF, score);
                }
                score = 0;
                scoreText.text = score.ToString();
                SceneManager.LoadScene("Level");
            }
        }
        if(currentLevelIndex > 3) { 
            if (Player.taskCompleted && Player2.taskCompleted && !levelCompleted) { 
                levelCompleted = true;
                player.PlayAudio("win level");
            }
        }
        if (levelCompleted)
        {
            //Time.timeScale = 0;
            levelCompletedPanel.SetActive(true);
            if (Input.GetButtonDown("Fire1"))
            {
                if (score > PlayerPrefs.GetInt(HIGH_SCORE_PREF, 0))
                {
                    PlayerPrefs.SetInt(HIGH_SCORE_PREF, score);
                    scoreText.text = PlayerPrefs.GetInt(HIGH_SCORE_PREF, 0).ToString();
                }
                //score = 0;
                //scoreText.text = score.ToString();
                PlayerPrefs.SetInt(CURRENT_LEVEL_PREF, currentLevelIndex + 1);
                SceneManager.LoadScene("Level");
            }
        }
    }

    private void StartGame()
    {
        isGameStarted = true;
        gamePlayPanel.SetActive(true);
        startMenuPanel.SetActive(false);
    }
}
