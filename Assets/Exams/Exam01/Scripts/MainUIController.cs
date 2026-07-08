using UnityEngine;
using UnityEngine.UI;

namespace Exam.Exam01
{
    /// <summary>W3 Exam01 · MainScene UI：背包 / 技能面板开关与技能详情</summary>
    public class MainUIController : MonoBehaviour
    {
        [Header("Bottom Nav")]
        [SerializeField] private Button _btnBackpack;
        [SerializeField] private Button _btnSkill;

        [Header("Panels")]
        [SerializeField] private GameObject _backpackPanel;
        [SerializeField] private GameObject _skillPanel;
        [SerializeField] private GameObject _skillDetailPanel;
        [SerializeField] private GameObject _itemDetailPanel;
        [SerializeField] private GameObject _equipDetailPanel;

        [Header("Close Buttons")]
        [SerializeField] private Button _btnCloseBackpack;
        [SerializeField] private Button _btnCloseSkill;
        [SerializeField] private Button _btnCloseDetail;
        [SerializeField] private Button _btnCloseItem;
        [SerializeField] private Button _btnCloseEquip;

        [Header("Skill Slots")]
        [SerializeField] private Button[] _skillSlotButtons;

        [Header("Detail Texts")]
        [SerializeField] private Text _detailTitleText;
        [SerializeField] private Text _detailDescText;

        private static readonly string[] SkillNames = { "冰霜刺", "雷霆击", "旋风刃", "烈焰斩" };
        private static readonly string[] SkillDescs =
        {
            "对目标造成冰霜伤害，并降低其移动速度。冷却时间：6秒。",
            "对目标造成雷电伤害，并有概率附加麻痹效果。冷却时间：8秒。",
            "对周围敌人造成旋风斩击伤害。冷却时间：10秒。",
            "对前方扇形区域造成火焰伤害。冷却时间：12秒。"
        };

        private UnityEngine.Events.UnityAction[] _skillSlotHandlers;

        private void Start()
        {
            BindButton(_btnBackpack, OpenBackpackPanel);
            BindButton(_btnSkill, OpenSkillPanel);
            BindButton(_btnCloseBackpack, CloseBackpackPanel);
            BindButton(_btnCloseSkill, CloseSkillPanel);
            BindButton(_btnCloseDetail, CloseSkillDetailPanel);
            BindButton(_btnCloseItem, CloseItemDetailPanel);
            BindButton(_btnCloseEquip, CloseEquipDetailPanel);

            BindSkillSlots();
        }

        private void BindSkillSlots()
        {
            if (_skillSlotButtons == null) return;

            _skillSlotHandlers = new UnityEngine.Events.UnityAction[_skillSlotButtons.Length];
            for (int i = 0; i < _skillSlotButtons.Length; i++)
            {
                if (_skillSlotButtons[i] == null) continue;
                int index = i;
                _skillSlotHandlers[i] = () => OpenSkillDetail(index);
                _skillSlotButtons[i].onClick.AddListener(_skillSlotHandlers[i]);
            }
        }

        public void OpenBackpackPanel()
        {
            SetPanelActive(_backpackPanel, true);
            SetPanelActive(_skillPanel, false);
            CloseAllDetailPanels();
        }

        public void CloseBackpackPanel()
        {
            SetPanelActive(_backpackPanel, false);
            CloseAllDetailPanels();
        }

        public void OpenSkillPanel()
        {
            SetPanelActive(_skillPanel, true);
            SetPanelActive(_backpackPanel, false);
            CloseAllDetailPanels();
        }

        public void CloseSkillPanel()
        {
            SetPanelActive(_skillPanel, false);
            CloseSkillDetailPanel();
        }

        public void OpenSkillDetail(int index)
        {
            if (_skillDetailPanel == null) return;

            _skillDetailPanel.SetActive(true);

            if (_detailTitleText != null && index >= 0 && index < SkillNames.Length)
                _detailTitleText.text = SkillNames[index];

            if (_detailDescText != null && index >= 0 && index < SkillDescs.Length)
                _detailDescText.text = SkillDescs[index];
        }

        public void CloseSkillDetailPanel()
        {
            SetPanelActive(_skillDetailPanel, false);
        }

        public void OpenItemDetailPanel()
        {
            SetPanelActive(_itemDetailPanel, true);
        }

        public void CloseItemDetailPanel()
        {
            SetPanelActive(_itemDetailPanel, false);
        }

        public void OpenEquipDetailPanel()
        {
            SetPanelActive(_equipDetailPanel, true);
        }

        public void CloseEquipDetailPanel()
        {
            SetPanelActive(_equipDetailPanel, false);
        }

        private void CloseAllDetailPanels()
        {
            CloseSkillDetailPanel();
            CloseItemDetailPanel();
            CloseEquipDetailPanel();
        }

        private static void SetPanelActive(GameObject panel, bool active)
        {
            if (panel != null)
                panel.SetActive(active);
        }

        private static void BindButton(Button button, UnityEngine.Events.UnityAction action)
        {
            if (button != null)
                button.onClick.AddListener(action);
        }

        private void OnDestroy()
        {
            UnbindButton(_btnBackpack, OpenBackpackPanel);
            UnbindButton(_btnSkill, OpenSkillPanel);
            UnbindButton(_btnCloseBackpack, CloseBackpackPanel);
            UnbindButton(_btnCloseSkill, CloseSkillPanel);
            UnbindButton(_btnCloseDetail, CloseSkillDetailPanel);
            UnbindButton(_btnCloseItem, CloseItemDetailPanel);
            UnbindButton(_btnCloseEquip, CloseEquipDetailPanel);

            if (_skillSlotButtons == null || _skillSlotHandlers == null) return;
            for (int i = 0; i < _skillSlotButtons.Length; i++)
            {
                if (_skillSlotButtons[i] == null || _skillSlotHandlers[i] == null) continue;
                _skillSlotButtons[i].onClick.RemoveListener(_skillSlotHandlers[i]);
            }
        }

        private static void UnbindButton(Button button, UnityEngine.Events.UnityAction action)
        {
            if (button != null)
                button.onClick.RemoveListener(action);
        }
    }
}
