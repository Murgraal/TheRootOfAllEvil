using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Quitter : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void GoToMenu()
    {
        Main.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            Main.LoadScene("MainMenu");
    }
}
