using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Creditos : MonoBehaviour
{
    
    void Start()
    {
        StartCoroutine(CreditsScene());
        
    }

    IEnumerator CreditsScene()
    {
        yield return new WaitForSeconds(20);
        SceneManager.LoadScene(3);
    }
    
}
