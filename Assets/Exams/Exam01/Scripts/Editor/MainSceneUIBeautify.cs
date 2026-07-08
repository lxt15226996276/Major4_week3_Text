using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.SceneManagement;

namespace Exam.Exam01.Editor
{
    /// <summary>
    /// Exam01 MainScene UI 分步搭建（对照 Assets/Images/效果图，适配套题考试）。
    /// 每步独立菜单，完成一步 Play 预览，回复 OK 再继续下一步。
    /// </summary>
    public static class MainSceneUIBeautify
    {
        private const string ScenePath = "Assets/Exams/Exam01/Scenes/Exam01_MainScene.unity";
        private const string FontPath = "Assets/Images/Font/方正粗圆简体.ttf";
        private const float RefWidth = 1920f;
        private const float RefHeight = 1080f;

        // ── 美术资源路径 ──
        private const string SprBgMap = "Assets/Images/003-taskother/bg_地图.png";
        private const string SprBgDim = "Assets/Images/003-taskother/半透明背景.png";
        private const string SprAvatarFrame = "Assets/Images/002-mainmenu/头像底板男性.png";
        private const string SprInfoBg = "Assets/Images/001-startmenu/pic_信息底.png";
        private const string SprBarBg = "Assets/Images/001-startmenu/pic_进度条底.png";
        private const string SprBarFillGreen = "Assets/Images/001-startmenu/pic_进度条.png";
        private const string SprBarFillYellow = "Assets/Images/002-mainmenu/pic_经验条长.png";
        private const string SprStaminaLabel = "Assets/Images/002-mainmenu/pic_体力.png";
        private const string SprCurrencyBg = "Assets/Images/002-mainmenu/bg_金币.png";
        private const string SprGoldIcon = "Assets/Images/002-mainmenu/金币.png";
        private const string SprDiamondIcon = "Assets/Images/002-mainmenu/钻石.png";
        private const string SprBtnPlus1 = "Assets/Images/002-mainmenu/btn_加号1.png";
        private const string SprBtnPlus2 = "Assets/Images/002-mainmenu/btn_加号2.png";
        private const string SprBtnPlus3 = "Assets/Images/002-mainmenu/btn_加号3.png";
        private const string SprBtnPlus4 = "Assets/Images/002-mainmenu/btn_加号4.png";

        // 底部导航图标（002-mainmenu 圆形功能钮）
        private const string SprMenuTask = "Assets/Images/002-mainmenu/任务.png";
        private const string SprMenuSystem = "Assets/Images/002-mainmenu/系统.png";
        private const string SprMenuBattle = "Assets/Images/002-mainmenu/战斗.png";
        private const string SprMenuSkill = "Assets/Images/002-mainmenu/技能.png";
        private const string SprMenuShop = "Assets/Images/002-mainmenu/商城.png";
        private const string SprMenuBackpack = "Assets/Images/002-mainmenu/背包.png";

        private static readonly (string btnName, string spritePath)[] BottomMenuItems =
        {
            ("BtnTask", SprMenuTask),
            ("BtnSystem", SprMenuSystem),
            ("BtnBattle", SprMenuBattle),
            ("BtnSkill", SprMenuSkill),
            ("BtnShop", SprMenuShop),
            ("BtnBackpack", SprMenuBackpack),
        };

        // 背包面板美术
        private const string SprBackpackBg = "Assets/Images/002-mainmenu/bg_背包.png";
        private const string SprCharacterBg = "Assets/Images/002-mainmenu/bg_角色.png";
        private const string SprItemSlot = "Assets/Images/002-mainmenu/bg_道具.png";
        private const string SprCharacterPortrait = "Assets/Images/003-taskother/NPC对话半身像288x347.png";
        private const string SprStatLifeBg = "Assets/Images/002-mainmenu/bg_角色_生命.png";
        private const string SprExpBarFill = "Assets/Images/002-mainmenu/pic_经验条长.png";
        private const string SprExpBarBg = "Assets/Images/002-mainmenu/bg_经验条长.png";
        private const string SprSampleItem = "Assets/Images/002-mainmenu/小体力丹.png";
        private const string SprBtnClose1 = "Assets/Images/001-startmenu/btn_关闭1.png";
        private const string SprBtnClose2 = "Assets/Images/001-startmenu/btn_关闭2.png";
        private const string SprBtnClose3 = "Assets/Images/001-startmenu/btn_关闭3.png";
        private const string SprBtnClose4 = "Assets/Images/001-startmenu/btn_关闭4.png";
        private const string SprBtnDetail1 = "Assets/Images/002-mainmenu/btn_详细信息1.png";
        private const string SprBtnDetail2 = "Assets/Images/002-mainmenu/btn_详细信息2.png";
        private const string SprBtnDetail3 = "Assets/Images/002-mainmenu/btn_详细信息3.png";
        private const string SprBtnDetail4 = "Assets/Images/002-mainmenu/btn_详细信息4.png";
        private const string SprBtnSell1 = "Assets/Images/002-mainmenu/btn_出售1.png";
        private const string SprBtnSell2 = "Assets/Images/002-mainmenu/btn_出售2.png";
        private const string SprBtnSell3 = "Assets/Images/002-mainmenu/btn_出售3.png";
        private const string SprBtnSell4 = "Assets/Images/002-mainmenu/btn_出售4.png";
        private const string SprBtnSort1 = "Assets/Images/002-mainmenu/btn_整理1.png";
        private const string SprBtnSort2 = "Assets/Images/002-mainmenu/btn_整理2.png";
        private const string SprBtnSort3 = "Assets/Images/002-mainmenu/btn_整理3.png";
        private const string SprBtnSort4 = "Assets/Images/002-mainmenu/btn_整理4.png";

        private static readonly string[] LeftEquipPaths =
        {
            "Assets/Images/002-mainmenu/装备/女性盔甲 (2).png",
            "Assets/Images/002-mainmenu/装备/女性帽子 (2).png",
            "Assets/Images/002-mainmenu/装备/女性武器 (2).png",
            "Assets/Images/002-mainmenu/装备/女性 靴子 (2).png",
        };

        private static readonly string[] RightEquipPaths =
        {
            "Assets/Images/002-mainmenu/装备/女性项链 (1).png",
            "Assets/Images/002-mainmenu/装备/女性戒指 (1).png",
            "Assets/Images/002-mainmenu/装备/女性手镯 (2).png",
            "Assets/Images/002-mainmenu/装备/女性翅膀 (2).png",
        };

        // 技能面板美术
        private const string SprSkillBg = "Assets/Images/003-taskother/技能系统背景.png";
        private const string SprSkillIconZhu = "Assets/Images/004-skill icon/icon_zhu.png";
        private const string SprSkillIconLi = "Assets/Images/004-skill icon/icon_li.png";
        private const string SprSkillIconHo = "Assets/Images/004-skill icon/iocn_ho.png";
        private const string SprSkillIconFo = "Assets/Images/004-skill icon/iocn_fo.png";

        private static readonly (string btnName, string skillName, string iconPath, int frameSet)[]
            SkillButtonDefs =
        {
            ("BtnSkill0", "冰霜刺", SprSkillIconZhu, 1),
            ("BtnSkill1", "雷霆击", SprSkillIconLi, 2),
            ("BtnSkill2", "旋风刃", SprSkillIconHo, 3),
            ("BtnSkill3", "烈焰斩", SprSkillIconFo, 1),
        };

        // 详情弹窗美术
        private const string SprDialogBg = "Assets/Images/002-mainmenu/bg_弹框.png";
        private const string SprEquipDetailBg = "Assets/Images/002-mainmenu/bg_装备详情.png";
        private const string SprEquippedBadge = "Assets/Images/002-mainmenu/pic_已装备.png";
        private const string SprStar = "Assets/Images/002-mainmenu/pic_星星.png";
        private const string SprTagAngel = "Assets/Images/002-mainmenu/pic_天使.png";
        private const string SprTagLegend = "Assets/Images/002-mainmenu/pic_传说.png";
        private const string SprChestIcon = "Assets/Images/002-mainmenu/宝箱.png";
        private const string SprSampleArmor = "Assets/Images/002-mainmenu/装备/女性盔甲 (2).png";
        private const string SprBtnDialog1 = "Assets/Images/002-mainmenu/btn_弹框1.png";
        private const string SprBtnDialog2 = "Assets/Images/002-mainmenu/btn_弹框2.png";
        private const string SprBtnDialog3 = "Assets/Images/002-mainmenu/btn_弹框3.png";
        private const string SprBtnDialog4 = "Assets/Images/002-mainmenu/btn_弹框4.png";
        private const string SprBtnEquip1 = "Assets/Images/002-mainmenu/btn_装备1.png";
        private const string SprBtnEquip2 = "Assets/Images/002-mainmenu/btn_装备2.png";
        private const string SprBtnEquip3 = "Assets/Images/002-mainmenu/btn_装备3.png";
        private const string SprBtnEquip4 = "Assets/Images/002-mainmenu/btn_装备4.png";
        private const string SprBtnAction1 = "Assets/Images/003-taskother/公用四字按钮198x70_1.png";
        private const string SprBtnAction2 = "Assets/Images/003-taskother/公用四字按钮198x70_2.png";
        private const string SprBtnAction3 = "Assets/Images/003-taskother/公用四字按钮198x70_3.png";
        private const string SprBtnAction4 = "Assets/Images/003-taskother/公用四字按钮198x70_4.png";

        // ─────────────────────────────────────────────
        //  第 1 步：Canvas + 纯色背景（考试够用，不含复杂 HUD）
        // ─────────────────────────────────────────────
        [MenuItem("Exam/Exam01/UI美化/第1步 Canvas+背景")]
        public static void Step1_CanvasAndBackground()
        {
            var canvas = EnsureMainCanvas();
            if (canvas == null) return;

            ApplySimpleBackground(canvas.transform);
            DisableSceneLightingForUi();
            MarkSceneDirty();
            Debug.Log("[Exam01] 第1步完成：Canvas + 背景。回复 OK 进行第2步。");
        }

        // ─────────────────────────────────────────────
        //  一键搭建（考试简化版）：1～6 步合并，够交卷/练习即可
        // ─────────────────────────────────────────────
        [MenuItem("Exam/Exam01/一键搭建 MainScene（考试简化版）")]
        public static void BuildSimpleMainScene()
        {
            if (EditorSceneManager.GetActiveScene().path != ScenePath)
            {
                if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    return;
                EditorSceneManager.OpenScene(ScenePath);
            }

            var canvas = EnsureMainCanvas();
            if (canvas == null) return;

            Font font = AssetDatabase.LoadAssetAtPath<Font>(FontPath);

            ApplySimpleBackground(canvas.transform);
            BuildBottomNavBar(canvas.transform);

            var backpack = EnsureBackpackPanel(canvas.transform);
            BuildSimpleBackpackPanel(backpack.transform, font);

            var skill = EnsureSkillPanel(canvas.transform);
            BuildSimpleSkillPanel(skill.transform, font);

            BuildSimpleSkillDetailPanel(
                EnsureDetailPanel(canvas, "SkillDetailPanel", 520f, 360f).transform, font);

            DisableSceneLightingForUi();
            Step6_WireMainUIControllerInternal(canvas, font);

            EditorSceneManager.SaveOpenScenes();
            Debug.Log("[Exam01] 一键搭建完成：底部导航 + 背包/技能面板 + 脚本接线。Play 测试即可。");
        }

