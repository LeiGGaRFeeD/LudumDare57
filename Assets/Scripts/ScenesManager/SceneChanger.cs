using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void GoToGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void GoToUpgrade()
    {
        SceneManager.LoadScene("Upgrade");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
