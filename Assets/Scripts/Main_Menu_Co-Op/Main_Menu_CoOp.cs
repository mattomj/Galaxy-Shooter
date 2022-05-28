using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Main_Menu_CoOp : MonoBehaviour
{
    public void LoadSinglePlayerGame()
    {
        Debug.Log("Single Player Game Loading...");
        SceneManager.LoadScene("Single_Player");
    }


    public void LoadCoOpMode()
    {
        Debug.Log("Co-Op Mode Loading...");
        SceneManager.LoadScene("Co-Op_Mode");
    }
}
