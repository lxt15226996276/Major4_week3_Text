using System;
using UnityEngine;
using UnityEngine.UI;

namespace Exam.Exam01
{
    /// <summary>Exam01 · 单条区服 UI（作者 lixiaotong）</summary>
    public class ServerItemView : MonoBehaviour
    {
        [SerializeField] private Text serverItemNameText;
        [SerializeField] private Button itemButton;
        [SerializeField] private Image serverItemImage;
        [SerializeField] private Sprite normalSprite;
        [SerializeField] private Sprite highlightedSprite;

        private Action<ServerItemView> _onClick;

        public string DisplayName => serverItemNameText != null ? serverItemNameText.text : string.Empty;

        public void Setup(int index, Action<ServerItemView> onClick)
        {
            _onClick = onClick;
            if (serverItemNameText != null)
                serverItemNameText.text = "服务器" + (index + 1);

            SetSelected(false);

            if (itemButton != null)
                itemButton.onClick.AddListener(HandleClick);
        }

        public void SetSelected(bool selected)
        {
            if (serverItemImage == null)
                return;

            serverItemImage.sprite = selected ? highlightedSprite : normalSprite;
        }

        private void HandleClick()
        {
            _onClick?.Invoke(this);
        }

        private void OnDestroy()
        {
            if (itemButton != null)
                itemButton.onClick.RemoveListener(HandleClick);
        }
    }
}
