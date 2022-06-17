using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentManager : MonoBehaviour
{

    #region Singleton

    private static TalentManager _instance;

    public static TalentManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void AwakeSingleton()
    {
        DontDestroyOnLoad(this);

        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    [SerializeField] private EconomyManager.CoinType _paymentMethod;
    [SerializeField] private int price = 100;
    // ? Talent list that we will have to save, or at least save its data somehow and load it back here
    [SerializeField] private Talent[] _talents = new Talent[12];
    [SerializeField] public int[] _maxLevel;
    private int _timesTalentsUpgraded;

    [System.Serializable]
    public class Talent
    {
        [SerializeField] public string name;
        [SerializeField] public int level;

        public void gainLevel() => level++;
    }

    public Talent[] Talents
    {
        get
        {
            return _talents;
        }
    }

    private void Awake() 
    {
        AwakeSingleton();
        loadData();    
        _maxLevel = new int[12]{3,3,3,3,3,3,3,3,3,3,3,3};
    }

    public void GiveRandomTalent()
    {
        bool userCanPay = true;
        bool completed = true;

        int i = 0;
        
        foreach(Talent t in _talents)
        {
            if(t.level != _maxLevel[i])
            {
                completed = false;
                break;
            }
            i++;
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

        if(_talents[rTalentIndex].level < _maxLevel[rTalentIndex])
        {
            _talents[rTalentIndex].gainLevel();
            return true;
        }
        return false;
    }   

    public void loadData()
    {
        int[] value = SaveDataController.TalentsList;

        for(int i = 0; i < value.Length; i++)
        {
            _talents[i].level = value[i];
        }
    }

    public void saveData()
    {
        int[] value = new int[12];

        for(int i = 0; i < value.Length; i++)
        {
            value[i] = _talents[i].level; 
        }

        SaveDataController.TalentsList = value;
    }

    public int GetTimesTalentsUpgraded()
    {
        _timesTalentsUpgraded = 0;
        foreach(Talent t in _talents)
        {
            _timesTalentsUpgraded += t.level;
        }
        return _timesTalentsUpgraded;
    }
}
