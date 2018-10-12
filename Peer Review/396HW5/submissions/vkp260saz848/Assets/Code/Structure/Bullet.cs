using UnityEngine;

namespace Assets.Code.Structure
{
    public class Bullet : MonoBehaviour
    {
        public const float Lifetime = 7.5f; // bullets last this long
        private float _deathtime;
        public string _firedBy;

        public void Initialize (Vector2 velocity, float deathtime, string firedBy) {
            GetComponent<Rigidbody2D>().velocity = velocity;
            _deathtime = Mathf.Infinity;
            _firedBy = firedBy;

        }

        internal void Update () {
            if (Time.time > _deathtime) { Die(); }
        }


        internal void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<Bullet>() == null)
            {
                Die(); // we die no matter what :(
            }
            if (_firedBy == "Player1")
            {
                if (other.gameObject.name == _firedBy)
                {
                    Game.Score.AddScore(-1);
                }
                else if (other.gameObject.name == "Player2")
                {
                    Game.Score.AddScore(2);
                }
            }
            else
            {
                if(other.gameObject.name == _firedBy)
                {
                    Game.Score2.AddScore(-1);
                }
                else if (other.gameObject.name == "Player1")
                {
                    Game.Score2.AddScore(2);
                }
            }
        }

        private void Die () {
            Destroy(gameObject);
        }
    }
}
