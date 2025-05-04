using UnityEngine;

public class ScreenEdgeColliders : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        AddEdgeColliders();
    }

    void AddEdgeColliders()
    {
        Vector2 bottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector2 topRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.nearClipPlane));
        Vector2 topLeft = new Vector2(bottomLeft.x, topRight.y);
        Vector2 bottomRight = new Vector2(topRight.x, bottomLeft.y);

        float thickness = 0.2f;
        CreateEdgeCollider("LeftEdge", bottomLeft, topLeft, thickness);
        CreateEdgeCollider("RightEdge", bottomRight, topRight, thickness);
        CreateEdgeCollider("TopEdge", topLeft, topRight, thickness);
        CreateEdgeCollider("BottomEdge", bottomLeft, bottomRight, thickness);
    }

    void CreateEdgeCollider(string name, Vector2 startPoint, Vector2 endPoint, float thickness)
    {
        GameObject edge = new GameObject(name);
        edge.transform.parent = transform;

        BoxCollider2D collider = edge.AddComponent<BoxCollider2D>();

        Vector2 center = (startPoint + endPoint) / 2f;
        edge.transform.position = new Vector3(center.x, center.y, 0);

        float length = Vector2.Distance(startPoint, endPoint);
        bool isVertical = Mathf.Abs(startPoint.x - endPoint.x) < 0.01f;

        if (isVertical)
        {
            collider.size = new Vector2(thickness, length);
        }
        else
        {
            collider.size = new Vector2(length, thickness);
        }
    }

    void Update()
    {
        if (Screen.width != mainCamera.pixelWidth || Screen.height != mainCamera.pixelHeight)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            AddEdgeColliders();
        }
    }
}