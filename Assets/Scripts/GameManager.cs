using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    static public GameManager S;

    [Header("Ball: Set in Inspector")]
    public int maxBalls = 3;
    public GameObject ballPrefab;

    [Header("BricksLines: Set in Inspector")]
    public GameObject brickPrefab;
    public float bricksMargin = 0.5f;
    public int bricksAmount = 12;
    public int linesAmount = 6;

    [Header("UI: Set in Inspector")]
    public Text scoreUI;
    public Text ballsUI;
    public GameObject RestartPanel;

    private int _ballsLeft;
    private GameObject curBall;
    private Color[] colors = new Color[] { Color.green, Color.yellow, new Color(1f, 0.45f, 0), Color.red };

    private int ballsLeft
    {
        get { return _ballsLeft; }
        set 
        {
            _ballsLeft = value;
            ballsUI.text = _ballsLeft.ToString();
            ballsUI.material.color = colors[maxBalls - _ballsLeft];
        }
    }

    void Start()
    {
        S = this;
        GenerateBrickLines();
        CreateBall();
        ballsLeft = maxBalls;
    }

    public void IncreaseScore(int value)
    {
        int newScore = int.Parse(scoreUI.text) + value;
        scoreUI.text = GetPrefix(newScore) + newScore.ToString();
    }

    public void LostBall()
    {
        ballsLeft--;
        if(ballsLeft <= 0)
        {
            Invoke("CallRestartMenu", 1f);
        }
        else
        {
            CreateBall();
        }
    }

    private void CallRestartMenu()
    {
        RestartPanel.SetActive(true);

        int curScore = int.Parse(scoreUI.text);
        if (!PlayerPrefs.HasKey("BestScore"))
            PlayerPrefs.SetInt("BestScore", curScore);
        else if(PlayerPrefs.GetInt("BestScore") < curScore)
            PlayerPrefs.SetInt("BestScore", curScore);

        int bestScore = PlayerPrefs.GetInt("BestScore");

        Text bestScoreUI = RestartPanel.transform.Find("BestScore").GetComponent<Text>();
        bestScoreUI.text = "BEST SCORE: " + GetPrefix(bestScore) + bestScore.ToString();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void GenerateBrickLines()
    {
        float brickSize = brickPrefab.GetComponent<BoxCollider2D>().size.x;
        for(int i = 0; i < linesAmount; i++)
        {
            GameObject bricksLine = new GameObject("BricksLine" + i);
            Vector2 linePos = new Vector2(-27.5f + bricksMargin + brickSize/2 + 0.3f, 15 + bricksMargin * i * 5);
            bricksLine.transform.position = linePos;

            for(int j = 0; j < bricksAmount; j++)
            {
                GameObject brick = Instantiate<GameObject>(brickPrefab);
                brick.transform.parent = bricksLine.transform;
                Vector2 brickPos = new Vector2((brickSize + bricksMargin) * j , 0);
                brick.transform.localPosition = brickPos;
                brick.GetComponent<Brick>().type = (BrickType)(i / 2);
            }
        }
    }

    private void CreateBall()
    {
        if (curBall != null)
            Destroy(curBall);
        curBall = Instantiate<GameObject>(ballPrefab);
        curBall.transform.position = new Vector3(0, 0, 0);
    }

    private string GetPrefix(int value)
    {
        string prefix;
        if (value < 10)
            prefix = "00";
        else if (value < 100)
            prefix = "0";
        else
            prefix = "";
        return prefix;
    }
}
