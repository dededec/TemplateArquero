using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TemplateArquero
{
    public class HardCoinManager : MonoBehaviour
    {
        #region Fields

        private static HardCoinManager _instance;
        private int _currentValue;

        #endregion

        #region Properties

        public static HardCoinManager Instance
        {
            get
            {
                return _instance;
            }
        }

        public int Value
        {
            get
            {
                return _currentValue;
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

        public bool Pay(int quantity)
        {
            if (quantity > _currentValue)
            {
                return false;
            }

            Debug.Log("Se han añadido hard coin");
            _currentValue -= quantity;
            return true;
        }

        public void Add(int quantity) => Pay(-quantity);

        #endregion
    }
}