using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUIManager : MonoBehaviour
{
    public Card _card;
    [SerializeField] TMP_Text cardTitle;
    [SerializeField] TMP_Text cardDescription;
    [SerializeField] Image cardIcon;
    [SerializeField] TMP_Text cardCost;
    [SerializeField] TMP_Text cardType;

    BattleMangager battleMangager;

    private void Awake()
    {
        battleMangager = FindObjectOfType<BattleMangager>();
    }
    void Start()
    {
        LoadCard(_card); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadCard(Card card)
    {
        _card = card;
        SetCardCost(card.GetCardValue());
        SetCardDescription(card.GetCardDecription());
        SetCardIcon(card.cardIcon);
        SetCardTitle(card.cardTitile);
        SetCardType(card.cardType.ToString());
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
    public void SelectedCard()
    {
        battleMangager.selectedCard = this;
        Debug.Log(this.cardDescription.text);
    }
    public void DeSelectedCard()
    {
        battleMangager.selectedCard = null;
        Debug.Log(this.cardDescription.text + " Null");
    }
    // Must make a custom Hand Holder to make this work
    public void HoverCard()
    {

    }
    public void DropCard()
    {

    }
    
}
