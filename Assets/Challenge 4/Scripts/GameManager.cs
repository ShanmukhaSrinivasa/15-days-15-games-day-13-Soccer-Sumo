using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Script Reference")]
    [SerializeField] private SpawnManagerX spawnManager;

    public bool isGameActive = false;

    [Header("Score info")]
    private int score = 0;
    private int highScore = 0;
    public int lives = 5;
    [SerializeField] private TextMeshProUGUI livesCount;
    [SerializeField] private TextMeshProUGUI scoreCount;
    [SerializeField] private TextMeshProUGUI go_scoreCount;
    [SerializeField] private TextMeshProUGUI highScoreCount;
    [SerializeField] private TextMeshProUGUI go_highScoreCount;
    public TextMeshProUGUI waveNumberCount;
    public TextMeshProUGUI boosterAvailable;
    public TextMeshProUGUI boosterNotAvailable;


    [Header("Canvas Groups")]
    [SerializeField] public CanvasGroup startGameCG;
    [SerializeField] public CanvasGroup gameCG;
    [SerializeField] public CanvasGroup gameOverCG;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Time.timeScale = 0;
    }

    void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore");
        highScoreCount.text = highScore.ToString();

        livesCount.text = lives.ToString();

        InvokeRepeating("IncrementScore", 2f, 1f);

        isGameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        livesCount.text = lives.ToString();
        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void GameStart()
    {
        ShowCG(gameCG);
        HideCG(startGameCG);
        HideCG(gameOverCG);

        Time.timeScale = 1;
    }

    public void GameOver()
    {
        ShowCG(gameOverCG);
        HideCG(gameCG);
        HideCG(startGameCG);

        Time.timeScale = 0;
        go_scoreCount.text = score.ToString();
        go_highScoreCount.text = highScore.ToString();
    }

    public void IncrementScore()
    {
        score++;
        scoreCount.text = score.ToString();

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("highScore", highScore);
        }
    }

    public void ShowCG(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    public void HideCG(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    public void PlayAgainButtonCallBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
