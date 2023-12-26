using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CardUIManager : MonoBehaviour
{
    public Card _card;
    [SerializeField] TMP_Text cardTitle;
    [SerializeField] TMP_Text cardDescription;
    [SerializeField] Image cardIcon;
    [SerializeField] TMP_Text cardCost;
    [SerializeField] TMP_Text cardType;

    BattleManager BattleManager;

    HorizontalLayoutGroup horizontalLayoutGroup;
    SortingGroup sortingGroup;

    Animator animator;

    private void Awake()
    {
        BattleManager = FindObjectOfType<BattleManager>();
        horizontalLayoutGroup = GetComponentInParent<HorizontalLayoutGroup>();
        sortingGroup = GetComponent<SortingGroup>();
        animator = GetComponent<Animator>();
    }


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
    public void SelectedCard()
    {
        BattleManager.selectedCard = this;
        Debug.Log(this.cardDescription.text);
        
        
    }
    public void DeSelectedCard()
    {
        BattleManager.selectedCard = null;
        Debug.Log(this.cardDescription.text + " Null");
        
    }
    // Must make a custom Hand Holder to make this work
    public void HoverCard()
    {
        if(BattleManager.selectedCard == null)
        {
            //hover animation
            AnimatonHoverOnCard();

        }
    }
    public void DropCard()
    {
        animator.SetBool("Hover", false);
    }
    public void HandleDrag()
    {
        //Nothing importance here
    }
    public void HandleEndDragCard()
    {
        Debug.Log("In Handle End Drag!!");
        if(BattleManager.energy < _card.GetCardValue())
        {
            Debug.Log("Not enough Energy");
            return;
        }
        if(_card.cardType == Card.CardType.Attack && BattleManager.cardTarget == null)
        {
            return;
        }
        if( _card.cardType == Card.CardType.Attack)
        {
            BattleManager.PlayCard(this);
            //animation
        }
        if( _card.cardType != Card.CardType.Attack)
        {
            Debug.Log("Skill, Power, Block");
            //animation
            BattleManager.PlayCard(this);
        }
    }

    public void AnimatonHoverOnCard()
    {
        animator.SetBool("Hover", true);
    }

    
}
