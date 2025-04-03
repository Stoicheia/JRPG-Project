using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Card
{
    public class Card : MonoBehaviour
    {
        [Header("Fields")] 
        [SerializeField] private TextMeshProUGUI _nameField;
        [SerializeField] private Image _spriteField;
        [SerializeField] private TextMeshProUGUI _descriptionField;
        [SerializeField] private Button _button;
        [Header("State")] 
        [SerializeField] private CardData _loadedCardData;

        public void Load(CardData data)
        {
            _loadedCardData = data;
            _nameField.text = _loadedCardData.CardName;
            _spriteField.sprite = _loadedCardData.Sprite;
            _descriptionField.text = _loadedCardData.Description;
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(HandleClickButton);
        }

        private void HandleClickButton()
        {
            
        }
    }
}