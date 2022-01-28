using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayHealth : MonoBehaviour
{
    Player player;
    TextMeshProUGUI scoreHealth;
    // Start is called before the first frame update
    void Start()
    {
        scoreHealth = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();//neka mi taj objekt bude skripta player odredena
    }

    // Update is called once per frame
    void Update()
    {
        scoreHealth.text = player.GetHealth().ToString();
    }
    
}
