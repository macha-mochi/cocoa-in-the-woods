using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class SubEmitterController : MonoBehaviour
{
    [SerializeField] float angularVelocity;
    [SerializeField] float radius = 1;
    private float angle = 0;
    private Vector3 center;
    void Start()
    {
        center = this.transform.position - new Vector3(radius, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        angle -= angularVelocity;
        angle %= 2 * Mathf.PI;
        this.transform.position = center + (radius * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0));
    }
}