        // ─────────────────────────────────────────────
        //  第 2 步：底部 6 个圆形功能导航（任务/系统/战斗/技能/商店/背包）
        // ─────────────────────────────────────────────
        [MenuItem("Exam/Exam01/UI美化/第2步 底部导航栏")]
        public static void Step2_BottomNavBar()
        {
            var canvas = EnsureMainCanvas();
            if (canvas == null) return;

            BuildBottomNavBar(canvas.transform);

            MarkSceneDirty();
            Debug.Log("[Exam01] 第2步完成：底部 6 个导航按钮。Play 预览，OK 后进第3步。");
        }

        // ─────────────────────────────────────────────
        //  第 3 步：背包面板（左角色装备 + 右 4×5 格子 + 出售/整理）
        // ─────────────────────────────────────────────
        [MenuItem("Exam/Exam01/UI美化/第3步 背包面板")]
        public static void Step3_BackpackPanel()
        {
            var canvas = EnsureMainCanvas();
            if (canvas == null) return;

            Font font = AssetDatabase.LoadAssetAtPath<Font>(FontPath);
            var panel = EnsureBackpackPanel(canvas.transform);
            BuildSimpleBackpackPanel(panel.transform, font);

            MarkSceneDirty();
            Debug.Log("[Exam01] 第3步完成：背包面板（格子区已对齐右侧库存区）。请重新运行本菜单或 Play 预览。");
        }

        // ─────────────────────────────────────────────
        //  第 4 步：技能面板（技能背景 + 4 技能图标 + 关闭按钮）
        // ─────────────────────────────────────────────
        [MenuItem("Exam/Exam01/UI美化/第4步 技能面板")]
        public static void Step4_SkillPanel()
        {
            var canvas = EnsureMainCanvas();
            if (canvas == null) return;

            Font font = AssetDatabase.LoadAssetAtPath<Font>(FontPath);
            var panel = EnsureSkillPanel(canvas.transform);
            BuildSimpleSkillPanel(panel.transform, font);

            MarkSceneDirty();
            Debug.Log("[Exam01] 第4步完成：技能面板（简化）。");
        }

        // ─────────────────────────────────────────────
        //  第 5 步：物品/装备/技能详情弹窗
        // ─────────────────────────────────────────────
        [MenuItem("Exam/Exam01/UI美化/第5步 详情弹窗")]
        public static void Step5_DetailPanels()
        {
            var canvas = EnsureMainCanvas();
            if (canvas == null) return;

            Font font = AssetDatabase.LoadAssetAtPath<Font>(FontPath);
            BuildSimpleSkillDetailPanel(
                EnsureDetailPanel(canvas, "SkillDetailPanel", 520f, 360f).transform, font);

            MarkSceneDirty();
            Debug.Log("[Exam01] 第5步完成：技能详情弹窗（简化）。");
        }

        // ─────────────────────────────────────────────
        //  第 6 步：MainUIController 脚本 + 自动接线
        // ─────────────────────────────────────────────
        [MenuItem("Exam/Exam01/UI美化/第6步 脚本与交互")]
        public static void Step6_WireMainUIController()
        {
            if (EditorSceneManager.GetActiveScene().path != ScenePath)
            {
                if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    return;
                EditorSceneManager.OpenScene(ScenePath);
            }

            var canvas = EnsureMainCanvas();
            if (canvas == null) return;

            Font font = AssetDatabase.LoadAssetAtPath<Font>(FontPath);
            Step6_WireMainUIControllerInternal(canvas, font);
            MarkSceneDirty();
            Debug.Log("[Exam01] 第6步完成：MainUIController 已接线。Play 测试背包/技能按钮。");
        }

        private static void Step6_WireMainUIControllerInternal(Transform canvas, Font font = null)
        {
            var uiManagerGo = GameObject.Find("UIManager");
            if (uiManagerGo == null)
                uiManagerGo = new GameObject("UIManager");

            GameObjectUtility.RemoveMonoBehavioursWithMissingScript(uiManagerGo);

            var controller = uiManagerGo.GetComponent<MainUIController>();
            if (controller == null)
                controller = uiManagerGo.AddComponent<MainUIController>();

            WireMainUIController(controller, canvas);
        }

        private static void WireMainUIController(MainUIController controller, Transform canvas)
        {
            var so = new SerializedObject(controller);

            SetRef(so, "_btnBackpack", FindButton(canvas, "BottomBar/BtnBackpack"));
            SetRef(so, "_btnSkill", FindButton(canvas, "BottomBar/BtnSkill"));

            SetRef(so, "_backpackPanel", FindGo(canvas, "BackpackPanel"));
            SetRef(so, "_skillPanel", FindGo(canvas, "SkillPanel"));
            SetRef(so, "_skillDetailPanel", FindGo(canvas, "SkillDetailPanel"));
            SetRef(so, "_itemDetailPanel", FindGo(canvas, "ItemDetailPanel"));
            SetRef(so, "_equipDetailPanel", FindGo(canvas, "EquipDetailPanel"));

            SetRef(so, "_btnCloseBackpack", FindButton(canvas, "BackpackPanel/PanelRoot/BtnCloseBackpack"));
            SetRef(so, "_btnCloseSkill", FindButton(canvas, "SkillPanel/PanelRoot/BtnCloseSkill"));
            SetRef(so, "_btnCloseDetail", FindButton(canvas, "SkillDetailPanel/PanelRoot/BtnCloseDetail"));
            SetRef(so, "_btnCloseItem", FindButton(canvas, "ItemDetailPanel/PanelRoot/BtnCloseItem"));
            SetRef(so, "_btnCloseEquip", FindButton(canvas, "EquipDetailPanel/PanelRoot/BtnCloseEquip"));

            SetRef(so, "_detailTitleText", FindText(canvas,
                "SkillDetailPanel/PanelRoot/DetailTitleText/Text")
                ?? FindText(canvas, "SkillDetailPanel/PanelRoot/DetailTitleText"));
            SetRef(so, "_detailDescText", FindText(canvas,
                "SkillDetailPanel/PanelRoot/DetailDescText/Text")
                ?? FindText(canvas, "SkillDetailPanel/PanelRoot/DetailDescText"));

            var slotsProp = so.FindProperty("_skillSlotButtons");
            if (slotsProp != null)
            {
                var row = canvas.Find("SkillPanel/PanelRoot/SkillButtonRow");
                if (row != null)
                {
                    slotsProp.arraySize = 4;
                    for (int i = 0; i < 4; i++)
                    {
                        var btn = row.Find("BtnSkill" + i)?.GetComponent<Button>();
                        slotsProp.GetArrayElementAtIndex(i).objectReferenceValue = btn;
                    }
                }
            }

            so.ApplyModifiedPropertiesWithoutUndo();
            EditorUtility.SetDirty(controller);
        }

        private static void SetRef(SerializedObject so, string propName, Object value)
        {
            var prop = so.FindProperty(propName);
            if (prop != null)
                prop.objectReferenceValue = value;
        }

        private static GameObject FindGo(Transform root, string path)
        {
            return root.Find(path)?.gameObject;
        }

        private static Button FindButton(Transform root, string path)
        {
            return root.Find(path)?.GetComponent<Button>();
        }

        private static Text FindText(Transform root, string path)
        {
            return root.Find(path)?.GetComponent<Text>();
        }

        // ═══════════════════════════════════════════════
        private static Transform EnsureMainCanvas()
        {
            if (EditorSceneManager.GetActiveScene().path != ScenePath)
            {
                if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    return null;
                EditorSceneManager.OpenScene(ScenePath);
            }

            var canvasGo = GameObject.Find("MainCanvas");
            if (canvasGo == null)
                canvasGo = GameObject.Find("Canvas");

            if (canvasGo == null)
            {
                canvasGo = CreateMainCanvasRoot();
                Debug.Log("[Exam01 美化] 已自动创建 MainCanvas（1920×1080 · Match 0.5）。");
            }

            EnsureEventSystem();
            return canvasGo.transform;
        }

        private static GameObject CreateMainCanvasRoot()
        {
            var canvasGo = new GameObject("MainCanvas", typeof(RectTransform));
            canvasGo.layer = LayerMask.NameToLayer("UI");

            var canvas = canvasGo.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 0;

            var scaler = canvasGo.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(RefWidth, RefHeight);
            scaler.matchWidthOrHeight = 0.5f;

            canvasGo.AddComponent<GraphicRaycaster>();
            return canvasGo;
        }

        private static void EnsureEventSystem()
        {
            if (Object.FindObjectOfType<EventSystem>() != null)
                return;

            var eventSystemGo = new GameObject("EventSystem");
            eventSystemGo.AddComponent<EventSystem>();
            eventSystemGo.AddComponent<StandaloneInputModule>();
        }

        private static void DisableSceneLightingForUi()
        {
            var light = GameObject.Find("Directional Light");
            if (light != null)
                light.SetActive(false);
        }

        private static void MarkSceneDirty()
        {
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            EditorUtility.SetDirty(EnsureMainCanvas()?.gameObject);
        }

        // ── 简化版 UI（考试够用）──
        private static void ApplySimpleBackground(Transform canvas)
        {
            DestroyChildIfExists(canvas, "TopHudBar");
            DestroyChildIfExists(canvas, "BgDimmer");

            var bgImage = FindOrCreate("BgImage", canvas);
            SetStretchFull(bgImage);
            var bgSprite = LoadSprite("Assets/Images/001-startmenu/BG.png");
            var img = SetImage(bgImage, bgSprite, raycast: false);
            img.color = bgSprite != null ? Color.white : new Color(0.12f, 0.14f, 0.2f, 1f);
            bgImage.transform.SetAsFirstSibling();
        }

        private static void DestroyChildIfExists(Transform parent, string name)
        {
            var child = parent.Find(name);
            if (child != null)
                Object.DestroyImmediate(child.gameObject);
        }

