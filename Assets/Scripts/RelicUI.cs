using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RelicUI : MonoBehaviour
{
    public Relic relic;
    public Image relicIcon;
    public GameObject relicTextObj;
    public TMP_Text relicText;

    public void LoadRelic(Relic _relic)
    {
        relic = _relic;
        relicIcon.sprite = relic.relicIcon;
        relicText.text = relic.relicDescription;
    }
    public void OnHoverRelic()
    {
        relicText.gameObject.SetActive(true);
    }
    public void EndHoverRelic()
    {
        relicText.gameObject.SetActive(false);
    }
}
