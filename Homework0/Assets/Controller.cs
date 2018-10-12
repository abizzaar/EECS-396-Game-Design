using System;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    private bool _moving = false; // are we moving?
    private Text _scoretext; // the text that displays the score
    private int _score; // our score


    // A Unity magic function. Called when the game starts.
    private void Start () {
        _scoretext = GameObject.Find("Score Text").GetComponent<Text>();
    }

    // Another Unity magic function. Update is called once per frame.
    private void Update () {
        if (ShouldStartMoving()) {
            StartMoving();
        }
        MaybeStopMoving();
    }


    private bool ShouldStartMoving ()
    {
        return Input.GetKey("space");
    }


    //
    // You don't have to worry about what this section does.
    // Feel free to read through it and consult the manual for parts that seem interesting.

    private void OnTriggerEnter (Collider other) {
        if (other.gameObject.GetComponent<Cube>() != null) { // check to see if the thing we touched has a "Cube" component
            IncrementScore();
        }
    }

    private void IncrementScore () {
        _score++;
        _scoretext.text = String.Format("Points: {0}", _score);
    }

    private void StartMoving () {
        if (_moving) { return; }

        _moving = true;

        if (transform.position.x < 0f) {
            GetComponent<Rigidbody>().velocity = Vector3.right * 5f; // go right
        } else {
            GetComponent<Rigidbody>().velocity = Vector3.left * 5f; // go left
        }
    }

    private const float THRESHOLD = 5f;
    private void MaybeStopMoving () {
        if (!_moving) { return; } // we're not moving, so we don't need to stop

        Rigidbody rb = GetComponent<Rigidbody>();
        bool stop =
            (transform.position.x < -THRESHOLD && rb.velocity.x < 0f) // are we far to the left and moving left?
            || (transform.position.x > THRESHOLD && rb.velocity.x > 0f); // are we far to the right and moving right?

        if (!stop) { return; } // if we don't need to stop, just bail out

        _moving = false;
        rb.velocity = Vector3.zero; // stop moving
    }
}
