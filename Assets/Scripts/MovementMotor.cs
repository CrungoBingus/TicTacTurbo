using UnityEngine;

public class MovementMotor : MonoBehaviour
{
    [SerializeField] float _movementSpeed = 5;
    [SerializeField] float screenEdgeThickness = 2;

    private void Update()
    {
        Vector3 pos = transform.localPosition;

        if (Input.mousePosition.x >= Screen.width - screenEdgeThickness)
            pos.x += _movementSpeed * Time.deltaTime;
        if (Input.mousePosition.x <= screenEdgeThickness)
            pos.x -= _movementSpeed * Time.deltaTime;

        if (Input.mousePosition.y >= Screen.height - screenEdgeThickness)
            pos.z += _movementSpeed * Time.deltaTime;
        if (Input.mousePosition.y <= screenEdgeThickness)
            pos.z -= _movementSpeed * Time.deltaTime;

        transform.localPosition = pos;
    }
}