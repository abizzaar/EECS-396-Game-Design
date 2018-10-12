using UnityEngine;

public class Bomb : MonoBehaviour {
	public float ThresholdForce = 2;
	public GameObject ExplosionPrefab;
	private void Destruct(){
        Destroy(this.gameObject);
	}
	private void Boom(){

		PointEffector2D pointeffector2D = GetComponent<PointEffector2D>();
		SpriteRenderer spriterenderer = gameObject.GetComponent<SpriteRenderer>();
		pointeffector2D.enabled = true;
		spriterenderer.enabled = false;

		Instantiate(ExplosionPrefab, transform.position, Quaternion.identity, transform.parent);
        float a = 0.1f;
		Invoke("Destruct",a);

	}
    private void OnCollisionEnter2D(Collision2D col){
        if (col.relativeVelocity.x > ThresholdForce){
            Boom();
        }

    }

}