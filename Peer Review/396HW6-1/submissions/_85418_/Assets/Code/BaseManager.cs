using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public class BaseManager : MonoBehaviour
    {
        private GameObject bottomRight;
        public GameObject myBase;
        public GameObject healthBar;
        
        
        internal void Start()
        {
            buildBase();
        }

        public void buildBase()
        {
            myBase = GameObject.CreatePrimitive(PrimitiveType.Cube);
            myBase.transform.SetParent(GameObject.Find("BaseCell").transform);
            myBase.AddComponent<Base>();

            myBase.transform.localScale = new Vector3(myBase.transform.localScale.x/2, 3f, myBase.transform.localScale.z/2);
            Vector3 scale       = transform.localScale;
            Vector3 alignAnchor = new Vector3(scale.x * 0.5f, scale.y * -0.5f, scale.z * 0.5f);
            Vector3 pos         = transform.position + alignAnchor;
            myBase.transform.position = new Vector3(pos.x + (4 * scale.x), 1.5f, pos.z + (0 * scale.z));
            

        }
        
        
    }
}