using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TemplateArquero
{
    public class EnergyManager : MonoBehaviour
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
        private float _timeRemaining;
        private int _currentEnergy;

        #endregion

        #region Properties

        public int Energy
        {
            get
            {
                return _currentEnergy;
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

        #region Private Methods

        private IEnumerator crRechargeEnergy()
        {
            yield return new WaitForSeconds(_timeToRecharge);
            _currentEnergy++;
        }

        private IEnumerator crRechargeEnergy2()
        {
            while(true)
            {
                
            }
            yield return new WaitForSeconds(_timeToRecharge);
            _currentEnergy++;
        }

        #endregion
    }
}
