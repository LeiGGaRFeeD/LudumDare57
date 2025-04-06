using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money")+PlayerPrefs.GetInt("moneySession"));
        SceneManager.LoadScene("Upgrade");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
