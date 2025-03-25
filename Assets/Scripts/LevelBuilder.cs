using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] float cellSize;
    [SerializeField] GameObject gridBox;
    int[,] grid;
    GameObject[,] tiles;
    bool objectSelected;
    Vector3 offset;
    Vector3 initPos;
    Transform curr;
    private RectTransform rectTransform;
    private bool canMove;

    void Start()
    {
        canMove = true;
        grid = new int[width, height];
        tiles = new GameObject[width, height];
        int r = 0;
        int c = 0;
        for(int i = (-width/2); i < (width/2); i++)
        {
            for(int j = (-height/2); j < (height/2); j++)
            {
                GameObject spawnedTile = Instantiate(gridBox);
                spawnedTile.transform.SetParent(transform);
                spawnedTile.transform.position = new Vector3(i * cellSize+cellSize/2, j * cellSize+cellSize/2);
                tiles[r, c] = spawnedTile;
                c++;
            }
            r++;
            c = 0;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseDown();
        }
        if (Input.GetMouseButton(0))
        {
            MouseDrag();
        }
        if (Input.GetMouseButtonUp(0) || (!canMove))
        {
            MouseUp();
        }
    }
    private Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void MouseDown()
    {
        
        Vector3 mousePos = GetMouseWorldPosition();
        Collider2D[] col = Physics2D.OverlapPointAll(new Vector2(mousePos.x, mousePos.y));
        for(int m = 0; m < col.Length; m++)
        {
            if (canMove && col != null && col[m].gameObject.layer == 6)
            {
                initPos = col[m].transform.parent.position;
                offset = col[m].transform.parent.position - mousePos;
                curr = col[m].transform.parent;
                objectSelected = true;
            }
        } 
    }

    private void MouseDrag()
    {
        if (objectSelected && canMove)
        {
            curr.position = GetMouseWorldPosition() + offset;
        }
        if (Input.GetKeyDown("r") && canMove)
        {
            curr.transform.eulerAngles += new Vector3(0, 0, 90);
        }
    }

    private void MouseUp()
    {
        objectSelected = false;
        if(curr == null)
        {
            return;
        }
        int i = (int)Mathf.Floor(curr.position.x / cellSize);
        int j = (int)Mathf.Floor(curr.position.y / cellSize);
        Collider2D[] children = curr.GetComponentsInChildren<Collider2D>();
        bool safe= true;
        for(int k = 0; k < children.Length; k++)
        {
            Collider2D[] hits = Physics2D.OverlapPointAll(children[k].transform.position);
            for(int l = 0; l < hits.Length; l++)
            {
                if (hits[l].transform.parent != curr && (hits[l].gameObject.layer == 6 || hits[l].transform.gameObject.layer == 3))
                {
                    safe = false;
                    break;
                }
            }
        }
        if (safe)
        {
            curr.position = new Vector3(i * cellSize+cellSize/2, j * cellSize+cellSize/2);
            grid[i + width / 2, j + height / 2] = 1;
        }
        else
        {
            curr.position = initPos;
        }
    }

    public void SetCanMove(bool boolInput)
    {
        canMove = boolInput;
        if (boolInput) MouseDown();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                tiles[i, j].SetActive(canMove);
            }
        }
    }
    public bool GetCanMove()
    {
        return canMove;
    }
}