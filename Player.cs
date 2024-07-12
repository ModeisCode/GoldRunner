using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int gold;
    public TextMeshProUGUI goldText;
    public bool isSetGold = false;
    private string goldTextContent = "______________________";

    void Start()
    {
        gold = 0;
        UpdateGoldText();
    }

    void Update()
    {
        if (isSetGold)
        {
            gold += 10;
            UpdateGoldText();
            isSetGold = false;
        }
    }

    private void UpdateGoldText()
    {
        goldTextContent = "Altın: " + gold.ToString();
        goldText.SetText(goldTextContent);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("gold"))
        {
            Destroy(collision.gameObject);
            isSetGold = true;
        }
    }
}