using UnityEngine;
using UnityEngine.Tilemaps;

public class GridSystem : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject tilePrefab;
    public int offsetAmount;

    private void Awake()
    {
        GenerateGrid();
    }

    private void Update()
    {

    }

    void GenerateGrid()
    {
        float offset = this.transform.localScale.x * offsetAmount;
        Debug.Log(this.transform.localScale.x);

        for (int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                var spawnedTile = Instantiate(tilePrefab, new Vector3(x * offset, 0, y * offset), Quaternion.identity, gameObject.transform);
                spawnedTile.name = $"Tile {x} {y}";
            }
        }
    }
}
