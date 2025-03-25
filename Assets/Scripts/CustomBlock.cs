using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CustomBlock : MonoBehaviour
{

    [SerializeField] Sprite[] customSprites;
    [SerializeField] int blockType; //0 will be trampoline for now, we figure out rest later
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float maxVelocity;
    [SerializeField] float jumpBlockVel;
    private float colliderOff;
    private GameObject player;
    private CharacterMovement cm;
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private bool inFan = false;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = customSprites[blockType];
        anim = spriteRenderer.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inFan)
        {
            if (rb.velocity.magnitude <= maxVelocity) rb.AddForce(transform.up * 50);
            else rb.velocity = transform.up * maxVelocity;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            cm = player.GetComponent<CharacterMovement>();
            rb = player.GetComponent<Rigidbody2D>();
            if (blockType == 0)
            {
                anim.SetTrigger("activated");
                if (transform.up.normalized == new Vector3(-1, 0, 0) || transform.up.normalized == new Vector3(1, 0, 0))
                {
                    rb.velocity = new Vector2(jumpBlockVel * transform.up.x, rb.velocity.y);
                    cm.setInTrigger(true);
                }
                else rb.velocity = new Vector2(rb.velocity.x, jumpBlockVel * transform.up.y);
                
                //StartCoroutine(waitForNormalMotion(0.5f));
            }
            if (blockType == 1)
            {
                //cm.setInTrigger(true);
                if (transform.up.normalized == new Vector3(-1, 0, 0) || transform.up.normalized == new Vector3(1, 0, 0)) cm.setInTrigger(true);
                inFan = true;
            }
            if (blockType == 2)
            {
                cm.Jump();
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            if(blockType == 1) cm.setInTrigger(false);
            inFan = false;
        }
    }

    private IEnumerator waitForNormalMotion()
    {
        Debug.Log("Freeze");
        yield return new WaitForSeconds(1);
        cm.setInTrigger(false);
    }

}
