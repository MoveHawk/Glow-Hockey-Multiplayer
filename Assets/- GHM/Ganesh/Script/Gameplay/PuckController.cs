// Scripts/Gameplay/PuckController.cs
using UnityEngine;

public class PuckController : MonoBehaviour
{
    [Header("Puck Settings")]
    public float maxSpeed = 15f;
    public float bounceForceMultiplier = 1.2f;

    private Rigidbody2D rb;
    private Vector3 startPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        // Limit maximum speed
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Goal"))
        {
            HandleGoal(collision.gameObject);
        }
    }

    private void HandleGoal(GameObject goal)
    {
        // Determine which player scored
        if (goal.name == "GoalTop")
        {
            // Player 1 scored (puck entered top goal)
            GameManager.Instance.PlayerScored(1);
        }
        else if (goal.name == "GoalBottom")
        {
            // Player 2 scored (puck entered bottom goal)
            GameManager.Instance.PlayerScored(2);
        }

        ResetPuck();
    }

    public void ResetPuck()
    {
        transform.position = startPosition;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Mallet"))
        {
            // Enhance the bounce when hit by mallet
            Vector2 force = collision.relativeVelocity * bounceForceMultiplier;
            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }
}