using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace TemplateArquero
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Image _selector;
        [SerializeField] private ScrollRect _scrollScreens;
        [SerializeField] private InputAction _UIControls;

        #region Life Cycle

        private void OnEnable()
        {
            _UIControls.started += OnPress;
            _UIControls.Enable();
        }

        private void OnDisable()
        {
            _UIControls.Disable();
        }

        #endregion

        #region Private Methods

        private void OnPress(InputAction.CallbackContext context)
        {
            // Detectar que ha pulsado un botón
            // Mover el scroll
            // Mover el selector
        }

        #endregion

    }
}
