// Scripts/Player/MalletController.cs
using UnityEngine;

public class MalletController : MonoBehaviour
{
    [Header("Player Settings")]
    public int playerID = 1;
    public float moveSpeed = 10f;
    public float malletRadius = 0.5f;

    [Header("Table Bounds")]
    public float tableWidth = 8f;
    public float tableHeight = 10f;

    private Vector2 targetPosition;
    private Rigidbody2D rb;
    private Camera mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;

        // Set initial positions
        if (playerID == 1)
        {
            transform.position = new Vector2(0, -3f);
        }
        else
        {
            transform.position = new Vector2(0, 3f);
        }
    }

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        MoveMallet();
    }

    private void HandleInput()
    {
        if (playerID == 1)
        {
            // Player 1 controls (WASD or Arrow Keys)
            //float horizontal = Input.GetKey(KeyCode.A) ? -1f : Input.GetKey(KeyCode.D) ? 1f : 0f;
            //float vertical = Input.GetKey(KeyCode.S) ? -1f : Input.GetKey(KeyCode.W) ? 1f : 0f;
            //targetPosition = (Vector2)transform.position + new Vector2(horizontal, vertical) * moveSpeed * Time.deltaTime;
            Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            targetPosition = mouseWorldPos;
        }
        else
        {
            // Player 2 controls (Mouse or Touch)
            Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            targetPosition = mouseWorldPos;
        }

        // Clamp to table bounds
        targetPosition.x = Mathf.Clamp(targetPosition.x, -tableWidth / 2 + malletRadius, tableWidth / 2 - malletRadius);

        if (playerID == 1)
        {
            targetPosition.y = Mathf.Clamp(targetPosition.y, -tableHeight / 2 + malletRadius, -malletRadius);
        }
        else
        {
            targetPosition.y = Mathf.Clamp(targetPosition.y, malletRadius, tableHeight / 2 - malletRadius);
        }
    }

    private void MoveMallet()
    {
        rb.MovePosition(targetPosition);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Puck"))
        {
            // Add sound effect here later
            Debug.Log($"Player {playerID} hit the puck!");
        }
    }
}