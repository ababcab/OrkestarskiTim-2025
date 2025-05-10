using System.IO.IsolatedStorage;
using UnityEngine;

public class Placement : MonoBehaviour
{
    public Tile parentTile;

    private bool isMoving = false;

    void Start()
    {
        // neki efekat na cacije
    }

    void Update()
    {
        if (isMoving)
        {
            // get object under the mouse
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                // place tent on hit object
                // TODO: add tiles to a layer and then only check hits with the tile layer
                transform.position = hit.collider.transform.position;
            }
        }

        if (isMoving && Input.GetMouseButtonUp(2))
        {
            isMoving = false;
        }
    }

    private void OnMouseOver()
    {
        // remove sator
        if (Input.GetMouseButtonDown(1))
        {
            parentTile.zauzeto = false;
            Destroy(gameObject);
        }

        // pomjeri sator
        if (Input.GetMouseButtonDown(2))
        {
            isMoving = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Tile")
        {
            collision.gameObject.GetComponent<Tile>().zauzeto = true;
        }

        if (collision.gameObject.tag == "EventTile")
        {
            collision.gameObject.GetComponent<EventTile>().zauzeto = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Tile")
        {
            collision.gameObject.GetComponent<Tile>().zauzeto = false;
        }

        if (collision.gameObject.tag == "EventTile")
        {
            collision.gameObject.GetComponent<EventTile>().zauzeto = false;
        }
    }
}
