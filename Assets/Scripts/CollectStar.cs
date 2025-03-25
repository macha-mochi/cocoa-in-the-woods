using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectStar : MonoBehaviour
{
    [SerializeField] ParticleSystem starExplode;
    private float durSeconds;
    private bool collected = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !collected)
        {
            collected = true;
            starExplode.Play();
            Canvas canvas = collision.gameObject.GetComponent<CharacterMovement>().getCanvas();
            canvas.GetComponent<UserInterface>().incStar(1);
            //add this to a count of stars earned in game manager or something like that
            Destroy(this.gameObject, starExplode.main.duration);
        }
    } 
}
