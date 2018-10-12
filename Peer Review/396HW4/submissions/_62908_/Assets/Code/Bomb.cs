using UnityEngine;

public class Bomb : MonoBehaviour {
    public float ThresholdForce = 2;
    public GameObject ExplosionPrefab;

    private void Destruct()
    {
        GameObject expl = this.gameObject;
        Destroy(expl);
    }

    private void Boom()
    {
        TargetBox expl = this.gameObject.GetComponent<TargetBox>();
        PointEffector2D pe2D = expl.GetComponent<PointEffector2D>();
        SpriteRenderer s = expl.GetComponent<SpriteRenderer>();
        pe2D.enabled = true;
        s.enabled = false;
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity, transform.parent);
        Invoke("Destruct", 0.1f);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        float vel = coll.relativeVelocity.magnitude;
        if (vel > ThresholdForce)
        {
            Boom();
        }
    }
}
