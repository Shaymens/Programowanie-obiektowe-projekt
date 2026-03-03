using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float damage;
    [SerializeField] private float health;
    [SerializeField] private int experienceToGive;
    [SerializeField] private GameObject destroyEffect;

    private Vector2 direction;

    void FixedUpdate()
    {
        if (PlayerMovement.Instance.gameObject.activeSelf)
        {
            var player = PlayerMovement.Instance;
            if (player == null) return;

            direction = (player.transform.position - transform.position).normalized;

            spriteRenderer.flipX = direction.x > 0;

            rb.linearVelocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);

        }
        else
        {
            rb.linearVelocity = Vector2.zero;

        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement.Instance.TakeDamage(damage);
        }
    }

    public void TakeDamage(float damage)
    {
            health -= damage;
                DamageNumberController.Instance.CreateNumber(1, transform.position);
        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(destroyEffect, transform.position, transform.rotation);
            PlayerMovement.Instance.GetExperience(experienceToGive);
        }
        
    }

}
