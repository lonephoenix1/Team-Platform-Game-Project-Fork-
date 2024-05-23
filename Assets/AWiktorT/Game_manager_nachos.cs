using UnityEngine;
using UnityEngine.UI;

public class Game_manager_nachos : MonoBehaviour
{
    public static Game_manager_nachos Instance;
    public Text scoreText;
    public Text timerText;
    public float gameTime = 60f;    
    private int score = 0;
    private float timeRemaining;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        timeRemaining = gameTime;
    }

    void Update()
    {
        timeRemaining -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.Ceil(timeRemaining).ToString();

        if (timeRemaining <= 0)
        {
            EndGame();
        }
    }

    public void AddPoint()
    {
        score++;
        Debug.Log("+1 Point");
        scoreText.text = "Score: " + score.ToString();
    }

    void EndGame()
    {
        Debug.Log("Game Over! Final Score: " + score);
    }
}
