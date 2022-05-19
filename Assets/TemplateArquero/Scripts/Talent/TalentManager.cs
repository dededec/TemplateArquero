using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentManager : MonoBehaviour
{
    [SerializeField] private EconomyManager.CoinType _paymentMethod;
    [SerializeField] private int price = 100;
    // ? Talent list that we will have to save, or at least save its data somehow and load it back here
    [SerializeField] private Talent[] _talentList = new Talent[12];
    [SerializeField] private int _maxLevel = 3;
    private int _timesTalentsUpgraded;

    [System.Serializable]
    public class Talent{
        [SerializeField] public string talentName;
        [SerializeField] public int talentLevel;

        public void gainLevel() => talentLevel++;
    }

    private void Awake() 
    {
        loadData();    
    }

    public void GiveRandomTalent()
    {
        bool userCanPay = true;
        bool completed = true;
        
        foreach(Talent t in _talentList)
        {
            if(t.talentLevel != _maxLevel)
            {
                completed = false;
                break;
            }
        }

        if(!completed) userCanPay = EconomyManager.Pay(_paymentMethod, price);
        if(userCanPay && !completed)
        {
            bool upgraded = selectRandomTalent();
            while(!upgraded) upgraded = selectRandomTalent();
            saveData();    
        }
    }

    private bool selectRandomTalent()
    {
        float rand = Random.value;
        System.Random r = new System.Random();
        int rTalentIndex = 0;

        if (rand <= .5f)
        {
            rTalentIndex = r.Next(0, 4);
        }
        else if(rand <= .8f)
        {
            rTalentIndex = r.Next(4, 8);
        }
        else
        {
            rTalentIndex = r.Next(8, 12);
        }

        if(_talentList[rTalentIndex].talentLevel < _maxLevel)
        {
            _talentList[rTalentIndex].gainLevel();
            return true;
        }
        return false;
    }   

    public void loadData()
    {
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
