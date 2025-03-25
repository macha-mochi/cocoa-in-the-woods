using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingBlock : MonoBehaviour
{
    private Transform transform;
    private Vector3 startPos;
    private void Awake()
    {
        transform = GetComponent<Transform>();
        startPos = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            transform.position = startPos;
        }
    }
}
