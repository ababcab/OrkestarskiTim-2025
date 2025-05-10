using UnityEngine;
using UnityEngine.Tilemaps;

public class GridSystem : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject tilePrefab;
    public int offsetAmount;
    public float posX;
    public float posY;

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

        for (int x = -width/2; x < width/2; x++)
        {
            for(int y = -height/2; y < height/2; y++)
            {
                var spawnedTile = Instantiate(tilePrefab, new Vector3(posX * offset, 0, posY * offset), Quaternion.identity, gameObject.transform);
                spawnedTile.name = $"Tile {x} {y}";
            }
        }
    }
}
