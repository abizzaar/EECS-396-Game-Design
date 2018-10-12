using UnityEngine;

public class Bomb : MonoBehaviour {
    public float ThresholdForce = 2;
    public GameObject ExplosionPrefab;

    private void Destruct()
    {
        Destroy(gameObject);
    }

    private void Boom()
    {
        PointEffector2D pointEffector2D = GetComponent<PointEffector2D>();
        pointEffector2D.enabled = true;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity,
            transform.parent);
        Invoke("Destruct", 0.1f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Projectile" && other.otherRigidbody.velocity.magnitude >= ThresholdForce)
        {
            Boom();
        }
    }
}