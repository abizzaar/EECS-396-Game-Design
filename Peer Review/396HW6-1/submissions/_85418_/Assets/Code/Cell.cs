using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private MeshRenderer   _meshRenderer;
    private Color          _defaultColor;
    //I'm not attached to a red highlight. Feel free to change.
    private readonly Color _hoverColor = Color.red;

    // Use this for initialization
    internal void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _defaultColor = _meshRenderer.material.color;
    }

    // Update is called once per frame
    internal void Update()
    {
    }

    internal void OnEnable()
    {
        MouseManager.OnHover += HighlightOnHover;
    }

    internal void OnDisable()
    {
        MouseManager.OnHover -= HighlightOnHover;
    }

    public void HighlightOnHover()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        //Highlight only when mouse is on the top face of the cube.
        if (GetComponent<Collider>().Raycast(ray, out hitInfo, Mathf.Infinity) &&
             hitInfo.normal == Vector3.up)
            { _meshRenderer.material.color = _hoverColor; }
        else
        {
            if (_meshRenderer) { _meshRenderer.material.color = _defaultColor; }
        }
    }
}
