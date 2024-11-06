using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TileBoard tileBoard;
    public CanvasGroup gameOverCanvas;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;

    private int score;


    private void Start()
    {
        NewGame();
    }
    public void NewGame()
    {
        SetScore(0);
        highscoreText.text = LoadHighscore().ToString();
        gameOverCanvas.alpha = 0;
        gameOverCanvas.interactable = false;
        tileBoard.ClearBoard();
        tileBoard.CreateTile();
        tileBoard.CreateTile();
        tileBoard.enabled = true;
    }
    public void GameOver()
    {
        tileBoard.enabled = false;
        gameOverCanvas.interactable = true;
        StartCoroutine(Fade(gameOverCanvas, 1f, 1f));

    }
    private IEnumerator Fade(CanvasGroup canvasGroup, float start, float delay)
    {
        yield return new WaitForSeconds(delay);
        float elapsedTime = 0f;
        float fadeTime = 0.5f;

        while (elapsedTime < fadeTime)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, start, elapsedTime / fadeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = start;
    }

    public void IncreaseScore(int value)
    {
        SetScore(score + value);
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();

        SaveHighscore();
    }
    private void SaveHighscore()
    {
        int highscore = LoadHighscore();
        if (score > highscore)
        {
            PlayerPrefs.SetInt("highscore", score);
        }
    }
    private int LoadHighscore()
    {
        return PlayerPrefs.GetInt("highscore", 0);
    }
}