        private static void BuildSimpleBackpackPanel(Transform panel, Font font)
        {
            ClearAllChildren(panel);

            const float panelW = 1050f;
            const float panelH = 760f;
            SetRect(panel.gameObject,
                anchorMin: new Vector2(0.5f, 0.5f), anchorMax: new Vector2(0.5f, 0.5f),
                pivot: new Vector2(0.5f, 0.5f),
                sizeDelta: new Vector2(panelW, panelH),
                anchoredPos: Vector2.zero);

            var root = FindOrCreate("PanelRoot", panel);
            SetStretchFull(root);
            var rootImg = SetImage(root, LoadSprite(SprBackpackBg), raycast: true);
            rootImg.type = Image.Type.Sliced;

            var titleGo = FindOrCreate("TitleText", root.transform);
            SetRect(titleGo,
                anchorMin: new Vector2(0.72f, 1f), anchorMax: new Vector2(0.72f, 1f),
                pivot: new Vector2(0.5f, 1f),
                sizeDelta: new Vector2(240f, 48f), anchoredPos: new Vector2(0f, -20f));
            CreateText(titleGo.transform, "Text", "背包", font, 32, TextAnchor.MiddleCenter, Color.white);

            CreateSpriteSwapActionButton(root.transform, "BtnCloseBackpack",
                LoadSprite(SprBtnClose1), LoadSprite(SprBtnClose2),
                LoadSprite(SprBtnClose3), LoadSprite(SprBtnClose4),
                new Vector2(0.96f, 1f), new Vector2(0.96f, 1f), new Vector2(1f, 1f),
                new Vector2(48f, 48f), new Vector2(-16f, -16f));

            // bg_背包 是左右双栏图：格子区锚定在右侧库存区域并随面板拉伸
            var gridRoot = FindOrCreate("InventoryGrid", root.transform);
            SetStretchAnchors(gridRoot,
                new Vector2(0.48f, 0.14f), new Vector2(0.96f, 0.84f),
                Vector2.zero, Vector2.zero);

            const int cols = 4;
            const int rows = 5;
            const float spacing = 10f;
            float gridW = panelW * (0.96f - 0.48f);
            float gridH = panelH * (0.84f - 0.14f);
            float cell = Mathf.Floor(Mathf.Min(
                (gridW - spacing * (cols - 1)) / cols,
                (gridH - spacing * (rows - 1)) / rows));

            var grid = gridRoot.GetComponent<GridLayoutGroup>();
            if (grid == null) grid = gridRoot.AddComponent<GridLayoutGroup>();
            grid.cellSize = new Vector2(cell, cell);
            grid.spacing = new Vector2(spacing, spacing);
            grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            grid.constraintCount = cols;
            grid.childAlignment = TextAnchor.MiddleCenter;
            grid.padding = new RectOffset(6, 6, 6, 6);

            for (int i = 0; i < cols * rows; i++)
                CreateInventorySlot(gridRoot.transform, "ItemSlot" + i, null);

            var capGo = FindOrCreate("CapacityText", root.transform);
            SetRect(capGo,
                anchorMin: new Vector2(0.72f, 0f), anchorMax: new Vector2(0.72f, 0f),
                pivot: new Vector2(0.5f, 0f),
                sizeDelta: new Vector2(160f, 32f), anchoredPos: new Vector2(0f, 20f));
            CreateText(capGo.transform, "Text", "0/120", font, 22, TextAnchor.MiddleCenter, Color.white);
        }

        private static void BuildSimpleSkillPanel(Transform panel, Font font)
        {
            ClearAllChildren(panel);

            var root = FindOrCreate("PanelRoot", panel);
            SetStretchFull(root);
            SetImage(root, LoadSprite(SprSkillBg), raycast: true);

            var titleGo = FindOrCreate("TitleText", root.transform);
            SetRect(titleGo,
                anchorMin: new Vector2(0.5f, 1f), anchorMax: new Vector2(0.5f, 1f),
                pivot: new Vector2(0.5f, 1f),
                sizeDelta: new Vector2(240f, 48f), anchoredPos: new Vector2(0f, -20f));
            CreateText(titleGo.transform, "Text", "技能", font, 32, TextAnchor.MiddleCenter, Color.white);

            CreateSpriteSwapActionButton(root.transform, "BtnCloseSkill",
                LoadSprite(SprBtnClose1), LoadSprite(SprBtnClose2),
                LoadSprite(SprBtnClose3), LoadSprite(SprBtnClose4),
                new Vector2(1f, 1f), new Vector2(1f, 1f), new Vector2(1f, 1f),
                new Vector2(48f, 48f), new Vector2(-16f, -16f));

            var row = FindOrCreate("SkillButtonRow", root.transform);
            SetRect(row,
                anchorMin: new Vector2(0.5f, 0.5f), anchorMax: new Vector2(0.5f, 0.5f),
                pivot: new Vector2(0.5f, 0.5f),
                sizeDelta: new Vector2(720f, 160f), anchoredPos: Vector2.zero);

            var hlg = row.GetComponent<HorizontalLayoutGroup>();
            if (hlg == null) hlg = row.AddComponent<HorizontalLayoutGroup>();
            hlg.spacing = 16f;
            hlg.childAlignment = TextAnchor.MiddleCenter;
            hlg.childControlWidth = hlg.childControlHeight = false;

            foreach (var def in SkillButtonDefs)
                CreateSkillButton(row.transform, def.btnName, def.skillName, def.iconPath, def.frameSet, font);
        }

        private static void BuildSimpleSkillDetailPanel(Transform panel, Font font)
        {
            ClearAllChildren(panel);

            var root = FindOrCreate("PanelRoot", panel);
            SetStretchFull(root);
            SetImage(root, LoadSprite(SprDialogBg), raycast: true);

            CreateSpriteSwapActionButton(root.transform, "BtnCloseDetail",
                LoadSprite(SprBtnClose1), LoadSprite(SprBtnClose2),
                LoadSprite(SprBtnClose3), LoadSprite(SprBtnClose4),
                new Vector2(1f, 1f), new Vector2(1f, 1f), new Vector2(1f, 1f),
                new Vector2(44f, 44f), new Vector2(-12f, -12f));

            var nameGo = FindOrCreate("DetailTitleText", root.transform);
            SetRect(nameGo,
                anchorMin: new Vector2(0.5f, 0.65f), anchorMax: new Vector2(0.5f, 0.65f),
                pivot: new Vector2(0.5f, 0.5f),
                sizeDelta: new Vector2(440f, 44f), anchoredPos: Vector2.zero);
            CreateText(nameGo.transform, "Text", "技能名称", font, 28, TextAnchor.MiddleCenter,
                new Color(1f, 0.92f, 0.55f));

            var descGo = FindOrCreate("DetailDescText", root.transform);
            SetRect(descGo,
                anchorMin: new Vector2(0.5f, 0.35f), anchorMax: new Vector2(0.5f, 0.35f),
                pivot: new Vector2(0.5f, 0.5f),
                sizeDelta: new Vector2(440f, 120f), anchoredPos: Vector2.zero);
            var desc = CreateText(descGo.transform, "Text", "技能描述", font, 22,
                TextAnchor.MiddleCenter, Color.white);
            desc.horizontalOverflow = HorizontalWrapMode.Wrap;
        }

        // ── 背景层（旧版复杂 HUD 保留，不再被菜单调用）──
        private static void ApplyBackground(Transform canvas)
        {
            var bgMap = LoadSprite(SprBgMap);
            var bgDim = LoadSprite(SprBgDim);

            var bgImage = FindOrCreate("BgImage", canvas);
            SetStretchFull(bgImage);
            SetImage(bgImage, bgMap, raycast: false);
            var bgImg = bgImage.GetComponent<Image>();
            if (bgImg != null)
            {
                bgImg.type = Image.Type.Simple;
                bgImg.preserveAspect = false;
            }
            bgImage.transform.SetAsFirstSibling();

            var dimmer = FindOrCreate("BgDimmer", canvas);
            SetStretchFull(dimmer);
            SetImage(dimmer, bgDim, raycast: false);
            var dimImg = dimmer.GetComponent<Image>();
            if (dimImg != null)
                dimImg.color = new Color(1f, 1f, 1f, 0.35f);
            dimmer.transform.SetSiblingIndex(1);
        }

        // ── 顶部 HUD ──
        private static void BuildTopHud(Transform canvas, Font font)
        {
            var hudRoot = FindOrCreate("TopHudBar", canvas);
            SetRect(hudRoot, anchorMin: new Vector2(0f, 1f), anchorMax: new Vector2(1f, 1f),
                pivot: new Vector2(0.5f, 1f), sizeDelta: new Vector2(0f, 110f), anchoredPos: Vector2.zero);
            SetImage(hudRoot, null, raycast: false);
            hudRoot.GetComponent<Image>().color = Color.clear;

            BuildHudLeft(hudRoot.transform, font);
            BuildHudRight(hudRoot.transform, font);

            hudRoot.transform.SetSiblingIndex(2);
        }

        private static void BuildHudLeft(Transform parent, Font font)
        {
            var leftRoot = FindOrCreate("HudLeft", parent);
            SetRect(leftRoot,
                anchorMin: new Vector2(0f, 0.5f), anchorMax: new Vector2(0f, 0.5f),
                pivot: new Vector2(0f, 0.5f),
                sizeDelta: new Vector2(560f, 100f),
                anchoredPos: new Vector2(24f, -8f));
            SetImage(leftRoot, null, raycast: false);
            leftRoot.GetComponent<Image>().color = Color.clear;

            // 头像框
            var avatarFrame = FindOrCreate("AvatarFrame", leftRoot.transform);
            SetRect(avatarFrame,
                anchorMin: new Vector2(0f, 0.5f), anchorMax: new Vector2(0f, 0.5f),
                pivot: new Vector2(0f, 0.5f),
                sizeDelta: new Vector2(88f, 88f),
                anchoredPos: new Vector2(0f, 0f));
            SetImage(avatarFrame, LoadSprite(SprAvatarFrame), raycast: false);

            // 等级角标
            var levelBadge = FindOrCreate("LevelBadge", avatarFrame.transform);
            SetRect(levelBadge,
                anchorMin: new Vector2(1f, 0f), anchorMax: new Vector2(1f, 0f),
                pivot: new Vector2(1f, 0f),
                sizeDelta: new Vector2(32f, 32f),
                anchoredPos: new Vector2(4f, -4f));
            SetImage(levelBadge, LoadSprite(SprInfoBg), raycast: false);
            CreateText(levelBadge.transform, "LevelText", "2", font, 20, TextAnchor.MiddleCenter,
                new Color(1f, 0.92f, 0.4f));

            // 角色名
            var nameText = FindOrCreate("PlayerNameText", leftRoot.transform);
            SetRect(nameText,
                anchorMin: new Vector2(0f, 1f), anchorMax: new Vector2(1f, 1f),
                pivot: new Vector2(0f, 1f),
                sizeDelta: new Vector2(-100f, 36f),
                anchoredPos: new Vector2(100f, -4f));
            CreateText(nameText.transform, "Text", "lixiaotong", font, 28, TextAnchor.MiddleLeft, Color.white);

            // 体力条
            BuildStatBar(leftRoot.transform, "StaminaBar", "体力", "92/100",
                LoadSprite(SprStaminaLabel), LoadSprite(SprBarFillGreen),
                new Vector2(100f, -42f), font, 0.92f);

            // 历练条
            BuildStatBar(leftRoot.transform, "TrainingBar", "历练", "50/50",
                null, LoadSprite(SprBarFillYellow),
                new Vector2(100f, -78f), font, 0.83f);
        }

