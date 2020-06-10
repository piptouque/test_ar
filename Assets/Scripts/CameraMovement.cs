using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    /*
     * should only be added as a component if the platform is mobile.
     */
    [SerializeField]
    private float movementSpeed = 3.0f;

    [SerializeField] private float rotationSpeed = 0.3f;

    /* relative to display width and height */
    private Vector2 _mousePositionLast = new Vector2(0.5f, 0.5f);


    // Update is called once per frame
    void Update()
    {
        Rotate();
        Move();
    }

    private void Rotate()
    {
        if (Input.GetMouseButton(2))
        {
            Vector2 mousePositionLastDisplay = new Vector2(
                _mousePositionLast.x * Display.main.renderingWidth,
                _mousePositionLast.y * Display.main.renderingHeight
                );
            /*
             * rotating only if middle button is pressed
             * left and right buttons are reserved
             */
            mousePositionLastDisplay = (Vector2) Input.mousePosition - mousePositionLastDisplay;
            Vector2 currentViewAngle = transform.eulerAngles;
            transform.eulerAngles = new Vector2(
                currentViewAngle.x + - mousePositionLastDisplay.y * rotationSpeed, 
                currentViewAngle.y + mousePositionLastDisplay.x * rotationSpeed
            );
        }
        _mousePositionLast = new Vector2(
            Input.mousePosition.x / Display.main.renderingWidth,
            Input.mousePosition.y / Display.main.renderingHeight
        );
    }

    private void Move()
    {
        var dir = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            dir += new Vector3(0.0f, 0.0f, 1.0f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            dir += new Vector3(0.0f, 0.0f, -1.0f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            dir += new Vector3(-1.0f, 0.0f, 0.0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            dir += new Vector3(1.0f, 0.0f, 0.0f);
        }
        dir = movementSpeed * Time.deltaTime * dir;
        /* actual mmovement */
        transform.Translate(dir); 
    }
}
