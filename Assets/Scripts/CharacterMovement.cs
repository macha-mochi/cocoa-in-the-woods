using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] BoxCollider2D collider2d;
    private float dir;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] LayerMask platformLayer;
    [SerializeField] LayerMask moveableLayer;
    [SerializeField] float edgeOffset;
    [SerializeField] Canvas canvas;
    private bool start;
    public bool inTrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        dir = 1;
        setStart(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            Move();
            RaycastHit2D[] isAtGround = Physics2D.CircleCastAll(transform.position, 0.5f, Vector2.down, 0.1f, platformLayer);
            RaycastHit2D[] isAtGround2 = Physics2D.CircleCastAll(transform.position, 0.5f, Vector2.down, 0.1f, moveableLayer);
            bool grounded = (isAtGround.Length != 0 && isAtGround[0].transform.tag != "Custom") || (isAtGround2.Length != 0 && isAtGround2[0].transform.tag != "Custom");
            if (grounded && onEdge())
            {
                Jump();
            }
            
        }
    }

    private bool onEdge()
    {
        bool isOnEdgeLeft = Physics2D.CircleCast(new Vector2(transform.position.x + edgeOffset, transform.position.y), 0.1f, Vector2.down, 1f, platformLayer);
        bool isOnEdgeLeft2 = Physics2D.CircleCast(new Vector2(transform.position.x + edgeOffset, transform.position.y), 0.1f, Vector2.down, 1f, moveableLayer);
        bool isOnEdgeRight = Physics2D.CircleCast(new Vector2(transform.position.x - edgeOffset, transform.position.y), 0.1f, Vector2.down, 1f, platformLayer);
        bool isOnEdgeRight2 = Physics2D.CircleCast(new Vector2(transform.position.x - edgeOffset, transform.position.y), 0.1f, Vector2.down, 1f, moveableLayer);
        
        return (dir == 1 && !(isOnEdgeLeft || isOnEdgeLeft2) && (isOnEdgeRight || isOnEdgeRight2)) || (dir == -1 && !(isOnEdgeRight || isOnEdgeRight2) && (isOnEdgeLeft || isOnEdgeLeft2));

    }

    public void Jump()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
    }

    public void Jump(float jumpVel)
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpVel);
    }

    public void setStart(bool start)
    {
        this.start = start;
        Time.timeScale = start ? 1 : 0;
    }
    void Move()
    {
        if(!inTrigger) rb2d.velocity = new Vector2(dir * speed, rb2d.velocity.y);
    }

    public void setDir(float dir)
    {
        this.dir = dir;
        if (dir > 0)
            gameObject.transform.localScale = new Vector2(1, 1);
        else if (dir < 0)
            gameObject.transform.localScale = new Vector2(-1, 1);
    }

    public float getDir()
    {
        return dir;
    }
    public void setInTrigger(bool b)
    {
        inTrigger = b;
    }

    public Canvas getCanvas()
    {
        return canvas;
    }

}
