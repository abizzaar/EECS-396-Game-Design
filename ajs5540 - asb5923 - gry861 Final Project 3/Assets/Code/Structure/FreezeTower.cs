using System.Collections;
using System.Collections.Generic;
using Assets.Code.Structure;
using UnityEngine;


namespace Assets.Code.Structure
{
    public class FreezeTower : MonoBehaviour
    {
        //No radius.  It must slow in the box collider that is defined as any adjacent GridSquare.
        //public float dot; //Small amount damage over time (DOT).  Additive
        //public float percentSlow; //Suggested = 1/2.  Multiplicative

        //Adds components via GameObject.AddComponent<Type>();
        //Removes components via Object.Destroy
        //DOT  component: DOT
        //GameObject.AddComponent<Dot>();
        //SLOW component: SLOW
        //GameObject.AddComponent<Slow>();

        //Detect any enemies inside using Physics.OverlapBox
        //public static Collider[] OverlapBox(Vector3 center, Vector3 halfExtents, Quaternion orientation = Quaternion.identity, int layerMask = AllLayers, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal); 
        //center = gameObject.transform
        //halfExtentes = 1.5
        //orientation = whatever;
        //layerMask = only enemies;


    }
}