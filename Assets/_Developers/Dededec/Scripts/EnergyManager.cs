using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TemplateArquero
{
    public class EnergyManager : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _rechargeUI;
        private static EnergyManager _instance;
        private int _currentEnergy;

        #endregion

        #region Properties

        public static EnergyManager Instance
        {
            get
            {
                return _instance;
            }
        }

        public int Energy
        {
            get
            {
                return _currentEnergy;
            }
        }

        #endregion

        #region Life Cycle

        private void Awake()
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

        #region Public Methods

        public void ShowRechargePopUp()
        {
            _rechargeUI.SetActive(true);
        }

        public bool Substract(int quantity)
        {
            if (quantity > _currentEnergy)
            {
                return false;
            }

            _currentEnergy -= quantity;
            return true;
        }

        public void Add(int quantity) => Substract(-quantity);

        #endregion
    }
}
