using UnityEngine;

public class TargetBox : MonoBehaviour
{
	/// <summary>
	/// Targets that move past this point score automatically.
	/// </summary>
	public static float OffScreen;
    private bool first = false;
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
		// FILL ME IN
        gameObject.GetComponent<SpriteRenderer>().color=Color.green;
        if (this.first == false)
        {
            ScoreKeeper.AddToScore(this.GetComponent<Rigidbody2D>().mass);
            first = true;
        }


	}
	private void OnCollisionEnter2D(Collision2D obj){
		if(obj.gameObject.tag == "Ground"){
			Scored();
		}
	}
}