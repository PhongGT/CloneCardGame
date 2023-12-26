using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadReward : MonoBehaviour
{
    // Start is called before the first frame update
    public Card _card;
    [SerializeField] TMP_Text cardTitle;
    [SerializeField] TMP_Text cardDescription;
    [SerializeField] Image cardIcon;
    [SerializeField] TMP_Text cardCost;
    [SerializeField] TMP_Text cardType; 
    public void LoadCard(Card card)
    {
        _card = card;
        SetCardCost(card.GetCardValue());
        SetCardDescription(card.GetCardDecription());
        SetCardIcon(card.cardIcon);
        SetCardTitle(card.cardTitile);
        SetCardType(card.cardType.ToString());
        this.gameObject.SetActive(true);
    }

    public void SetCardTitle(string title)
    {
        cardTitle.text = title;
    }
    public void SetItemPositon(Vector2 pos)
    {
        GetComponent <RectTransform> ().anchoredPosition += pos;        
    }
    public void SetCardDescription(string des) {
        cardDescription.text = des;
        
    }
    public void SetCardIcon(Sprite image) {
        cardIcon.sprite = image;

    }
    public void SetCardCost(int cost)
    {
        cardCost.text = cost.ToString();
    }
    public void SetCardType(string type)
    {
        cardType.text = type;
    }
}
