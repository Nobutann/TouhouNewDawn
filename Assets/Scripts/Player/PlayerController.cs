using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float normalSpeed = 5f;
    public float focusSpeed = 2f;

    [Header("Boundaries")]
    public float boundaryLeft = -8f;
    public float boundaryRight = 8f;
    public float boundaryBottom = -4.5f;
    public float boundaryTop = 4.5f;

    [Header("Focus Mode")]
    public KeyCode focusKey = KeyCode.LeftShift;
    public GameObject focusIndicator;

    private Rigidbody2D rb;
    private bool isFocusing = false;
    private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = normalSpeed;

        rb.gravityScale = 0;
        rb.freezeRotation = true;
    }

    void Update()
    {
        HandleInput();
        HandleFocusMode();
        ClampToBoundaries();
    }

    void HandleInput()
    {
        float horizontal = 0f;
        float vertical = 0f;

        if (Input.GetKey(KeyCode.LeftArrow)) horizontal = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) horizontal = 1f;
        if (Input.GetKey(KeyCode.UpArrow)) vertical = 1f;
        if (Input.GetKey(KeyCode.DownArrow)) vertical = -1f;

        Vector2 movement = new Vector2(horizontal, vertical);

        if (movement.magnitude > 1)
        {
            movement = movement.normalized;
        }

        movement *= currentSpeed;

        rb.linearVelocity = movement;
    }

    void HandleFocusMode()
    {
        if (Input.GetKey(focusKey))
        {
            if (!isFocusing)
            {
                isFocusing = true;
                currentSpeed = focusSpeed;

                if (focusIndicator != null)
                {
                    focusIndicator.SetActive(true);
                }
            }
        }
        else
        {
            if (isFocusing)
            {
                isFocusing = false;
                currentSpeed = normalSpeed;

                if (focusIndicator != null)
                {
                    focusIndicator.SetActive(false);
                }
            }
        }
    }

    void ClampToBoundaries()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, boundaryLeft, boundaryRight);
        pos.y = Mathf.Clamp(pos.y, boundaryBottom, boundaryTop);

        transform.position = pos;
    }
}