        private static void BuildStatBar(Transform parent, string barName, string label, string valueText,
            Sprite labelSprite, Sprite fillSprite, Vector2 pos, Font font, float fillAmount)
        {
            var barRoot = FindOrCreate(barName, parent);
            SetRect(barRoot,
                anchorMin: new Vector2(0f, 1f), anchorMax: new Vector2(1f, 1f),
                pivot: new Vector2(0f, 1f),
                sizeDelta: new Vector2(-100f, 32f),
                anchoredPos: pos);

            // 标签（体力有专用图，历练用文字）
            if (labelSprite != null)
            {
                var labelGo = FindOrCreate("Label", barRoot.transform);
                SetRect(labelGo,
                    anchorMin: new Vector2(0f, 0.5f), anchorMax: new Vector2(0f, 0.5f),
                    pivot: new Vector2(0f, 0.5f),
                    sizeDelta: new Vector2(48f, 28f),
                    anchoredPos: Vector2.zero);
                SetImage(labelGo, labelSprite, raycast: false);
            }
            else
            {
                var labelGo = FindOrCreate("LabelText", barRoot.transform);
                SetRect(labelGo,
                    anchorMin: new Vector2(0f, 0.5f), anchorMax: new Vector2(0f, 0.5f),
                    pivot: new Vector2(0f, 0.5f),
                    sizeDelta: new Vector2(48f, 28f),
                    anchoredPos: Vector2.zero);
                CreateText(labelGo.transform, "Text", label, font, 22, TextAnchor.MiddleLeft,
                    new Color(1f, 0.85f, 0.3f));
            }

            // 进度条底
            var barBg = FindOrCreate("BarBg", barRoot.transform);
            SetRect(barBg,
                anchorMin: new Vector2(0f, 0.5f), anchorMax: new Vector2(1f, 0.5f),
                pivot: new Vector2(0f, 0.5f),
                sizeDelta: new Vector2(-130f, 24f),
                anchoredPos: new Vector2(52f, 0f));
            SetImage(barBg, LoadSprite(SprBarBg), raycast: false);
            barBg.GetComponent<Image>().type = Image.Type.Sliced;

            // 进度条填充
            var barFill = FindOrCreate("BarFill", barBg.transform);
            SetStretchFull(barFill);
            var fillImg = SetImage(barFill, fillSprite, raycast: false);
            fillImg.type = Image.Type.Filled;
            fillImg.fillMethod = Image.FillMethod.Horizontal;
            fillImg.fillOrigin = (int)Image.OriginHorizontal.Left;
            fillImg.fillAmount = fillAmount;

            // 数值文字
            var valueGo = FindOrCreate("ValueText", barRoot.transform);
            SetRect(valueGo,
                anchorMin: new Vector2(1f, 0.5f), anchorMax: new Vector2(1f, 0.5f),
                pivot: new Vector2(1f, 0.5f),
                sizeDelta: new Vector2(80f, 28f),
                anchoredPos: new Vector2(0f, 0f));
            CreateText(valueGo.transform, "Text", valueText, font, 20, TextAnchor.MiddleRight, Color.white);
        }

        private static void BuildHudRight(Transform parent, Font font)
        {
            var rightRoot = FindOrCreate("HudRight", parent);
            SetRect(rightRoot,
                anchorMin: new Vector2(1f, 0.5f), anchorMax: new Vector2(1f, 0.5f),
                pivot: new Vector2(1f, 0.5f),
                sizeDelta: new Vector2(420f, 100f),
                anchoredPos: new Vector2(-24f, -8f));
            SetImage(rightRoot, null, raycast: false);
            rightRoot.GetComponent<Image>().color = Color.clear;

            BuildCurrencyRow(rightRoot.transform, "GoldRow", LoadSprite(SprGoldIcon),
                "156184", new Vector2(0f, 12f), font);
            BuildCurrencyRow(rightRoot.transform, "DiamondRow", LoadSprite(SprDiamondIcon),
                "156184", new Vector2(0f, -38f), font);
        }

        private static void BuildCurrencyRow(Transform parent, string rowName, Sprite icon, string amount,
            Vector2 pos, Font font)
        {
            var row = FindOrCreate(rowName, parent);
            SetRect(row,
                anchorMin: new Vector2(1f, 0.5f), anchorMax: new Vector2(1f, 0.5f),
                pivot: new Vector2(1f, 0.5f),
                sizeDelta: new Vector2(320f, 44f),
                anchoredPos: pos);

            // 底条
            var bg = FindOrCreate("Bg", row.transform);
            SetStretchFull(bg);
            SetImage(bg, LoadSprite(SprCurrencyBg), raycast: false);
            bg.GetComponent<Image>().type = Image.Type.Sliced;

            // 图标
            var iconGo = FindOrCreate("Icon", row.transform);
            SetRect(iconGo,
                anchorMin: new Vector2(0f, 0.5f), anchorMax: new Vector2(0f, 0.5f),
                pivot: new Vector2(0f, 0.5f),
                sizeDelta: new Vector2(40f, 40f),
                anchoredPos: new Vector2(8f, 0f));
            SetImage(iconGo, icon, raycast: false);

            // 数量
            var amountGo = FindOrCreate("AmountText", row.transform);
            SetRect(amountGo,
                anchorMin: new Vector2(0f, 0f), anchorMax: new Vector2(1f, 1f),
                pivot: new Vector2(0.5f, 0.5f),
                sizeDelta: new Vector2(-100f, 0f),
                anchoredPos: new Vector2(20f, 0f));
            CreateText(amountGo.transform, "Text", amount, font, 24, TextAnchor.MiddleCenter, Color.white);

            // 加号按钮（Sprite Swap 四态）
            var plusBtn = FindOrCreate("BtnPlus", row.transform);
            SetRect(plusBtn,
                anchorMin: new Vector2(1f, 0.5f), anchorMax: new Vector2(1f, 0.5f),
                pivot: new Vector2(1f, 0.5f),
                sizeDelta: new Vector2(44f, 44f),
                anchoredPos: new Vector2(-4f, 0f));
            SetupSpriteSwapButton(plusBtn,
                LoadSprite(SprBtnPlus1), LoadSprite(SprBtnPlus2),
                LoadSprite(SprBtnPlus3), LoadSprite(SprBtnPlus4));
        }

        // ── 底部导航栏 ──
        private static void BuildBottomNavBar(Transform canvas)
        {
            var navBar = FindOrCreate("BottomBar", canvas);
            // 右下角排列，与效果图一致
            SetRect(navBar,
                anchorMin: new Vector2(1f, 0f), anchorMax: new Vector2(1f, 0f),
                pivot: new Vector2(1f, 0f),
                sizeDelta: new Vector2(740f, 120f),
                anchoredPos: new Vector2(-20f, 20f));
            SetImage(navBar, null, raycast: false);
            navBar.GetComponent<Image>().color = Color.clear;

            var hlg = navBar.GetComponent<HorizontalLayoutGroup>();
            if (hlg == null) hlg = navBar.AddComponent<HorizontalLayoutGroup>();
            hlg.spacing = 16f;
            hlg.childAlignment = TextAnchor.MiddleRight;
            hlg.childControlWidth = false;
            hlg.childControlHeight = false;
            hlg.childForceExpandWidth = false;
            hlg.childForceExpandHeight = false;
            hlg.padding = new RectOffset(0, 0, 4, 4);

            var validNames = new System.Collections.Generic.HashSet<string>();
            foreach (var (btnName, spritePath) in BottomMenuItems)
            {
                validNames.Add(btnName);
                CreateOrUpdateMenuButton(navBar.transform, btnName, LoadSprite(spritePath));
            }

            RemoveExtraChildren(navBar.transform, validNames);
            navBar.transform.SetAsLastSibling();
        }

        // ── 背包面板 ──
        private static GameObject EnsureBackpackPanel(Transform canvas)
        {
            var existing = canvas.Find("BackpackPanel");
            GameObject panel;

            if (existing != null)
            {
                panel = existing.gameObject;
                if (PrefabUtility.IsPartOfPrefabInstance(panel))
                {
                    PrefabUtility.UnpackPrefabInstance(panel, PrefabUnpackMode.Completely,
                        InteractionMode.AutomatedAction);
                }
            }
            else
            {
                panel = new GameObject("BackpackPanel", typeof(RectTransform));
                panel.transform.SetParent(canvas, false);
                panel.layer = LayerMask.NameToLayer("UI");
            }

            NormalizePanelRoot(panel);
            return panel;
        }

        private static void NormalizePanelRoot(GameObject panel)
        {
            foreach (var c in panel.GetComponents<Canvas>()) Object.DestroyImmediate(c);
            foreach (var s in panel.GetComponents<CanvasScaler>()) Object.DestroyImmediate(s);
            foreach (var g in panel.GetComponents<GraphicRaycaster>()) Object.DestroyImmediate(g);

            SetRect(panel,
                anchorMin: new Vector2(0.5f, 0.5f), anchorMax: new Vector2(0.5f, 0.5f),
                pivot: new Vector2(0.5f, 0.5f),
                sizeDelta: new Vector2(1050f, 760f),
                anchoredPos: Vector2.zero);
            panel.SetActive(false);
        }

        private static void BuildBackpackPanelContent(Transform panel, Font font)
        {
            ClearAllChildren(panel);

            var root = FindOrCreate("PanelRoot", panel);
            SetStretchFull(root);
            var rootImg = SetImage(root, LoadSprite(SprBackpackBg), raycast: true);
            rootImg.type = Image.Type.Sliced;

            BuildCharacterSection(root.transform, font);
            BuildInventorySection(root.transform, font);
        }

