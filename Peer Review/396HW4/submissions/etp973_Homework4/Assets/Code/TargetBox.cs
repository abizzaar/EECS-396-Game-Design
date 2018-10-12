using UnityEngine;

public class TargetBox : MonoBehaviour
{
    /// <summary>
    /// Targets that move past this point score automatically.
    /// </summary>
    public static float OffScreen;

    internal bool firsthit = false;

    internal void Start() {
        OffScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width-100, 0, 0)).x;
    }

    internal void Update()
    {
        if (transform.position.x > OffScreen)
        {
            if (!firsthit) {Scored();}
            firsthit = true;
        }
    }

    private void Scored()
    {
        
		SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
        Rigidbody2D r = this.GetComponent<Rigidbody2D>();
        ScoreKeeper.AddToScore(r.mass);
        sr.color = Color.green;
    }

    internal void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject == GameObject.FindGameObjectWithTag("Ground")){
			if (!firsthit) { Scored(); }
			firsthit = true;
        }
    }

}
