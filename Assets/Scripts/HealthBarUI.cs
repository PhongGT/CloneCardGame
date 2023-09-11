using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarUI : MonoBehaviour
{
    public Slider healthBar;
    public Slider poisionBar;
    public Image blockBG;
    public Image blockIcon;
    public TMP_Text blockNumber;
    public TMP_Text healthText;
    public void DisplayBlock(int blockAmount)
    {
        if(blockAmount > 0)
        {
            blockBG.color = Color.blue;
            blockIcon.enabled = true;

            blockNumber.text = blockAmount.ToString();
            blockNumber.enabled = true;
        
        }
        else
        {
            blockBG.color = Color.white;
            blockIcon.enabled = false;
            blockNumber.enabled = false; 
        }
    }
    public void DisplayHealth(int healthAmount)
    {
        healthText.text = $"{healthAmount}/{healthBar.maxValue}";
        healthBar.value = healthAmount;
    }
    public void DisplayPoision(int poisionAmount, int healthAmount)
    {
        poisionBar.maxValue = healthAmount;
        poisionBar.value = Mathf.Clamp(poisionAmount, 0, poisionBar.maxValue);
    }




}
