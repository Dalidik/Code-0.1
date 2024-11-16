using System;
using Data.JSON;
using Managers;
using TMPro;
using UI.Components;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    
    public class PlayerIconsWindow : BaseWindow
    {
        [Header("Components")]
        [SerializeField] private UIButton _closeButton;
        [SerializeField] private TopButtonsPanel _topPanel;
        [SerializeField] private TMP_InputField _usernameInputField;
        [SerializeField] private ScrollRect _scrollRect;
        
        private MapUpgradeData _upgradeData;
        
        public Action<string> OnUsernameChanged = delegate { };

        protected override void Awake()
        {
            base.Awake();
            _usernameInputField.text = GameManager.Player.Username;
        }

        public override void Show()
        {
            base.Show();
            _topPanel.ShowPanel(TopButtonPanelType.Nickname);
        }
        
        private void OnEnable()
        {
            _closeButton.OnClick += Save;
            _closeButton.OnClick += ShowTopPanel;
        }

        private void OnDisable()
        {
            _closeButton.OnClick -= Save;
            _closeButton.OnClick -= ShowTopPanel;
        }

       private void  ShowTopPanel()
        {
            _topPanel.SetTopPanelVisible(true);
        }

       private void Save()
       {
           if (string.IsNullOrEmpty(_usernameInputField.text) || string.IsNullOrWhiteSpace(_usernameInputField.text))
               _usernameInputField.text = GameManager.Player.Username;

           if (!_usernameInputField.text.Equals(GameManager.Player.Username))
           {
               GameManager.Player.Username = _usernameInputField.text;
               OnUsernameChanged.Invoke(_usernameInputField.text);
               GameManager.Player.SaveState();
               GameManager.AppService.UpdateUserData();

               GameManager.Analytics.RecordUsernameChanged();
               GameManager.BoombitAnalytics.LogChangedNameEvent();
           }

           _scrollRect.verticalNormalizedPosition = 1;
           Close();
       }

       public override bool HasPauseEffect() => false;
    }
}