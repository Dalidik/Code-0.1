using System.Collections.Generic;
using Managers;
using UI.Components;
using UnityEngine;
using UnityEngine.UI;


namespace Data.ScriptableObjects
{
	public class PlayerIconController : MonoBehaviour
	{
        [Header("ScriptableObjects")]
        public List<PlayerIcon> AvailableIcons;
        public PlayerIcon CurrentIcon;

        [Header("Components")]
        [SerializeField] private Image _playerIconStarsCoinsPanel;
        [SerializeField] private Transform _iconContainer;
        [SerializeField] private GameObject _iconPrefab;
        [SerializeField] private Image _playerIcon;
        
        private void Awake() => GameManager.Player.OnIconChanged += LoadIcon;
        
        private void Start()
        {
            PopulateIcons();
            LoadIcon(GameManager.Player.IconId);
        }

        private void PopulateIcons()
        {
            foreach (Transform child in _iconContainer) Destroy(child.gameObject);
            
            for (var i = 0; i < AvailableIcons.Count; i++)
            {
                var iconInstance = Instantiate(_iconPrefab, _iconContainer);
                var buyPlayerIconScript = iconInstance.GetComponent<BuyPlayerIcon>();
                var iconImage = iconInstance.GetComponentInChildren<Image>();
                
                buyPlayerIconScript.PlayerIconController = this;
                iconImage.sprite = AvailableIcons[i].IconSprite;
                buyPlayerIconScript.Icon = AvailableIcons[i];
            }
        }
       
        
        public void SelectIcon(string index)
        {
            var iconIndex = AvailableIcons.FindIndex(i => i.Id == index);
            if (!(iconIndex > -1)) return;

            CurrentIcon = AvailableIcons[iconIndex];
            _playerIcon.sprite = CurrentIcon.IconSprite;
            _playerIconStarsCoinsPanel.sprite = CurrentIcon.IconSprite;
            SaveIcon();
        }

        private void SaveIcon()
        {
            if (CurrentIcon != null)
            {
                var iconId = CurrentIcon.Id;

                GameManager.Player.IconId = iconId;
                GameManager.Player.SaveState();
            }
        }

        private void LoadIcon(string index)
        {
            var iconIndex = AvailableIcons.FindIndex(i => i.Id == index);
            if (!(iconIndex > -1)) return;

            CurrentIcon = AvailableIcons[iconIndex];
            _playerIcon.sprite = CurrentIcon.IconSprite;
            _playerIconStarsCoinsPanel.sprite = CurrentIcon.IconSprite;
        }
        
    }
}


