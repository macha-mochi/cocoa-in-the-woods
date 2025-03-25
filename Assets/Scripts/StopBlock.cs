using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StopBlock : MonoBehaviour
{
    GameObject dog;
    CharacterMovement cm;
     [SerializeField] bool isLevelComplete = false; //idk do we need this
    bool activated = false;
    LevelBuilder lb;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        lb = GameObject.FindObjectOfType<LevelBuilder>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!activated && collision.gameObject.tag == "Player")
        {
            activated = true;
            dog = collision.gameObject;
            cm = dog.GetComponent<CharacterMovement>();
            dog.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            cm.setInTrigger(true);
            if (isLevelComplete)
            {
                anim.SetTrigger("activate");
                collision.gameObject.SetActive(false);
                transform.position -= new Vector3(0.6666667f, 0, 0);
                cm.getCanvas().GetComponent<UserInterface>().setActiveStar(false);
                cm.getCanvas().GetComponent<UserInterface>().setActiveStar2(true);
                StartCoroutine(LoadNewLevelRoutine());
            }
            else
            {
                lb.SetCanMove(true);
                StartCoroutine(StartWaitTime());
            }
            Debug.Log("coroutine started");
        }
    }
    IEnumerator LoadNewLevelRoutine(){
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    IEnumerator StartWaitTime()
    {
        yield return new WaitForSeconds(10f);
        dog.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        dog.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        cm.setInTrigger(false);
        lb.SetCanMove(false);
    }
}
