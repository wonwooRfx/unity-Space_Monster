using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsGizmo : MonoBehaviour
{
    public Color color = Color.yellow;
    public float radius = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
