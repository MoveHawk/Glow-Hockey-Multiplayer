// Scripts/Managers/GameManager.cs
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Score Settings")]
    public int maxScore = 7;
    public Text player1ScoreText;
    public Text player2ScoreText;
    public Text winText;

    private int player1Score = 0;
    private int player2Score = 0;
    private PuckController puck;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        puck = FindObjectOfType<PuckController>();
        UpdateScoreUI();
        winText.gameObject.SetActive(false);
    }

    public void PlayerScored(int playerID)
    {
        if (playerID == 1)
        {
            player1Score++;
            Debug.Log("Player 1 scored! Total: " + player1Score);
        }
        else if (playerID == 2)
        {
            player2Score++;
            Debug.Log("Player 2 scored! Total: " + player2Score);
        }

        UpdateScoreUI();
        CheckWinCondition();
    }

    private void UpdateScoreUI()
    {
        if (player1ScoreText != null)
            player1ScoreText.text = player1Score.ToString();

        if (player2ScoreText != null)
            player2ScoreText.text = player2Score.ToString();
    }

    private void CheckWinCondition()
    {
        if (player1Score >= maxScore)
        {
            EndGame(1);
        }
        else if (player2Score >= maxScore)
        {
            EndGame(2);
        }
        else
        {
            // Reset puck for next round
            if (puck != null)
            {
                puck.ResetPuck();
            }
        }
    }

    private void EndGame(int winningPlayer)
    {
        winText.text = $"Player {winningPlayer} Wins!";
        winText.gameObject.SetActive(true);

        // Disable puck movement
        if (puck != null)
        {
            puck.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }

        Debug.Log($"Game Over! Player {winningPlayer} wins!");
    }

    public void RestartGame()
    {
        player1Score = 0;
        player2Score = 0;
        UpdateScoreUI();
        winText.gameObject.SetActive(false);

        if (puck != null)
        {
            puck.ResetPuck();
        }
    }
}