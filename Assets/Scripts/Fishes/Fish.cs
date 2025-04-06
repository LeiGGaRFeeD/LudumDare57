using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public int Fish_Money_Add;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
      //  if (collision.gameObject.CompareTag("Player") == true)
        {
            PlayerPrefs.SetInt("moneySession", PlayerPrefs.GetInt("moneySession") + Fish_Money_Add);
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
