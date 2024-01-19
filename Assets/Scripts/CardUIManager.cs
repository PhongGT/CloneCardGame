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

    BattleManager battleManager;

    HorizontalLayoutGroup horizontalLayoutGroup;
    SortingGroup sortingGroup;

    Animator animator;

    private void Awake()
    {
        battleManager = FindObjectOfType<BattleManager>();
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
        GetComponent<RectTransform>().anchoredPosition += pos;
    }
    public void SetCardDescription(string des)
    {
        cardDescription.text = des;

    }
    public void SetCardIcon(Sprite image)
    {
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
        battleManager.selectedCard = this;
        Debug.Log(this.cardDescription.text);


    }
    public void DeSelectedCard()
    {
        battleManager.selectedCard = null;
        Debug.Log(this.cardDescription.text + " Null");

    }
    // Must make a custom Hand Holder to make this work
    public void HoverCard()
    {
        if (battleManager.selectedCard == null)
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
        if (_card.cardType == Card.CardType.Attack)
        {
            foreach (Enemy e in battleManager.enemies)
            {
                e.targetObject.SetActive(true);
            }
        }

    }
    public void HandleEndDragCard()
    {
        Debug.Log("In Handle End Drag!!");
        if (battleManager.energy < _card.GetCardValue())
        {
            Debug.Log("Not enough Energy");
            foreach (Enemy e in battleManager.enemies)
            {
                e.targetObject.SetActive(false);
            }
            return;
        }
        if (_card.cardType == Card.CardType.Attack && battleManager.cardTarget == null)
        {
            foreach (Enemy e in battleManager.enemies)
            {
                e.targetObject.SetActive(false);
            }
            return;
        }
        if (_card.cardType == Card.CardType.Attack)
        {
            StartCoroutine(battleManager.PlayCard(this));
            //animation
        }
        if (_card.cardType != Card.CardType.Attack)
        {
            Debug.Log("Skill, Power, Block");
            //animation
            StartCoroutine(battleManager.PlayCard(this));
        }

        foreach (Enemy e in battleManager.enemies)
        {
            e.targetObject.SetActive(false);
        }
    }

    public void AnimatonHoverOnCard()
    {
        animator.SetBool("Hover", true);
    }


}
