using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tile : MonoBehaviour
{
    public TileState state { get; private set; }
    public TileCell cell { get; private set; }
    public int number { get; private set; }
    public bool locked { get; set; }

    private Image background;
    private TextMeshProUGUI text;


    private void Awake()
    {
        background = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetState(TileState state,int number)
    {
        this.state = state;
        this.number = number;
        background.color = state.backgroundColor;
        text.text = number.ToString();
        text.color = state.textColor;
    }
    public void Spawn(TileCell cell)
    {
        if (this.cell != null)
        {
            this.cell.tile = null;
        }

        this.cell = cell;
        this.cell.tile = this;
        transform.position = cell.transform.position;
    }
    public void Merge(TileCell cell)
    {
        if (this.cell != null)
        {
            this.cell.tile = null;
        }

        this.cell = null;
        cell.tile.locked = true;
        StartCoroutine(AnimateMerge(cell.transform.position));
    }
    public void MoveTo(TileCell cell)
    {
        this.cell.tile = null;
        this.cell = cell;
        this.cell.tile = this;
        StartCoroutine(AnimateMove(cell.transform.position));
    }
    private IEnumerator AnimateMove(Vector3 targetPosition)
    {
        float duration = 0.1f;
        float elapsedTime = 0;
        Vector3 startPosition = transform.position;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }
    private IEnumerator AnimateMerge(Vector3 targetPosition)
    {
        float duration = 0.1f;
        float elapsedTime = 0;
        Vector3 startPosition = transform.position;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            transform.localScale = Vector3.one * (1 + elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        Destroy(gameObject);
    }
}
