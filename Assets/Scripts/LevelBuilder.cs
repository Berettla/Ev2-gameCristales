using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private Vector2Int origin = new Vector2Int(-4, -7);

    [Header("Tiles")]
    [SerializeField] private TileBase grassLeft;       // Ground 04
    [SerializeField] private TileBase grassTop;        // Ground 02
    [SerializeField] private TileBase grassRight;      // Ground 08
    [SerializeField] private TileBase dirt;            // Ground 06
    [SerializeField] private TileBase rockLeft;        // Ground 09
    [SerializeField] private TileBase rockRight;       // Ground 13
    [SerializeField] private TileBase platformLeft;    // Ground 10
    [SerializeField] private TileBase platformMiddle;  // Ground 11
    [SerializeField] private TileBase platformRight;   // Ground 12

    // Cada texto es una fila, de arriba hacia abajo.
    private static readonly string[] map =
    {
        ".............................[=]................", // plataforma a +3
        "........................[=].....................", // plataforma a +2
        "...................[=]..........................", // plataforma a +1
        "LGGGGGGGGR..LGGGR..................LGGGGGGGGGGGR", // suelo con pasto
        "lDDDDDDDDr..lDDDr..................lDDDDDDDDDDDr", // tierra con costados rocosos
        "lDDDDDDDDr..lDDDr..................lDDDDDDDDDDDr",
        "lDDDDDDDDr..lDDDr..................lDDDDDDDDDDDr"
    };

    [ContextMenu("Construir nivel")]
    public void Build()
    {
        Tilemap tilemap = GetComponent<Tilemap>();
        if (tilemap == null)
        {
            Debug.LogError("Pon este script en el objeto TilemapGround.");
            return;
        }

        tilemap.ClearAllTiles();
        int rows = map.Length;

        for (int i = 0; i < rows; i++)
        {
            string line = map[i];
            if (line.Length != map[0].Length)
                Debug.LogWarning("La fila " + i + " tiene un largo distinto al de la primera.");

            int y = origin.y + (rows - 1 - i);
            for (int x = 0; x < line.Length; x++)
            {
                TileBase tile = GetTile(line[x]);
                if (tile != null)
                    tilemap.SetTile(new Vector3Int(origin.x + x, y, 0), tile);
            }
        }

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(tilemap);
#endif
        Debug.Log("Nivel construido.");
    }

    private TileBase GetTile(char c)
    {
        switch (c)
        {
            case 'L': return grassLeft;
            case 'G': return grassTop;
            case 'R': return grassRight;
            case 'D': return dirt;
            case 'l': return rockLeft;
            case 'r': return rockRight;
            case '[': return platformLeft;
            case '=': return platformMiddle;
            case ']': return platformRight;
            default: return null;
        }
    }
}