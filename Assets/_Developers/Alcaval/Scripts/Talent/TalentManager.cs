using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentManager : MonoBehaviour
{

    [SerializeField] private EconomyManager.CoinType _paymentMethod;
    [SerializeField] private int price = 100;
    // ? Talent list that we will have to save, or at least save its data somehow and load it back here
    [SerializeField] private Talent[] _talentList = new Talent[12];

    // ? TALENT ORDER: In case we have a spreadsheet to follow a talent order
    private List<Talent> _talentOrder = new List<Talent>();
    private int _timesTalentsUpgraded;

    [System.Serializable]
    public class Talent{
        [SerializeField] public string talentName;
        [SerializeField] public int talentLevel;
        public bool LastLevelGained;

        public void gainLevel() => talentLevel++;
    }

    private void Awake() 
    {
        loadData();    
    }

    public void GiveRandomTalent()
    {
        bool userCanPay = true;
        userCanPay = EconomyManager.Pay(_paymentMethod, price);

        if(userCanPay)
        {
            System.Random r = new System.Random();
            int rTalentIndex = r.Next(0, _talentList.Length);

            _talentList[rTalentIndex].gainLevel();

            saveData();    
        }
    }

    public void loadData()
    {
        //TODO Load CSV of talent order in case we choose that route

        int[] value = SaveDataController.TalentsList;

        for(int i = 0; i < value.Length; i++)
        {
            _talentList[i].talentLevel = value[i];
        }
    }

    public void saveData()
    {
        int[] value = new int[12];

        for(int i = 0; i < value.Length; i++)
        {
            value[i] = _talentList[i].talentLevel; 
        }

        SaveDataController.TalentsList = value;
    }
}
