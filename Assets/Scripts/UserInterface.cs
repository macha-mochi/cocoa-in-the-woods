using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button resetButton;
    [SerializeField] private TextMeshProUGUI starText;
    [SerializeField] private GameObject starImage;
    [SerializeField] private GameObject starImage2;
    [SerializeField] private TextMeshProUGUI starText2;
    [SerializeField] private CharacterMovement[] cm;
    [SerializeField] private LevelBuilder lb;
    [SerializeField] ParticleSystem leaves;
    [SerializeField] int totalStarNum;
    private int numStars;
    private int scene;
    private void Start()
    {
        scene = SceneManager.GetActiveScene().buildIndex;
        numStars = 0;
    }

    public void Play()
    {
        for(int i = 0; i < cm.Length; i++)
        {
            cm[i].setStart(true);
            lb.SetCanMove(false);
        }
        leaves.gameObject.SetActive(true);
        
    }

    public void Reset()
    {
        SceneManager.LoadScene(scene);
    }


    public void incStar(int amt)
    {
        numStars += amt;
        starText.text = "X" + numStars;
        starText2.text = numStars + "/" + totalStarNum;
        if (numStars > 0 && !starImage.activeSelf)
        {
            starImage.SetActive(true);
        }

    }
    public void setActiveStar(bool active)
    {
        starImage.SetActive(active);
    }

    public void setActiveStar2(bool active)
    {
        starImage2.SetActive(active);
    }
}
