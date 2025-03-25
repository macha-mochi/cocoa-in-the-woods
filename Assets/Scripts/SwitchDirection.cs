using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDirection : MonoBehaviour
{
    CharacterMovement cm;

    private void Start()
    {
        cm = GetComponentInParent<CharacterMovement>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.layer == 3 || other.gameObject.layer == 6) && other.gameObject.tag!="Custom")
        {
            cm.setDir(cm.getDir() * -1);
        }
    }
}
