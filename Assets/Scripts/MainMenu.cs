using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Load_Scene(string name) 
    {
        Application.LoadLevel(name);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
