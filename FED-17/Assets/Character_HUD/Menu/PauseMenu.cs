using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    [SerializeField]
    Transform menu;
		
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(menu.gameObject.activeInHierarchy == false)
            {
                startMenu();
            }
            else
            {
                exitMenu();
            }
        }
	}

   private void startMenu()
    {
        menu.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void exitMenu()
    {
        menu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
