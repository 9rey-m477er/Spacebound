using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldMenu : MonoBehaviour
{
    public GameObject menuObject;
    // Start is called before the first frame update
    void Start()
    {
        menuObject.SetActive(false);
    }
    public void handleMenu()
    {
        if (menuObject.activeSelf == true)
        {
            menuObject.SetActive(false);

        }
        else
        {
            menuObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (menuObject.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
            {
                handleMenu();
            }
        }
        else if(menuObject.activeSelf == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                handleMenu();
            }
        }
    }
}
