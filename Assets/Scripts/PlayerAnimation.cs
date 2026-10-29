using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private float airVelocityThreshold = 0.1f;

    private Animator animator;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private string currentState = "";

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float vx = rb.linearVelocity.x;
        float vy = rb.linearVelocity.y;

        if (vx > 0.1f) sprite.flipX = false;
        else if (vx < -0.1f) sprite.flipX = true;

        if (Mathf.Abs(vy) > airVelocityThreshold) Change("Jump");
        else if (Mathf.Abs(vx) > 0.1f) Change("Run");
        else Change("Idle");
    }

    private void Change(string state)
    {
        if (state == currentState) return;
        animator.Play(state);
        currentState = state;
    }
}