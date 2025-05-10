using UnityEngine;
using UnityEngine.Tilemaps;

public class GridSystem : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject tilePrefab;

    private void Awake()
    {
        GenerateGrid();
    }

    private void Update()
    {

    }

    void GenerateGrid()
    {
        var offset = this.transform.localScale.x * 10;
        Debug.Log(this.transform.localScale.x);

        for (int x = -width/2; x < width/2; x++)
        {
            for(int y = -height/2; y < height/2; y++)
            {
                var spawnedTile = Instantiate(tilePrefab, new Vector3(x*offset, 0, y*offset), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
            }
        }
    }
}
