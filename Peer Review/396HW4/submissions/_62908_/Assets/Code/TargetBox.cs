using UnityEngine;

public class TargetBox : MonoBehaviour
{
    /// <summary>
    /// Targets that move past this point score automatically.
    /// </summary>
    public static float OffScreen;

    internal void Start() {
        OffScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width-100, 0, 0)).x;
    }

    internal void Update()
    {
        if (transform.position.x > OffScreen)
            Scored();
    }

    private void Scored()
    {

        TargetBox t = this.gameObject.GetComponent<TargetBox>();
        float mass = t.GetComponent<Rigidbody2D>().mass;
        SpriteRenderer s = t.gameObject.GetComponent<SpriteRenderer>();
        if (s.color != Color.green)
        {
            ScoreKeeper.AddToScore(mass);
            s.color = Color.green;
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            Scored();
        }
    }
}
