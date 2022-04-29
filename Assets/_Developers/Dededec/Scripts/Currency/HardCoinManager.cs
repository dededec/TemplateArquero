using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TemplateArquero
{
    public class HardCoinManager : MonoBehaviour
    {
        #region Fields

        private static HardCoinManager _instance;

        #endregion

        #region Properties

        public static HardCoinManager Instance
        {
            get
            {
                return _instance;
            }
        }

        public int HardCoins
        {
            get
            {
                return SaveDataController.HardCoins;
            }

            set
            {
                SaveDataController.HardCoins = value;
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
            if (quantity > HardCoins)
            {
                return false;
            }

            Debug.Log("Se han pagado hard coins.");
            HardCoins -= quantity;
            return true;
        }

        public void Add(int quantity)
        {
            Debug.Log("Se han añadido hard coins.");
            HardCoins += quantity;
        }

        #endregion
    }
}