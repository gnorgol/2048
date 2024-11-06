using UnityEngine;

public class TileCell : MonoBehaviour
{
    public Vector2Int position { get; set; }
    public Tile tile { get; set; }

    public bool empty { get { return tile == null; } }

}
