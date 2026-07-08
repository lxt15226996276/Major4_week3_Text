using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Exam.Exam01
{
    /// <summary>Exam01 · 选服列表（作者 lixiaotong）：动态 10 条、单选、关闭 Panel</summary>
    public class ServerListController : MonoBehaviour
    {
        // 试卷要求：for (i=0; i<10; i++) Instantiate，Content 初始为空
        private const int ServerCount = 10;

        [Header("Prefabs & Roots")]
        [SerializeField] private GameObject serverItemPrefab;
        [SerializeField] private Transform contentRoot;
        [SerializeField] private GameObject serverPanel;
        [SerializeField] private Button closeButton;

        [Header("Optional")]
        [SerializeField] private Text selectedLabel;

        private readonly List<ServerItemView> _items = new List<ServerItemView>();
        private ServerItemView _currentSelected;

        private void Start()
        {
            SpawnServerItems();

            if (closeButton != null)
                closeButton.onClick.AddListener(ClosePanel);
        }

        private void SpawnServerItems()
        {
            if (serverItemPrefab == null || contentRoot == null)
                return;

            for (int i = 0; i < ServerCount; i++)
            {
                var instance = Instantiate(serverItemPrefab, contentRoot);
                var view = instance.GetComponent<ServerItemView>();
                if (view == null)
                    continue;

                view.Setup(i, OnItemClicked);
                _items.Add(view);
            }
        }

        private void OnItemClicked(ServerItemView clicked)
        {
            if (clicked == null || _currentSelected == clicked)
                return;

            _currentSelected?.SetSelected(false);
            _currentSelected = clicked;
            _currentSelected.SetSelected(true);

            if (selectedLabel != null)
                selectedLabel.text = _currentSelected.DisplayName;
        }

        /// <summary>试卷：CloseButton → serverPanel.SetActive(false)</summary>
        private void ClosePanel()
        {
            if (serverPanel != null)
                serverPanel.SetActive(false);
        }

        private void OnDestroy()
        {
            if (closeButton != null)
                closeButton.onClick.RemoveListener(ClosePanel);
        }
    }
}
