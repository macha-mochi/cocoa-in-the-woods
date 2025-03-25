using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimations : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rg2d;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rg2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isMoving = !rg2d.velocity.x.Equals(0);
        anim.SetBool("isMoving", isMoving);
    }
}
