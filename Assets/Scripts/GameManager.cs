using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TileBoard tileBoard;
    public CanvasGroup gameOverCanvas;

    private void Start()
    {
        NewGame();
    }
    public void NewGame()
    {
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
}
