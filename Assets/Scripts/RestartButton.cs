using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{

    PlayerController playerController;
   

    public void RestartScene()
    {
        
        SceneManager.LoadScene("Level01");
        

    }

   


}
