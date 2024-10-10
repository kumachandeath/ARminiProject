using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;  // 싱글톤 인스턴스

    public TextMeshProUGUI scoreText;    // 현재 스코어를 보여줄 UI
    public TextMeshProUGUI highScoreText; // 하이스코어를 보여줄 UI
    public int score;                    // 현재 스코어
    private int highScore;               // 하이스코어

    private void Awake()
    {
        // 싱글톤 패턴
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 게임 시작 시 하이스코어 로드
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScoreUI();
    }

    // 점수 증가 함수
    public void AddScore(int value)
    {
        score += value;
        UpdateScoreUI();

        // 하이스코어 갱신
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore); // PlayerPrefs에 저장
            PlayerPrefs.Save(); // 즉시 저장
        }
    }

    // 점수와 하이스코어 UI 갱신
    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
        highScoreText.text = "High Score: " + highScore.ToString();
    }

    // 게임 재시작 시 스코어 초기화
    public void ResetScore()
    {
        score = 0;
        UpdateScoreUI();
    }

    // 게임 종료 시 하이스코어를 저장
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }
}
