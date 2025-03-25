using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    private LevelBuilder lb;
    private SpriteRenderer[] sps;
    private Color normal;
    private Color selected;
    private Color cantPlace;
    private Color notMovable;
    bool isValid;
    // Start is called before the first frame update
    void Start()
    {
        lb = GameObject.Find("LevelBuilder").GetComponent<LevelBuilder>();
        sps = gameObject.GetComponentsInChildren<SpriteRenderer>();
        isValid = true;
        normal = Color.white;
        notMovable = new Color(0.75f, 0.6f, 0.6f);
        selected = new Color(0.75f, 0.75f, 0.75f);
        cantPlace = new Color(1, 0.5f, 0.5f);   
        if(gameObject.layer != 6)
        {
            sps[0].color = notMovable;
            normal = notMovable;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lb != null && lb.GetCanMove())
        {
            if (Input.GetMouseButtonDown(0))
            {
                MouseDown();
            }
            if (Input.GetMouseButton(0))
            {
                MouseDrag();
            }
            if (Input.GetMouseButtonUp(0))
            {
                MouseUp();
            }
        }
    }
    private Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    private void MouseDown()
    {
        Vector3 mousePos = GetMouseWorldPosition();
        Collider2D col = Physics2D.OverlapPoint(new Vector2(mousePos.x, mousePos.y));
        if (col != null && col.gameObject.layer == 6 && col.gameObject.transform.parent == transform)
        {
            for(int i = 0; i < sps.Length; i++)
            {
                sps[i].color = selected;
            }
        }
    }

    private void MouseDrag()
    {
        Collider2D[] children = gameObject.GetComponentsInChildren<Collider2D>();
        bool safe = true;
        for (int k = 0; k < children.Length; k++)
        {
            Collider2D[] hits = Physics2D.OverlapPointAll(children[k].transform.position);
            for (int l = 0; l < hits.Length; l++)
            {
                if (hits[l].transform.parent != this.transform)
                {
                    safe = false;
                    break;
                }
            }
        }
        if (safe && !isValid)
        {
            for (int i = 0; i < sps.Length; i++)
            {
                sps[i].color = selected;
            }
            isValid = true;
        }
        else if(!safe && isValid)
        {
            for (int i = 0; i < sps.Length; i++)
            {
                sps[i].color = cantPlace;
            }
            isValid = false;
        }
    }
    private void MouseUp()
    {
        for (int i = 0; i < sps.Length; i++)
        {
            sps[i].color = normal;
        }
    }
}
