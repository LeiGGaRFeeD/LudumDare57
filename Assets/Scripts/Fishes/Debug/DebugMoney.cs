using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugMoney : MonoBehaviour
{
    [SerializeField] private Text money_text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.SetInt("money",0);
        }
        money_text.text = PlayerPrefs.GetInt("money").ToString();
    }
}
