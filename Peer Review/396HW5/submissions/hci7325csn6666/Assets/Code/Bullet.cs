using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour {
    public const float Lifetime = 4f; // bullet life time
    private GameObject parent;

    public void Initialize(Vector2 velocity, GameObject player, Color color) {
        GetComponent<Rigidbody2D>().velocity = velocity;
        parent = player;
        GetComponent<SpriteRenderer>().color = color;
        // Bullets should ignore other bullets.
        // There's probably a more efficient way to do this.
        Collider2D collider = GetComponent<Collider2D>();
        foreach (Bullet other in FindObjectsOfType(typeof(Bullet)) as Bullet[]) {
            if (other != this) {
                Physics2D.IgnoreCollision(collider, other.GetComponent<Collider2D>());
            }
        }
    }

    internal void Update() {
    }

    internal void OnCollisionEnter2D(Collision2D other) {
        PlayerTest otherPlayer = other.gameObject.GetComponent<PlayerTest>();
        if (otherPlayer != null) {
            if (otherPlayer.gameObject == parent) {
                parent.GetComponent<PlayerTest>().UpdateScore(-1);
            } else {
                parent.GetComponent<PlayerTest>().UpdateScore(2);
            }
        }
        Die();
    }

    private void Die() {
        Destroy(gameObject);
    }
}