        private static void BuildCharacterSection(Transform parent, Font font)
        {
            var section = FindOrCreate("CharacterSection", parent);
            SetRect(section,
                anchorMin: new Vector2(0f, 0f), anchorMax: new Vector2(0.44f, 1f),
                pivot: new Vector2(0f, 0.5f),
                sizeDelta: Vector2.zero,
                anchoredPos: Vector2.zero);
            SetImage(section, LoadSprite(SprCharacterBg), raycast: false);
            section.GetComponent<Image>().type = Image.Type.Sliced;

            // 角色名
            var nameGo = FindOrCreate("CharNameText", section.transform);
            SetRect(nameGo,
                anchorMin: new Vector2(0.5f, 1f), anchorMax: new Vector2(0.5f, 1f),
                pivot: new Vector2(0.5f, 1f),
                sizeDelta: new Vector2(300f, 44f),
                anchoredPos: new Vector2(0f, -18f));
            CreateText(nameGo.transform, "Text", "lixiaotong", font, 30, TextAnchor.MiddleCenter, Color.white);

            // 立绘
            var portrait = FindOrCreate("CharacterPortrait", section.transform);
            SetRect(portrait,
                anchorMin: new Vector2(0.5f, 0.5f), anchorMax: new Vector2(0.5f, 0.5f),
                pivot: new Vector2(0.5f, 0.5f),
                sizeDelta: new Vector2(220f, 280f),
                anchoredPos: new Vector2(0f, 20f));
            var portraitImg = SetImage(portrait, LoadSprite(SprCharacterPortrait), raycast: false);
            portraitImg.preserveAspect = true;

            // 左列装备
            BuildEquipColumn(section.transform, "EquipColLeft", true, LeftEquipPaths);
            // 右列装备
            BuildEquipColumn(section.transform, "EquipColRight", false, RightEquipPaths);

            // 详细属性
            CreateSpriteSwapActionButton(section.transform, "BtnDetailAttr",
                LoadSprite(SprBtnDetail1), LoadSprite(SprBtnDetail2),
                LoadSprite(SprBtnDetail3), LoadSprite(SprBtnDetail4),
                new Vector2(0.5f, 0f), new Vector2(0.5f, 0f), new Vector2(0.5f, 0f),
                new Vector2(200f, 56f), new Vector2(0f, 72f));

            // 生命 / 伤害
            BuildStatField(section.transform, "StatLife", "生命", "3130",
                new Vector2(0.08f, 0.12f), font);
            BuildStatField(section.transform, "StatDamage", "伤害", "160",
                new Vector2(0.55f, 0.12f), font);

            // 经验条
            BuildExpBar(section.transform, font);
        }

        private static void BuildEquipColumn(Transform parent, string colName, bool isLeft, string[] equipPaths)
        {
            var col = FindOrCreate(colName, parent);
            float x = isLeft ? 0.06f : 0.78f;
            SetRect(col,
                anchorMin: new Vector2(x, 0.25f), anchorMax: new Vector2(x, 0.25f),
                pivot: new Vector2(0.5f, 0.5f),
                sizeDelta: new Vector2(72f, 320f),
                anchoredPos: Vector2.zero);

            var vlg = col.GetComponent<VerticalLayoutGroup>();
            if (vlg == null) vlg = col.AddComponent<VerticalLayoutGroup>();
            vlg.spacing = 8f;
            vlg.childAlignment = TextAnchor.MiddleCenter;
            vlg.childControlWidth = vlg.childControlHeight = false;
            vlg.childForceExpandWidth = vlg.childForceExpandHeight = false;

            for (int i = 0; i < equipPaths.Length; i++)
                CreateEquipSlot(col.transform, "EquipSlot" + i, LoadSprite(equipPaths[i]));
        }

        private static void CreateEquipSlot(Transform parent, string name, Sprite icon)
        {
            var slot = FindOrCreate(name, parent);
            var le = slot.GetComponent<LayoutElement>();
            if (le == null) le = slot.AddComponent<LayoutElement>();
            le.preferredWidth = le.preferredHeight = 68f;

            SetImage(slot, LoadSprite(SprItemSlot), raycast: false);
            slot.GetComponent<Image>().type = Image.Type.Sliced;

            if (icon != null)
            {
                var iconGo = FindOrCreate("Icon", slot.transform);
                SetStretchFull(iconGo);
                var iconRt = iconGo.GetComponent<RectTransform>();
                iconRt.offsetMin = new Vector2(6f, 6f);
                iconRt.offsetMax = new Vector2(-6f, -6f);
                var iconImg = SetImage(iconGo, icon, raycast: false);
                iconImg.preserveAspect = true;
            }
        }

        private static void BuildStatField(Transform parent, string name, string label, string value,
            Vector2 anchorX, Font font)
        {
            var field = FindOrCreate(name, parent);
            SetRect(field,
                anchorMin: anchorX, anchorMax: anchorX,
                pivot: new Vector2(0f, 0f),
                sizeDelta: new Vector2(180f, 48f),
                anchoredPos: new Vector2(0f, 24f));
            SetImage(field, LoadSprite(SprStatLifeBg), raycast: false);
            field.GetComponent<Image>().type = Image.Type.Sliced;

            var labelGo = FindOrCreate("Label", field.transform);
            SetRect(labelGo,
                anchorMin: new Vector2(0f, 0f), anchorMax: new Vector2(0.4f, 1f),
                pivot: new Vector2(0f, 0.5f), sizeDelta: Vector2.zero, anchoredPos: Vector2.zero);
            CreateText(labelGo.transform, "Text", label, font, 22, TextAnchor.MiddleCenter,
                new Color(1f, 0.9f, 0.5f));

            var valueGo = FindOrCreate("Value", field.transform);
            SetRect(valueGo,
                anchorMin: new Vector2(0.4f, 0f), anchorMax: new Vector2(1f, 1f),
                pivot: new Vector2(0.5f, 0.5f), sizeDelta: Vector2.zero, anchoredPos: Vector2.zero);
            CreateText(valueGo.transform, "Text", value, font, 24, TextAnchor.MiddleCenter, Color.white);
        }

        private static void BuildExpBar(Transform parent, Font font)
        {
            var barRoot = FindOrCreate("ExpBar", parent);
            SetRect(barRoot,
                anchorMin: new Vector2(0.5f, 0f), anchorMax: new Vector2(0.5f, 0f),
                pivot: new Vector2(0.5f, 0f),
                sizeDelta: new Vector2(420f, 36f),
                anchoredPos: new Vector2(0f, 8f));

            var labelGo = FindOrCreate("Label", barRoot.transform);
            SetRect(labelGo,
                anchorMin: new Vector2(0f, 0.5f), anchorMax: new Vector2(0f, 0.5f),
                pivot: new Vector2(0f, 0.5f),
                sizeDelta: new Vector2(48f, 28f), anchoredPos: Vector2.zero);
            CreateText(labelGo.transform, "Text", "经验", font, 20, TextAnchor.MiddleLeft,
                new Color(1f, 0.85f, 0.3f));

            var barBg = FindOrCreate("BarBg", barRoot.transform);
            SetRect(barBg,
                anchorMin: new Vector2(0f, 0.5f), anchorMax: new Vector2(1f, 0.5f),
                pivot: new Vector2(0f, 0.5f),
                sizeDelta: new Vector2(-110f, 22f), anchoredPos: new Vector2(52f, 0f));
            SetImage(barBg, LoadSprite(SprExpBarBg), raycast: false);

            var barFill = FindOrCreate("BarFill", barBg.transform);
            SetStretchFull(barFill);
            var fillImg = SetImage(barFill, LoadSprite(SprExpBarFill), raycast: false);
            fillImg.type = Image.Type.Filled;
            fillImg.fillMethod = Image.FillMethod.Horizontal;
            fillImg.fillOrigin = (int)Image.OriginHorizontal.Left;
            fillImg.fillAmount = 312f / 360f;

            var valueGo = FindOrCreate("ValueText", barRoot.transform);
            SetRect(valueGo,
                anchorMin: new Vector2(1f, 0.5f), anchorMax: new Vector2(1f, 0.5f),
                pivot: new Vector2(1f, 0.5f),
                sizeDelta: new Vector2(60f, 28f), anchoredPos: Vector2.zero);
            CreateText(valueGo.transform, "Text", "312/360", font, 18, TextAnchor.MiddleRight, Color.white);
        }

        private static void BuildInventorySection(Transform parent, Font font)
        {
            var section = FindOrCreate("InventorySection", parent);
            SetRect(section,
                anchorMin: new Vector2(0.44f, 0f), anchorMax: new Vector2(1f, 1f),
                pivot: new Vector2(1f, 0.5f),
                sizeDelta: Vector2.zero,
                anchoredPos: Vector2.zero);
            SetImage(section, null, raycast: false);
            section.GetComponent<Image>().color = Color.clear;

            // 关闭按钮（保留名 BtnCloseBackpack 供 UIManager 接线）
            CreateSpriteSwapActionButton(section.transform, "BtnCloseBackpack",
                LoadSprite(SprBtnClose1), LoadSprite(SprBtnClose2),
                LoadSprite(SprBtnClose3), LoadSprite(SprBtnClose4),
                new Vector2(1f, 1f), new Vector2(1f, 1f), new Vector2(1f, 1f),
                new Vector2(52f, 52f), new Vector2(-16f, -16f));

            // 4×5 格子区
            var gridRoot = FindOrCreate("InventoryGrid", section.transform);
            SetRect(gridRoot,
                anchorMin: new Vector2(0.5f, 0.5f), anchorMax: new Vector2(0.5f, 0.5f),
                pivot: new Vector2(0.5f, 0.5f),
                sizeDelta: new Vector2(560f, 560f),
                anchoredPos: new Vector2(0f, 20f));

            var grid = gridRoot.GetComponent<GridLayoutGroup>();
            if (grid == null) grid = gridRoot.AddComponent<GridLayoutGroup>();
            grid.cellSize = new Vector2(120f, 120f);
            grid.spacing = new Vector2(10f, 10f);
            grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            grid.constraintCount = 4;
            grid.childAlignment = TextAnchor.UpperCenter;

            var sampleItem = LoadSprite(SprSampleItem);
            for (int i = 0; i < 20; i++)
                CreateInventorySlot(gridRoot.transform, "ItemSlot" + i, i < 3 ? sampleItem : null);

            // 容量
            var capGo = FindOrCreate("CapacityText", section.transform);
            SetRect(capGo,
                anchorMin: new Vector2(0.5f, 0f), anchorMax: new Vector2(0.5f, 0f),
                pivot: new Vector2(0.5f, 0f),
                sizeDelta: new Vector2(200f, 32f),
                anchoredPos: new Vector2(0f, 88f));
            CreateText(capGo.transform, "Text", "0/120", font, 22, TextAnchor.MiddleCenter,
                new Color(0.85f, 0.85f, 0.85f));

            // 底部操作栏
            var actionBar = FindOrCreate("ActionBar", section.transform);
            SetRect(actionBar,
                anchorMin: new Vector2(0.5f, 0f), anchorMax: new Vector2(0.5f, 0f),
                pivot: new Vector2(0.5f, 0f),
                sizeDelta: new Vector2(520f, 64f),
                anchoredPos: new Vector2(0f, 20f));

            CreateSpriteSwapActionButton(actionBar.transform, "BtnSell",
                LoadSprite(SprBtnSell1), LoadSprite(SprBtnSell2),
                LoadSprite(SprBtnSell3), LoadSprite(SprBtnSell4),
                new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), new Vector2(0f, 0.5f),
                new Vector2(160f, 56f), new Vector2(80f, 0f));

