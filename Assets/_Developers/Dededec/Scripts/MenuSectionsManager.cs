using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TemplateArquero
{
    public class MenuSectionsManager : MonoBehaviour
    {
        [SerializeField] private RectTransform _selector;
        [SerializeField] private ScrollRect _scrollScreens;
        [SerializeField] private List<Button> _buttons;

        #region Life Cycle

        private void OnEnable()
        {
            for (int i = 0; i < _buttons.Count; ++i)
            {
                var aux = _buttons[i].gameObject.GetComponent<RectTransform>();
                var index = i;
                _buttons[i].onClick.AddListener(delegate { OnClick(aux, index); });
            }
        }

        #endregion

        #region Public Methods

        public void LerpPosition(int orden)
        {
            StartCoroutine(crLerpPosition(orden));
        }

        #endregion

        #region Private Methods

        public void OnClick(RectTransform rect, float orden)
        {
            StartCoroutine(LerpPosition(rect, orden));
        }

        private IEnumerator LerpPosition(RectTransform rect, float orden)
        {
            var duration = 0.15f;
            var selectorStart = _selector.anchoredPosition;
            var selectorGoal = new Vector2(rect.anchoredPosition.x, _selector.anchoredPosition.y);
            var scrollStart = _scrollScreens.horizontalNormalizedPosition;
            var scrollGoal = Mathf.Lerp(0, 1, (float)orden / (float)(_buttons.Count - 1));

            for (float i = 0; i < duration; i += Time.deltaTime)
            {
                _selector.anchoredPosition = Vector2.Lerp(selectorStart, selectorGoal, i / duration);
                _scrollScreens.horizontalNormalizedPosition = Mathf.Lerp(scrollStart, scrollGoal, i / duration);
                yield return null;
            }

            _selector.anchoredPosition = selectorGoal;
            _scrollScreens.horizontalNormalizedPosition = scrollGoal;
        }

        private IEnumerator crLerpPosition(int orden)
        {
            var duration = 0.15f;
            var selectorStart = _selector.anchoredPosition;
            var selectorGoal = new Vector2(_buttons[orden].gameObject.GetComponent<RectTransform>().anchoredPosition.x, _selector.anchoredPosition.y);
            var scrollStart = _scrollScreens.horizontalNormalizedPosition;
            var scrollGoal = Mathf.Lerp(0, 1, (float)orden / (float)(_buttons.Count - 1));

            
            for (float i = 0f; i < duration; i += Time.deltaTime)
            {
                _selector.anchoredPosition = Vector2.Lerp(selectorStart, selectorGoal, i / duration);
                _scrollScreens.horizontalNormalizedPosition = Mathf.Lerp(scrollStart, scrollGoal, i / duration);
                yield return new WaitForEndOfFrame();
            }
                        
            _selector.anchoredPosition = selectorGoal;
            _scrollScreens.horizontalNormalizedPosition = scrollGoal;
        }

        #endregion

    }
}
