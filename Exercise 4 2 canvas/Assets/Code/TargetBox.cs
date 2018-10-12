using UnityEngine;

public class TargetBox : MonoBehaviour
{
    /// <summary>
    /// Targets that move past this point score automatically.
    /// </summary>
    public static float OffScreen;

    private bool scoreCounted = false;

    internal void Start()
    {
        OffScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - 100, 0, 0)).x;
    }

    internal void Update()
    {
        if (!scoreCounted && transform.position.x > OffScreen)
        {
            Scored();
        }
    }

    private void Scored()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.green;
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
        if (!scoreCounted)
        {
            ScoreKeeper.AddToScore(rigidBody.mass);
            scoreCounted = true;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!scoreCounted && other.gameObject.CompareTag("Ground")) {
            Scored();
            
        }
    }
}