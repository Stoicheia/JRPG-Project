using System;
using Script.Game;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Script.Card
{
    public class CardRenderer : MonoBehaviour
    {
        public event Action<CardInstance> OnClick;
        
        [Header("Fields")] 
        [SerializeField] private TextMeshProUGUI _nameField;
        [SerializeField] private Image _spriteField;
        [SerializeField] private TextMeshProUGUI _descriptionField;
        [SerializeField] private Button _button;
        [FormerlySerializedAs("_loadedCardData")]
        [Header("State")] 
        [SerializeField] private CardInstance _loadedCardInstance;

        private void OnEnable()
        {
            _button.onClick.AddListener(HandleClickButton);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        public void Load(CardInstance data)
        {
            _loadedCardInstance = data;
            _nameField.text = _loadedCardInstance.Data.CardName;
            _spriteField.sprite = _loadedCardInstance.Data.Sprite;
            _descriptionField.text = _loadedCardInstance.Data.GetDescription(CombatManager.Instance.ActiveCombatant);
        }

        public void Reload()
        {
            Load(_loadedCardInstance);
        }

        public void LoadEmpty()
        {
            _loadedCardInstance = null;
            _nameField.text = "";
            _spriteField.sprite = null;
            _descriptionField.text = "";
        }

        private void HandleClickButton()
        {
            if (_loadedCardInstance != null)
            {
                OnClick?.Invoke(_loadedCardInstance);
            }
        }
    }
}