            CreateSpriteSwapActionButton(actionBar.transform, "BtnSort",
                LoadSprite(SprBtnSort1), LoadSprite(SprBtnSort2),
                LoadSprite(SprBtnSort3), LoadSprite(SprBtnSort4),
                new Vector2(1f, 0.5f), new Vector2(1f, 0.5f), new Vector2(1f, 0.5f),
                new Vector2(200f, 56f), new Vector2(-100f, 0f));
        }

        private static void CreateInventorySlot(Transform parent, string name, Sprite itemIcon)
        {
            var slot = FindOrCreate(name, parent);
            SetImage(slot, LoadSprite(SprItemSlot), raycast: true);
            slot.GetComponent<Image>().type = Image.Type.Sliced;

            if (itemIcon != null)
            {
                var iconGo = FindOrCreate("Icon", slot.transform);
                SetStretchFull(iconGo);
                var iconRt = iconGo.GetComponent<RectTransform>();
                iconRt.offsetMin = new Vector2(8f, 8f);
                iconRt.offsetMax = new Vector2(-8f, -8f);
                var iconImg = SetImage(iconGo, itemIcon, raycast: false);
                iconImg.preserveAspect = true;
            }
        }

        private static GameObject CreateSpriteSwapActionButton(Transform parent, string btnName,
            Sprite n, Sprite h, Sprite p, Sprite d,
            Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot,
            Vector2 size, Vector2 pos)
        {
            var btnGo = FindOrCreate(btnName, parent);
            SetRect(btnGo, anchorMin, anchorMax, pivot, size, pos);
            SetupSpriteSwapButton(btnGo, n, h, p, d);
            return btnGo;
        }

        private static void ClearAllChildren(Transform parent)
        {
            for (int i = parent.childCount - 1; i >= 0; i--)
                Object.DestroyImmediate(parent.GetChild(i).gameObject);
        }

        // ── 技能面板 ──
        private static GameObject EnsureSkillPanel(Transform canvas)
        {
            var existing = canvas.Find("SkillPanel");
            GameObject panel;

            if (existing != null)
                panel = existing.gameObject;
            else
            {
                panel = new GameObject("SkillPanel", typeof(RectTransform));
                panel.transform.SetParent(canvas, false);
                panel.layer = LayerMask.NameToLayer("UI");
            }

            SetRect(panel,
                anchorMin: new Vector2(0.5f, 0.5f), anchorMax: new Vector2(0.5f, 0.5f),
                pivot: new Vector2(0.5f, 0.5f),
                sizeDelta: new Vector2(960f, 680f),
                anchoredPos: Vector2.zero);
            panel.SetActive(false);
            return panel;
        }

        private static void BuildSkillPanelContent(Transform panel, Font font)
        {
            ClearAllChildren(panel);

            // 面板遮罩
            var dimmer = FindOrCreate("Dimmer", panel);
            SetStretchFull(dimmer);
            var dimImg = SetImage(dimmer, null, raycast: true);
            dimImg.color = new Color(0f, 0f, 0f, 0.55f);

            // 主背景
            var root = FindOrCreate("PanelRoot", panel);
            SetStretchFull(root);
            var rootImg = SetImage(root, LoadSprite(SprSkillBg), raycast: true);
            rootImg.type = Image.Type.Sliced;

            // 标题
            var titleGo = FindOrCreate("TitleText", root.transform);
            SetRect(titleGo,
                anchorMin: new Vector2(0.5f, 1f), anchorMax: new Vector2(0.5f, 1f),
                pivot: new Vector2(0.5f, 1f),
                sizeDelta: new Vector2(400f, 56f),
                anchoredPos: new Vector2(0f, -24f));
            CreateText(titleGo.transform, "Text", "技能", font, 36, TextAnchor.MiddleCenter,
                new Color(1f, 0.92f, 0.55f));

            // 关闭（保留 BtnCloseSkill 供 UIManager 接线）
            CreateSpriteSwapActionButton(root.transform, "BtnCloseSkill",
                LoadSprite(SprBtnClose1), LoadSprite(SprBtnClose2),
                LoadSprite(SprBtnClose3), LoadSprite(SprBtnClose4),
                new Vector2(1f, 1f), new Vector2(1f, 1f), new Vector2(1f, 1f),
                new Vector2(52f, 52f), new Vector2(-20f, -20f));

            // 4 技能图标行
            BuildSkillButtonRow(root.transform, font);

            // 底部技能说明区
            BuildSkillInfoArea(root.transform, font);
        }

        private static void BuildSkillButtonRow(Transform parent, Font font)
        {
            var row = FindOrCreate("SkillButtonRow", parent);
            SetRect(row,
                anchorMin: new Vector2(0.5f, 0.5f), anchorMax: new Vector2(0.5f, 0.5f),
                pivot: new Vector2(0.5f, 0.5f),
                sizeDelta: new Vector2(780f, 180f),
                anchoredPos: new Vector2(0f, 20f));

            var hlg = row.GetComponent<HorizontalLayoutGroup>();
            if (hlg == null) hlg = row.AddComponent<HorizontalLayoutGroup>();
            hlg.spacing = 24f;
            hlg.childAlignment = TextAnchor.MiddleCenter;
            hlg.childControlWidth = hlg.childControlHeight = false;
            hlg.childForceExpandWidth = hlg.childForceExpandHeight = false;

            foreach (var def in SkillButtonDefs)
                CreateSkillButton(row.transform, def.btnName, def.skillName, def.iconPath, def.frameSet, font);
        }

        private static void CreateSkillButton(Transform parent, string btnName, string skillName,
            string iconPath, int frameSet, Font font)
        {
            var btnRoot = FindOrCreate(btnName, parent);
            var le = btnRoot.GetComponent<LayoutElement>();
            if (le == null) le = btnRoot.AddComponent<LayoutElement>();
            le.preferredWidth = 160f;
            le.preferredHeight = 170f;

            var rt = btnRoot.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(160f, 170f);

            LoadSkillBtnSprites(frameSet, out Sprite n, out Sprite h, out Sprite p, out Sprite d);
            SetupSpriteSwapButton(btnRoot, n, h, p, d);
            btnRoot.GetComponent<Image>().preserveAspect = true;

            // 技能图标
            var iconGo = FindOrCreate("SkillIcon", btnRoot.transform);
            SetRect(iconGo,
                anchorMin: new Vector2(0.5f, 0.5f), anchorMax: new Vector2(0.5f, 0.5f),
                pivot: new Vector2(0.5f, 0.5f),
                sizeDelta: new Vector2(90f, 90f),
                anchoredPos: new Vector2(0f, 10f));
            var iconImg = SetImage(iconGo, LoadSprite(iconPath), raycast: false);
            iconImg.preserveAspect = true;

            // 技能名
            var nameGo = FindOrCreate("SkillNameText", btnRoot.transform);
            SetRect(nameGo,
                anchorMin: new Vector2(0.5f, 0f), anchorMax: new Vector2(0.5f, 0f),
                pivot: new Vector2(0.5f, 0f),
                sizeDelta: new Vector2(140f, 32f),
                anchoredPos: new Vector2(0f, 4f));
            CreateText(nameGo.transform, "Text", skillName, font, 22, TextAnchor.MiddleCenter, Color.white);
        }

        private static void LoadSkillBtnSprites(int setIndex, out Sprite n, out Sprite h, out Sprite p, out Sprite d)
        {
            string prefix = $"Assets/Images/003-taskother/btn_技能图标{setIndex}_";
            n = LoadSprite(prefix + "1.png");
            h = LoadSprite(prefix + "2.png");
            p = LoadSprite(prefix + "3.png");
            d = LoadSprite(prefix + "4.png");
        }

        private static Sprite LoadSkillBtnSpritesDisplay(int setIndex)
        {
            return LoadSprite($"Assets/Images/003-taskother/btn_技能图标{setIndex}_1.png");
        }

        private static void BuildSkillInfoArea(Transform parent, Font font)
        {
            var infoArea = FindOrCreate("SkillInfoArea", parent);
            SetRect(infoArea,
                anchorMin: new Vector2(0.5f, 0f), anchorMax: new Vector2(0.5f, 0f),
                pivot: new Vector2(0.5f, 0f),
                sizeDelta: new Vector2(720f, 120f),
                anchoredPos: new Vector2(0f, 36f));

            var bg = FindOrCreate("InfoBg", infoArea.transform);
            SetStretchFull(bg);
            SetImage(bg, LoadSprite(SprInfoBg), raycast: false);
            bg.GetComponent<Image>().type = Image.Type.Sliced;

            var nameGo = FindOrCreate("SelectedSkillName", infoArea.transform);
            SetRect(nameGo,
                anchorMin: new Vector2(0f, 0.55f), anchorMax: new Vector2(1f, 1f),
                pivot: new Vector2(0.5f, 0.5f),
                sizeDelta: new Vector2(-40f, 0f), anchoredPos: Vector2.zero);
            CreateText(nameGo.transform, "Text", "技能名称", font, 28, TextAnchor.MiddleLeft,
                new Color(1f, 0.92f, 0.55f));

            var descGo = FindOrCreate("SelectedSkillDesc", infoArea.transform);
            SetRect(descGo,
                anchorMin: new Vector2(0f, 0f), anchorMax: new Vector2(1f, 0.5f),
                pivot: new Vector2(0.5f, 0.5f),
                sizeDelta: new Vector2(-40f, 0f), anchoredPos: Vector2.zero);
            CreateText(descGo.transform, "Text", "技能描述：选中技能后在此展示基本信息。",
                font, 22, TextAnchor.MiddleLeft, new Color(0.85f, 0.85f, 0.85f));
        }

        // ── 详情弹窗 ──
        private static GameObject EnsureDetailPanel(Transform canvas, string panelName, float w, float h)
        {
            var existing = canvas.Find(panelName);
            GameObject panel;

            if (existing != null)
                panel = existing.gameObject;
            else
            {
                panel = new GameObject(panelName, typeof(RectTransform));
                panel.transform.SetParent(canvas, false);
                panel.layer = LayerMask.NameToLayer("UI");
            }

            SetRect(panel,
                anchorMin: new Vector2(0.5f, 0.5f), anchorMax: new Vector2(0.5f, 0.5f),
                pivot: new Vector2(0.5f, 0.5f),
                sizeDelta: new Vector2(w, h),
                anchoredPos: Vector2.zero);
            panel.SetActive(false);
            return panel;
        }

