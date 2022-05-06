using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentManager : MonoBehaviour
{
    
    [SerializeField] private Talent[] _talentList = new Talent[12];

    [System.Serializable]
    public class Talent{
        [SerializeField] private string talentName;
        [SerializeField] private int talentLevel;
    }

    public void giveTalent()
    {
        
    }
}
