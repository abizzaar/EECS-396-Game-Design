using UnityEngine;

namespace Assets.Other_Code
{
    public class ForceScreenWidth : MonoBehaviour
    {
        public float WorldWidth = 100f;

        internal void Start () {
            if (Application.isEditor && !Application.isPlaying)
                // Don't screw around with things in the middle of the editor!
                return;

            var aspectRatio = ((float)Screen.width) / Screen.height;
            var thisObjectsCamera = GetComponent<Camera>();
            var currentWorldWidth = thisObjectsCamera.orthographicSize * aspectRatio;
            // Want the worldWidth to be 55.
            var correction = 0.5f * WorldWidth / currentWorldWidth;
            thisObjectsCamera.orthographicSize *= correction;
        }
    }
}
