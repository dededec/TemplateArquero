using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TemplateArquero
{
    public class EnergyPayButton : MonoBehaviour
    {
        [SerializeField] private int _payAmount;
        [SerializeField] private int _energyRecovered;
        [SerializeField] private GameObject _payUI;
        [SerializeField] private MenuSectionsManager _menuManager;

        private void Awake()
        {
            this.GetComponent<Button>().onClick.AddListener(delegate { OnClick(); });
        }

        public void OnClick()
        {
            if (HardCoinManager.Instance.Pay(_payAmount))
            {
                Debug.Log("Se paga no problem");
                EnergyManager.Instance.Add(_energyRecovered);
            }
            else
            {
                Debug.Log("Demasie money");
                _menuManager.LerpPosition(0);
            }

            _payUI.SetActive(false);

        }
    }
}
