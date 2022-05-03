using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TemplateArquero
{
    public class EnergyManager : EconomyManager
    {
        #region Singleton
        private static EnergyManager _instance;
        public static EnergyManager Instance
        {
            get
            {
                return _instance;
            }
        }

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

        #region Fields

        [SerializeField] private GameObject _rechargeUI;
        [SerializeField] private float _timeToRecharge;
        private float _timeElapsed;

        #endregion

        #region Properties

        public int Energy
        {
            get
            {
                return SaveDataController.Energy;
            }

            set
            {
                SaveDataController.Energy = value;
            }
        }

        public float TimeToRecharge
        {
            get;
            set;
        }

        #endregion

        #region Life Cycle

        private void Update() 
        {
            if(_timeElapsed > _timeToRecharge)
            {
                // Recarga
                _timeElapsed = 0;
                Energy++;
            }
            else
            {
                _timeElapsed += Time.deltaTime;
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
            if (quantity > Energy)
            {
                return false;
            }

            Energy -= quantity;
            return true;
        }

        public override bool Pay(int amount)
        {
            return Substract(amount);
        }

        public override void Add(int quantity)
        {
            Energy += quantity;
        }

        #endregion

    }
}
