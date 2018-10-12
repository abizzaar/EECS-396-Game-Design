using UnityEngine;

public class Bomb : MonoBehaviour {
    public float ThresholdForce = 2;
    public GameObject ExplosionPrefab;

    private void Destruct(){
        Destroy(gameObject);
    }

    private void Boom(){
        PointEffector2D pe = gameObject.GetComponent<PointEffector2D>();
        pe.enabled = true;
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        sr.enabled = false;
		Instantiate(ExplosionPrefab, transform.position, Quaternion.identity, transform.parent);
        Invoke("Destruct", 0.1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.relativeVelocity.magnitude >= ThresholdForce){
            Boom();
        }
    }


}
