using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TemplateArquero
{
    public class EnergyManager : MonoBehaviour
    {
        #region Fields

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


        // Llamado al pulsar el botón de Play (ESTO DEBERIA DE IR EN OTRO LADO POR LOGICA)
        // public void OnPlay()
        // {
        //     if(Energy>=5)
        //     {
        //         Energy-=5;
        //         // Mandamos a la escena de juego
        //     }
        //     else
        //     {
        //         // Avisamos de que no hay energía
        //         Debug.Log("No hay energía suficiente para jugar");
        //     }
        // }
    }
}
