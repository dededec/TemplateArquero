using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TalentsController : MonoBehaviour
{

    [SerializeField] private TalentManager _talentManager;
    [SerializeField] private TextMeshProUGUI _priceText;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = _talentManager._talentList[i].talentLevel + "";
        }
        int aux = _talentManager.GetTimesTalentsUpgraded(); 
        float mod = 1f + aux/10f;

        _priceText.text = "Upgrade\n x" + (500 * mod);
    }

    public void GiveTalent()
    {
        _talentManager.GiveRandomTalent(); 
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = _talentManager._talentList[i].talentLevel + "";
        }

        int aux = _talentManager.GetTimesTalentsUpgraded(); 
        float mod = 1f + aux/10f;

        _priceText.text = "Upgrade\n x" + (500 * mod);
    }
}
