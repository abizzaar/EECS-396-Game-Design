using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    //For controlling hover events over objects.
    public delegate void HoverAction();
    public static event HoverAction OnHover;
    
    internal void FixedUpdate()
    {
        if (OnHover != null)
            OnHover();
    }
}
