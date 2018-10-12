using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTest : MonoBehaviour {

    // Based on Homework 2's Player class, mostly.

    public int playerNum; // The player number; should be 1 or 2.
    // We use this to determine the correct input axes.

    public float moveThreshold = 0.02f; // Tolerance to register movement
    public float turnThreshold = 0.02f; // Tolerance to register torque
    // These should be 0.02f by default, but we make them public so they can be exposed.

    public float moveScale = 1f; // Multiplier for movement speed.  Default: 1.0
    public float turnScale = 0.05f; // Multiplier for turn speed.  Default: 0.05

    public float bulletSpeed = 2f; // Multiplier for bullet speed.
    public float bulletOffset = 0.5f; // How far off the player the bullet should spawn.  Default: 0.5 (for now)

    public float cooldownRate = 0.5f; // How fast the player can fire, in seconds.  Default: 0.5

    private Rigidbody2D _rb;
    private Text _scoreText;

    private string _moveAxis;
    private string _turnAxis;
    private string _fireAxis;

    private float _lastFire;

    private int _score;

    private Object _bullet;

    void Start() {
        _rb = GetComponent<Rigidbody2D>();

        _moveAxis = Platform.GetMoveAxis(playerNum);
        _turnAxis = Platform.GetTurnAxis(playerNum);
        _fireAxis = Platform.GetFireAxis(playerNum);

        _scoreText = GameObject.Find("Score" + playerNum.ToString()).GetComponent<Text>();
        _scoreText.color = GetComponent<SpriteRenderer>().color;
        _score = 0;

        _bullet = Resources.Load("Prefab/Bullet");
    }

    void Update() {
        Move(Input.GetAxis(_moveAxis));
        Turn(Input.GetAxis(_turnAxis));
        if (Input.GetAxis(_fireAxis) == 1) {
            AttemptToFire();
        }
    }

    private void Move(float amount) {
        if (Mathf.Abs(amount) >= moveThreshold) {
            _rb.AddRelativeForce(Vector2.up * amount * moveScale);
        }
    }

    private void Turn(float amount) {
        if (Mathf.Abs(amount) >= turnThreshold) {
            _rb.AddTorque(-1.0f * amount * turnScale);
        }
    }

    private void AttemptToFire() {
        if (Time.time >= _lastFire + cooldownRate) {
            _lastFire = Time.time;
            FireBullet();
        }
    }

    private void FireBullet() {
        // Instantiate bullet prefab w/ right owner, color, velocity, position
        GameObject bulletObject = (GameObject)UnityEngine.Object.Instantiate(_bullet, transform.position + transform.up * bulletOffset, transform.rotation);
        Vector2 velocity = transform.up * bulletSpeed;
        bulletObject.GetComponent<Bullet>().Initialize(velocity, gameObject, GetComponent<SpriteRenderer>().color);
    }

    public void UpdateScore(int change) {
        _score += change;
        _scoreText.text = _score.ToString();
    }
}