        private static void BuildEquipDetailPanel(Transform canvas, Font font)
        {
            var panel = EnsureDetailPanel(canvas, "EquipDetailPanel", 480f, 680f);
            var panelRt = panel.GetComponent<RectTransform>();
            panelRt.anchoredPosition = new Vector2(-300f, 0f);

            ClearAllChildren(panel.transform);

            var root = FindOrCreate("PanelRoot", panel.transform);
            SetStretchFull(root);
            SetImage(root, LoadSprite(SprEquipDetailBg), raycast: true).type = Image.Type.Sliced;

            // 标题行
            CreateDetailHeader(root.transform, font, "DetailItemName", "舒适铠甲",
                "DetailCategory", "衣服", new Vector2(0f, -28f));

            CreateSpriteSwapActionButton(root.transform, "BtnCloseEquip",
                LoadSprite(SprBtnClose1), LoadSprite(SprBtnClose2),
                LoadSprite(SprBtnClose3), LoadSprite(SprBtnClose4),
                new Vector2(1f, 1f), new Vector2(1f, 1f), new Vector2(1f, 1f),
                new Vector2(48f, 48f), new Vector2(-16f, -16f));

            // 物品图标区
            var iconArea = FindOrCreate("ItemIconArea", root.transform);
            SetRect(iconArea,
                anchorMin: new Vector2(0.5f, 0.5f), anchorMax: new Vector2(0.5f, 0.5f),
                pivot: new Vector2(0.5f, 0.5f),
                sizeDelta: new Vector2(120f, 120f), anchoredPos: new Vector2(0f, 80f));
            SetImage(iconArea, LoadSprite(SprItemSlot), raycast: false);

            var icon = FindOrCreate("ItemIcon", iconArea.transform);
            SetStretchFull(icon);
            var iconRt = icon.GetComponent<RectTransform>();
            iconRt.offsetMin = new Vector2(8f, 8f);
            iconRt.offsetMax = new Vector2(-8f, -8f);
            SetImage(icon, LoadSprite(SprSampleArmor), raycast: false).preserveAspect = true;

            var badge = FindOrCreate("EquippedBadge", iconArea.transform);
            SetRect(badge,
                anchorMin: new Vector2(0f, 1f), anchorMax: new Vector2(0f, 1f),
                pivot: new Vector2(0f, 1f),
                sizeDelta: new Vector2(72f, 28f), anchoredPos: new Vector2(-4f, 4f));
            SetImage(badge, LoadSprite(SprEquippedBadge), raycast: false);

            // 星级 + 标签
            BuildEquipTags(root.transform);

            // 属性文本
            CreateDetailStatLine(root.transform, font, "StatQuality", "品质  5",
                new Vector2(0.5f, 0.52f), new Color(0.6f, 0.85f, 1f));
            CreateDetailStatLine(root.transform, font, "StatDamage", "伤害  16",
                new Vector2(0.5f, 0.46f), Color.white);
            CreateDetailStatLine(root.transform, font, "StatLife", "生命  140",
                new Vector2(0.5f, 0.40f), Color.white);
            CreateDetailStatLine(root.transform, font, "StatSpecial", "雷霆万钧  冷却降低3秒",
                new Vector2(0.5f, 0.34f), new Color(0.5f, 0.85f, 1f));

            // 战斗力
            var cpGo = FindOrCreate("CombatPowerText", root.transform);
            SetRect(cpGo,
                anchorMin: new Vector2(0.5f, 0.5f), anchorMax: new Vector2(0.5f, 0.5f),
                pivot: new Vector2(0.5f, 0.5f),
                sizeDelta: new Vector2(360f, 48f), anchoredPos: new Vector2(0f, -20f));
            CreateText(cpGo.transform, "Text", "战斗力  691", font, 30, TextAnchor.MiddleCenter,
                new Color(1f, 0.88f, 0.2f));

            // 底部按钮
            CreateSpriteSwapActionButton(root.transform, "BtnEquip",
                LoadSprite(SprBtnEquip1), LoadSprite(SprBtnEquip2),
                LoadSprite(SprBtnEquip3), LoadSprite(SprBtnEquip4),
                new Vector2(0.3f, 0f), new Vector2(0.3f, 0f), new Vector2(0.5f, 0f),
                new Vector2(180f, 56f), new Vector2(0f, 28f));

            CreateSpriteSwapActionButton(root.transform, "BtnUpgrade",
                LoadSprite(SprBtnEquip1), LoadSprite(SprBtnEquip2),
                LoadSprite(SprBtnEquip3), LoadSprite(SprBtnEquip4),
                new Vector2(0.7f, 0f), new Vector2(0.7f, 0f), new Vector2(0.5f, 0f),
                new Vector2(180f, 56f), new Vector2(0f, 28f));

            SetButtonLabel(root.transform, "BtnEquip", "装备", font);
            SetButtonLabel(root.transform, "BtnUpgrade", "升级", font);
        }

        private static void BuildItemDetailPanel(Transform canvas, Font font)
        {
            var panel = EnsureDetailPanel(canvas, "ItemDetailPanel", 520f, 420f);
            var panelRt = panel.GetComponent<RectTransform>();
            panelRt.anchoredPosition = new Vector2(280f, 40f);

            ClearAllChildren(panel.transform);

            var root = FindOrCreate("PanelRoot", panel.transform);
            SetStretchFull(root);
            SetImage(root, LoadSprite(SprDialogBg), raycast: true).type = Image.Type.Sliced;

            CreateDetailHeader(root.transform, font, "ItemNameText", "黄金宝箱",
                "ItemCategoryText", "宝箱", new Vector2(0f, -24f));

            CreateSpriteSwapActionButton(root.transform, "BtnCloseItem",
                LoadSprite(SprBtnClose1), LoadSprite(SprBtnClose2),
                LoadSprite(SprBtnClose3), LoadSprite(SprBtnClose4),
                new Vector2(1f, 1f), new Vector2(1f, 1f), new Vector2(1f, 1f),
                new Vector2(44f, 44f), new Vector2(-12f, -12f));

            var iconArea = FindOrCreate("ItemIconArea", root.transform);
            SetRect(iconArea,
                anchorMin: new Vector2(0f, 0.5f), anchorMax: new Vector2(0f, 0.5f),
                pivot: new Vector2(0f, 0.5f),
                sizeDelta: new Vector2(100f, 100f), anchoredPos: new Vector2(24f, 10f));
            SetImage(iconArea, LoadSprite(SprItemSlot), raycast: false);
            var chestIcon = FindOrCreate("Icon", iconArea.transform);
            SetStretchFull(chestIcon);
            var chestRt = chestIcon.GetComponent<RectTransform>();
            chestRt.offsetMin = new Vector2(6f, 6f);
            chestRt.offsetMax = new Vector2(-6f, -6f);
            SetImage(chestIcon, LoadSprite(SprChestIcon), raycast: false).preserveAspect = true;

            var descGo = FindOrCreate("ItemDescText", root.transform);
            SetRect(descGo,
                anchorMin: new Vector2(0f, 0.3f), anchorMax: new Vector2(1f, 0.72f),
                pivot: new Vector2(0.5f, 0.5f),
                sizeDelta: new Vector2(-140f, 0f), anchoredPos: new Vector2(50f, 0f));
            var descText = CreateText(descGo.transform, "Text",
                "从艾伦遗迹中寻得的宝箱，可能开出橙色符文、橙装碎片，开启需要一把金钥匙。",
                font, 20, TextAnchor.UpperLeft, new Color(0.88f, 0.88f, 0.88f));
            descText.horizontalOverflow = HorizontalWrapMode.Wrap;
            descText.verticalOverflow = VerticalWrapMode.Overflow;

            var actionBar = FindOrCreate("ActionBar", root.transform);
            SetRect(actionBar,
                anchorMin: new Vector2(0.5f, 0f), anchorMax: new Vector2(0.5f, 0f),
                pivot: new Vector2(0.5f, 0f),
                sizeDelta: new Vector2(460f, 64f), anchoredPos: new Vector2(0f, 20f));

            CreateSpriteSwapActionButton(actionBar.transform, "BtnUse",
                LoadSprite(SprBtnAction1), LoadSprite(SprBtnAction2),
                LoadSprite(SprBtnAction3), LoadSprite(SprBtnAction4),
                new Vector2(0.3f, 0.5f), new Vector2(0.3f, 0.5f), new Vector2(0.5f, 0.5f),
                new Vector2(180f, 56f), Vector2.zero);
            SetButtonLabel(actionBar.transform, "BtnUse", "使用", font);

            CreateSpriteSwapActionButton(actionBar.transform, "BtnBatchUse",
                LoadSprite(SprBtnAction1), LoadSprite(SprBtnAction2),
                LoadSprite(SprBtnAction3), LoadSprite(SprBtnAction4),
                new Vector2(0.72f, 0.5f), new Vector2(0.72f, 0.5f), new Vector2(0.5f, 0.5f),
                new Vector2(200f, 56f), Vector2.zero);
            SetButtonLabel(actionBar.transform, "BtnBatchUse", "批量使用(1)", font);
        }

