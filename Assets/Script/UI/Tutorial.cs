using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private int state;

    public GameObject[] tutorialImages;
    [SerializeField]
    private string gamelevelname;

    void Start()
    {
        state = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextState()
    {
        state++;
        if (state == tutorialImages.Length)
        {
            if (gamelevelname == "") return;
            SceneManager.LoadScene(gamelevelname);
        }
        tutorialImages[state - 1].SetActive(false);
        tutorialImages[state].SetActive(true);
    }
}