        private static void BuildSkillDetailPanelContent(Transform panel, Font font)
        {
            ClearAllChildren(panel);

            var dimmer = FindOrCreate("Dimmer", panel);
            SetStretchFull(dimmer);
            SetImage(dimmer, null, raycast: true).color = new Color(0f, 0f, 0f, 0.45f);

            var root = FindOrCreate("PanelRoot", panel);
            SetStretchFull(root);
            SetImage(root, LoadSprite(SprDialogBg), raycast: true).type = Image.Type.Sliced;

            var titleGo = FindOrCreate("TitleText", root.transform);
            SetRect(titleGo,
                anchorMin: new Vector2(0.5f, 1f), anchorMax: new Vector2(0.5f, 1f),
                pivot: new Vector2(0.5f, 1f),
                sizeDelta: new Vector2(400f, 48f), anchoredPos: new Vector2(0f, -20f));
            CreateText(titleGo.transform, "Text", "技能详情", font, 32, TextAnchor.MiddleCenter,
                new Color(1f, 0.92f, 0.55f));

            // 保留 BtnCloseDetail 供 UIManager 接线
            CreateSpriteSwapActionButton(root.transform, "BtnCloseDetail",
                LoadSprite(SprBtnClose1), LoadSprite(SprBtnClose2),
                LoadSprite(SprBtnClose3), LoadSprite(SprBtnClose4),
                new Vector2(1f, 1f), new Vector2(1f, 1f), new Vector2(1f, 1f),
                new Vector2(48f, 48f), new Vector2(-14f, -14f));

            var iconArea = FindOrCreate("SkillIconArea", root.transform);
            SetRect(iconArea,
                anchorMin: new Vector2(0f, 0.5f), anchorMax: new Vector2(0f, 0.5f),
                pivot: new Vector2(0f, 0.5f),
                sizeDelta: new Vector2(110f, 110f), anchoredPos: new Vector2(28f, 10f));
            SetImage(iconArea, LoadSkillBtnSpritesDisplay(1), raycast: false);

            var skillIcon = FindOrCreate("SkillIcon", iconArea.transform);
            SetStretchFull(skillIcon);
            var skRt = skillIcon.GetComponent<RectTransform>();
            skRt.offsetMin = new Vector2(12f, 12f);
            skRt.offsetMax = new Vector2(-12f, -12f);
            SetImage(skillIcon, LoadSprite(SprSkillIconLi), raycast: false).preserveAspect = true;

            // 保留 DetailTitleText / DetailDescText 名称
            var nameGo = FindOrCreate("DetailTitleText", root.transform);
            SetRect(nameGo,
                anchorMin: new Vector2(0f, 0.62f), anchorMax: new Vector2(1f, 0.78f),
                pivot: new Vector2(0.5f, 0.5f),
                sizeDelta: new Vector2(-150f, 0f), anchoredPos: new Vector2(40f, 0f));
            CreateText(nameGo.transform, "Text", "雷霆击", font, 30, TextAnchor.MiddleLeft,
                new Color(1f, 0.92f, 0.55f));

            var descGo = FindOrCreate("DetailDescText", root.transform);
            SetRect(descGo,
                anchorMin: new Vector2(0f, 0.28f), anchorMax: new Vector2(1f, 0.58f),
                pivot: new Vector2(0.5f, 0.5f),
                sizeDelta: new Vector2(-150f, 0f), anchoredPos: new Vector2(40f, 0f));
            var desc = CreateText(descGo.transform, "Text",
                "对目标造成雷电伤害，并有概率附加麻痹效果。冷却时间：8秒。",
                font, 22, TextAnchor.UpperLeft, new Color(0.85f, 0.85f, 0.85f));
            desc.horizontalOverflow = HorizontalWrapMode.Wrap;

            CreateDetailStatLine(root.transform, font, "StatCooldown", "冷却  8秒",
                new Vector2(0.5f, 0.22f), new Color(0.6f, 0.85f, 1f));
            CreateDetailStatLine(root.transform, font, "StatDamage", "伤害  120",
                new Vector2(0.5f, 0.16f), Color.white);
        }

        private static void CreateDetailHeader(Transform parent, Font font,
            string nameKey, string nameVal, string catKey, string catVal, Vector2 namePos)
        {
            var nameGo = FindOrCreate(nameKey, parent);
            SetRect(nameGo,
                anchorMin: new Vector2(0f, 1f), anchorMax: new Vector2(0.7f, 1f),
                pivot: new Vector2(0f, 1f),
                sizeDelta: new Vector2(0f, 40f), anchoredPos: namePos);
            CreateText(nameGo.transform, "Text", nameVal, font, 28, TextAnchor.MiddleLeft,
                new Color(0.6f, 0.85f, 1f));

            var catGo = FindOrCreate(catKey, parent);
            SetRect(catGo,
                anchorMin: new Vector2(1f, 1f), anchorMax: new Vector2(1f, 1f),
                pivot: new Vector2(1f, 1f),
                sizeDelta: new Vector2(100f, 36f), anchoredPos: new Vector2(-60f, -28f));
            CreateText(catGo.transform, "Text", catVal, font, 22, TextAnchor.MiddleRight,
                new Color(0.85f, 0.85f, 0.85f));
        }

        private static void CreateDetailStatLine(Transform parent, Font font, string name,
            string content, Vector2 anchorY, Color color)
        {
            var go = FindOrCreate(name, parent);
            SetRect(go,
                anchorMin: new Vector2(0.5f, anchorY.y), anchorMax: new Vector2(0.5f, anchorY.y),
                pivot: new Vector2(0.5f, 0.5f),
                sizeDelta: new Vector2(380f, 32f), anchoredPos: Vector2.zero);
            CreateText(go.transform, "Text", content, font, 22, TextAnchor.MiddleCenter, color);
        }

        private static void BuildEquipTags(Transform parent)
        {
            var tagRow = FindOrCreate("TagRow", parent);
            SetRect(tagRow,
                anchorMin: new Vector2(0.5f, 0.5f), anchorMax: new Vector2(0.5f, 0.5f),
                pivot: new Vector2(0.5f, 0.5f),
                sizeDelta: new Vector2(280f, 36f), anchoredPos: new Vector2(0f, 30f));

            var hlg = tagRow.GetComponent<HorizontalLayoutGroup>();
            if (hlg == null) hlg = tagRow.AddComponent<HorizontalLayoutGroup>();
            hlg.spacing = 8f;
            hlg.childAlignment = TextAnchor.MiddleCenter;
            hlg.childControlWidth = hlg.childControlHeight = false;

            for (int i = 0; i < 5; i++)
            {
                var star = FindOrCreate("Star" + i, tagRow.transform);
                var le = star.AddComponent<LayoutElement>();
                le.preferredWidth = le.preferredHeight = 28f;
                SetImage(star, LoadSprite(SprStar), raycast: false);
            }

            AddTagBadge(tagRow.transform, "TagAngel", SprTagAngel, 56f, 24f);
            AddTagBadge(tagRow.transform, "TagLegend", SprTagLegend, 56f, 24f);
        }

        private static void AddTagBadge(Transform parent, string name, string spritePath, float w, float h)
        {
            var go = FindOrCreate(name, parent);
            var le = go.GetComponent<LayoutElement>();
            if (le == null) le = go.AddComponent<LayoutElement>();
            le.preferredWidth = w;
            le.preferredHeight = h;
            SetImage(go, LoadSprite(spritePath), raycast: false);
        }

        private static void SetButtonLabel(Transform parent, string btnName, string label, Font font)
        {
            var btn = parent.Find(btnName);
            if (btn == null) return;
            var labelGo = FindOrCreate("Label", btn);
            SetStretchFull(labelGo.gameObject);
            CreateText(labelGo.transform, "Text", label, font, 24, TextAnchor.MiddleCenter, Color.white);
        }

        private static void CreateOrUpdateMenuButton(Transform parent, string btnName, Sprite icon)
        {
            var btnGo = FindOrCreate(btnName, parent);

            var rt = btnGo.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.sizeDelta = new Vector2(108f, 108f);

            var le = btnGo.GetComponent<LayoutElement>();
            if (le == null) le = btnGo.AddComponent<LayoutElement>();
            le.preferredWidth = 108f;
            le.preferredHeight = 108f;

            var img = SetImage(btnGo, icon, raycast: true);
            img.type = Image.Type.Simple;
            img.preserveAspect = true;

            var btn = btnGo.GetComponent<Button>();
            if (btn == null) btn = btnGo.AddComponent<Button>();
            btn.transition = Selectable.Transition.ColorTint;
            btn.targetGraphic = img;
            var colors = btn.colors;
            colors.normalColor = Color.white;
            colors.highlightedColor = new Color(1.12f, 1.12f, 1.12f, 1f);
            colors.pressedColor = new Color(0.86f, 0.86f, 0.86f, 1f);
            colors.selectedColor = new Color(1.08f, 1.08f, 1.08f, 1f);
            colors.disabledColor = new Color(0.6f, 0.6f, 0.6f, 0.5f);
            btn.colors = colors;

            // 图标资源已含文字，移除旧版矩形按钮下的 Text 子物体
            RemoveLegacyTextChild(btnGo.transform);
        }

        private static void RemoveLegacyTextChild(Transform btnTransform)
        {
            var textChild = btnTransform.Find("Text");
            if (textChild != null)
                Object.DestroyImmediate(textChild.gameObject);
        }

        private static void RemoveExtraChildren(Transform parent,
            System.Collections.Generic.HashSet<string> keepNames)
        {
            for (int i = parent.childCount - 1; i >= 0; i--)
            {
                var child = parent.GetChild(i);
                if (!keepNames.Contains(child.name))
                    Object.DestroyImmediate(child.gameObject);
            }
        }

        // ═══════════════════════════════════════════════
        //  UI 工具方法
        // ═══════════════════════════════════════════════
        private static Sprite LoadSprite(string path)
        {
            var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
            if (sprite != null) return sprite;

            var tex = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
            if (tex != null)
                return Sprite.Create(tex, new Rect(0f, 0f, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            return null;
        }

        private static GameObject FindOrCreate(string name, Transform parent)
        {
            var t = parent.Find(name);
            if (t != null) return t.gameObject;

            var go = new GameObject(name, typeof(RectTransform));
            go.transform.SetParent(parent, false);
            go.layer = LayerMask.NameToLayer("UI");
            return go;
        }

        private static void SetRect(GameObject go,
            Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot,
            Vector2 sizeDelta, Vector2 anchoredPos)
        {
            var rt = go.GetComponent<RectTransform>();
            rt.anchorMin = anchorMin;
            rt.anchorMax = anchorMax;
            rt.pivot = pivot;
            rt.sizeDelta = sizeDelta;
            rt.anchoredPosition = anchoredPos;
        }

        private static void SetStretchFull(GameObject go)
        {
            var rt = go.GetComponent<RectTransform>();
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;
        }

        private static void SetStretchAnchors(GameObject go,
            Vector2 anchorMin, Vector2 anchorMax, Vector2 offsetMin, Vector2 offsetMax)
        {
            var rt = go.GetComponent<RectTransform>();
            rt.anchorMin = anchorMin;
            rt.anchorMax = anchorMax;
            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.offsetMin = offsetMin;
            rt.offsetMax = offsetMax;
        }

        private static Image SetImage(GameObject go, Sprite sprite, bool raycast)
        {
            var img = go.GetComponent<Image>();
            if (img == null) img = go.AddComponent<Image>();
            img.sprite = sprite;
            img.raycastTarget = raycast;
            img.color = Color.white;
            return img;
        }

        private static Text CreateText(Transform parent, string name, string content, Font font,
            int fontSize, TextAnchor alignment, Color color)
        {
            var go = FindOrCreate(name, parent);
            SetStretchFull(go);
            var text = go.GetComponent<Text>();
            if (text == null) text = go.AddComponent<Text>();
            text.text = content;
            text.font = font != null ? font : Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            text.fontSize = fontSize;
            text.alignment = alignment;
            text.color = color;
            text.raycastTarget = false;
            return text;
        }

        private static void SetupSpriteSwapButton(GameObject go,
            Sprite normal, Sprite highlighted, Sprite pressed, Sprite disabled)
        {
            var img = SetImage(go, normal, raycast: true);
            img.type = Image.Type.Sliced;

            var btn = go.GetComponent<Button>();
            if (btn == null) btn = go.AddComponent<Button>();
            btn.transition = Selectable.Transition.SpriteSwap;
            btn.targetGraphic = img;
            var ss = btn.spriteState;
            ss.highlightedSprite = highlighted;
            ss.pressedSprite = pressed;
            ss.disabledSprite = disabled;
            btn.spriteState = ss;
        }
    }
}
