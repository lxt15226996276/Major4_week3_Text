using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

namespace Exam.Exam01.Editor
{
    public static class MainSceneAutoSetup
    {
        private const string ScenePath = "Assets/Exams/Exam01/Scenes/Exam01_MainScene.unity";
        private const string ItemPrefabPath = "Assets/Exams/Exam01/Prefabs/InventoryItem.prefab";
        private const string SkillItemPrefabPath = "Assets/Exams/Exam01/Prefabs/SkillItem.prefab";
        private const string FontPath = "Assets/Images/Font/方正粗圆简体.ttf";
        private const float RefWidth = 1920f;
        private const float RefHeight = 1080f;

        [MenuItem("Exam/Exam01/搭建 MainScene 背包技能场景")]
        public static void BuildMainScene()
        {
            EnsureDirectory("Assets/Exams/Exam01/Scenes");
            EnsureDirectory("Assets/Exams/Exam01/Prefabs");

            Font font = AssetDatabase.LoadAssetAtPath<Font>(FontPath);

            var inventoryItemPrefab = CreateInventoryItemPrefab(font);
            var skillItemPrefab = CreateSkillItemPrefab(font);

            if (EditorSceneManager.GetActiveScene().path == ScenePath)
            {
                EditorSceneManager.CloseScene(EditorSceneManager.GetActiveScene(), true);
            }

            if (File.Exists(ScenePath))
            {
                AssetDatabase.DeleteAsset(ScenePath);
                AssetDatabase.Refresh();
            }

            var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);

            BuildSceneHierarchy(font, inventoryItemPrefab, skillItemPrefab);

            EditorSceneManager.SaveScene(scene, ScenePath);

            AddSceneToBuild(ScenePath);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log("[Exam01] MainScene 搭建完成：" + ScenePath);
        }

        private static void BuildSceneHierarchy(Font font, GameObject inventoryItemPrefab, GameObject skillItemPrefab)
        {
            GameObject canvasObj = CreateCanvas();
            GameObject.Find("Directional Light").SetActive(false);

            GameObject eventSystemObj = GameObject.Find("EventSystem");
            if (eventSystemObj == null)
            {
                eventSystemObj = new GameObject("EventSystem");
                eventSystemObj.AddComponent<UnityEngine.EventSystems.EventSystem>();
                eventSystemObj.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
            }

            GameObject bgImage = CreateBgImage(canvasObj.transform);
            CreateBottomMenu(canvasObj.transform, font);

            CreatePlayerPanel(canvasObj.transform, font);
            CreateInventoryPanel(canvasObj.transform, font, inventoryItemPrefab);
            CreateSkillPanel(canvasObj.transform, font, skillItemPrefab);
            CreateItemDetailPanel(canvasObj.transform, font);
            CreateSkillDetailPanel(canvasObj.transform, font);

            GameObject managerObj = new GameObject("UIManager");
            managerObj.transform.SetParent(canvasObj.transform, false);
        }

        private static GameObject CreateCanvas()
        {
            GameObject canvasObj = new GameObject("Canvas");
            Canvas canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 0;

            CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(RefWidth, RefHeight);
            scaler.matchWidthOrHeight = 0.5f;

            canvasObj.AddComponent<GraphicRaycaster>();
            return canvasObj;
        }

        private static GameObject CreateBgImage(Transform parent)
        {
            GameObject bg = new GameObject("Background");
            bg.transform.SetParent(parent, false);

            Image img = bg.AddComponent<Image>();
            img.color = new Color(0.1f, 0.12f, 0.18f, 1f);
            img.raycastTarget = false;

            RectTransform rt = bg.GetComponent<RectTransform>();
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;
            return bg;
        }

        private static GameObject CreateBottomMenu(Transform parent, Font font)
        {
            GameObject menuBar = new GameObject("BottomMenuBar");
            menuBar.transform.SetParent(parent, false);

            Image img = menuBar.AddComponent<Image>();
            img.color = new Color(0.15f, 0.18f, 0.25f, 0.95f);
            img.raycastTarget = false;

            RectTransform rt = menuBar.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0f, 0f);
            rt.anchorMax = new Vector2(1f, 0f);
            rt.pivot = new Vector2(0.5f, 0f);
            rt.sizeDelta = new Vector2(0f, 120f);
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = new Vector2(0f, 120f);

            HorizontalLayoutGroup hlg = menuBar.AddComponent<HorizontalLayoutGroup>();
            hlg.spacing = 20f;
            hlg.childAlignment = TextAnchor.MiddleCenter;
            hlg.childControlWidth = true;
            hlg.childControlHeight = true;
            hlg.childForceExpandWidth = true;
            hlg.childForceExpandHeight = true;

            ContentSizeFitter fitter = menuBar.AddComponent<ContentSizeFitter>();
            fitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            string[] menuNames = { "任务", "系统", "战斗", "技能", "商店", "背包" };
            Color[] btnColors = {
                new Color(0.8f, 0.5f, 0.2f),
                new Color(0.5f, 0.5f, 0.5f),
                new Color(0.2f, 0.5f, 0.8f),
                new Color(0.5f, 0.2f, 0.8f),
                new Color(0.8f, 0.2f, 0.5f),
                new Color(0.2f, 0.8f, 0.5f)
            };

            for (int i = 0; i < menuNames.Length; i++)
            {
                CreateMenuButton(menuBar.transform, font, menuNames[i], btnColors[i]);
            }

            return menuBar;
        }

        private static GameObject CreateMenuButton(Transform parent, Font font, string name, Color color)
        {
            GameObject btnObj = new GameObject(name + "Button");
            btnObj.transform.SetParent(parent, false);

            Image img = btnObj.AddComponent<Image>();
            img.color = color;
            img.raycastTarget = true;

            Button btn = btnObj.AddComponent<Button>();
            ColorBlock colors = btn.colors;
            colors.normalColor = color;
            colors.highlightedColor = color * 1.2f;
            colors.pressedColor = color * 0.8f;
            btn.colors = colors;

            RectTransform rt = btnObj.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(180f, 100f);

            GameObject textObj = new GameObject("Text");
            textObj.transform.SetParent(btnObj.transform, false);
            Text text = textObj.AddComponent<Text>();
            text.text = name;
            text.font = font != null ? font : Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            text.fontSize = 28;
            text.alignment = TextAnchor.MiddleCenter;
            text.color = Color.white;
            text.raycastTarget = false;

            RectTransform textRt = textObj.GetComponent<RectTransform>();
            textRt.anchorMin = Vector2.zero;
            textRt.anchorMax = Vector2.one;
            textRt.offsetMin = Vector2.zero;
            textRt.offsetMax = Vector2.zero;

            return btnObj;
        }

        private static GameObject CreatePlayerPanel(Transform parent, Font font)
        {
            GameObject panel = new GameObject("PlayerPanel");
            panel.transform.SetParent(parent, false);

            Image img = panel.AddComponent<Image>();
            img.color = new Color(0.2f, 0.18f, 0.15f, 0.96f);
            img.raycastTarget = true;

            RectTransform rt = panel.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0f, 0.5f);
            rt.anchorMax = new Vector2(0.4f, 0.95f);
            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.sizeDelta = new Vector2(0f, 0f);

            GameObject titleObj = new GameObject("TitleText");
            titleObj.transform.SetParent(panel.transform, false);
            Text title = titleObj.AddComponent<Text>();
            title.text = "角色信息";
            title.font = font != null ? font : Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            title.fontSize = 36;
            title.alignment = TextAnchor.MiddleCenter;
            title.color = new Color(1f, 0.92f, 0.6f, 1f);
            title.raycastTarget = false;

            RectTransform titleRt = titleObj.GetComponent<RectTransform>();
            titleRt.anchorMin = new Vector2(0.5f, 1f);
            titleRt.anchorMax = new Vector2(0.5f, 1f);
            titleRt.pivot = new Vector2(0.5f, 1f);
            titleRt.sizeDelta = new Vector2(300f, 60f);
            titleRt.anchoredPosition = new Vector2(0f, -30f);

            GameObject nameObj = new GameObject("NameText");
            nameObj.transform.SetParent(panel.transform, false);
            Text name = nameObj.AddComponent<Text>();
            name.text = "角色名：lixiaotong";
            name.font = font != null ? font : Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            name.fontSize = 28;
            name.alignment = TextAnchor.MiddleLeft;
            name.color = Color.white;
            name.raycastTarget = false;

            RectTransform nameRt = nameObj.GetComponent<RectTransform>();
            nameRt.anchorMin = new Vector2(0f, 0.8f);
            nameRt.anchorMax = new Vector2(1f, 0.8f);
            nameRt.pivot = new Vector2(0f, 0.5f);
            nameRt.sizeDelta = new Vector2(0f, 40f);
            nameRt.offsetMin = new Vector2(30f, 0f);
            nameRt.offsetMax = new Vector2(-30f, 0f);

            GameObject hpObj = new GameObject("HPText");
            hpObj.transform.SetParent(panel.transform, false);
            Text hp = hpObj.AddComponent<Text>();
            hp.text = "生命值：100/100";
            hp.font = font != null ? font : Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            hp.fontSize = 24;
            hp.alignment = TextAnchor.MiddleLeft;
            hp.color = new Color(0.9f, 0.3f, 0.3f, 1f);
            hp.raycastTarget = false;

            RectTransform hpRt = hpObj.GetComponent<RectTransform>();
            hpRt.anchorMin = new Vector2(0f, 0.7f);
            hpRt.anchorMax = new Vector2(1f, 0.7f);
            hpRt.pivot = new Vector2(0f, 0.5f);
            hpRt.sizeDelta = new Vector2(0f, 40f);
            hpRt.offsetMin = new Vector2(30f, 0f);
            hpRt.offsetMax = new Vector2(-30f, 0f);

            GameObject mpObj = new GameObject("MPText");
            mpObj.transform.SetParent(panel.transform, false);
            Text mp = mpObj.AddComponent<Text>();
            mp.text = "魔力值：80/100";
            mp.font = font != null ? font : Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            mp.fontSize = 24;
            mp.alignment = TextAnchor.MiddleLeft;
            mp.color = new Color(0.3f, 0.5f, 0.9f, 1f);
            mp.raycastTarget = false;

            RectTransform mpRt = mpObj.GetComponent<RectTransform>();
            mpRt.anchorMin = new Vector2(0f, 0.6f);
            mpRt.anchorMax = new Vector2(1f, 0.6f);
            mpRt.pivot = new Vector2(0f, 0.5f);
            mpRt.sizeDelta = new Vector2(0f, 40f);
            mpRt.offsetMin = new Vector2(30f, 0f);
            mpRt.offsetMax = new Vector2(-30f, 0f);

            GameObject attackObj = new GameObject("AttackText");
            attackObj.transform.SetParent(panel.transform, false);
            Text attack = attackObj.AddComponent<Text>();
            attack.text = "攻击力：100";
            attack.font = font != null ? font : Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            attack.fontSize = 24;
            attack.alignment = TextAnchor.MiddleLeft;
            attack.color = Color.white;
            attack.raycastTarget = false;

            RectTransform attackRt = attackObj.GetComponent<RectTransform>();
            attackRt.anchorMin = new Vector2(0f, 0.5f);
            attackRt.anchorMax = new Vector2(0.5f, 0.5f);
            attackRt.pivot = new Vector2(0f, 0.5f);
            attackRt.sizeDelta = new Vector2(0f, 40f);
            attackRt.offsetMin = new Vector2(30f, 0f);
            attackRt.offsetMax = new Vector2(0f, 0f);

            GameObject defenseObj = new GameObject("DefenseText");
            defenseObj.transform.SetParent(panel.transform, false);
            Text defense = defenseObj.AddComponent<Text>();
            defense.text = "防御力：50";
            defense.font = font != null ? font : Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            defense.fontSize = 24;
            defense.alignment = TextAnchor.MiddleLeft;
            defense.color = Color.white;
            defense.raycastTarget = false;

            RectTransform defenseRt = defenseObj.GetComponent<RectTransform>();
            defenseRt.anchorMin = new Vector2(0.5f, 0.5f);
            defenseRt.anchorMax = new Vector2(1f, 0.5f);
            defenseRt.pivot = new Vector2(0f, 0.5f);
            defenseRt.sizeDelta = new Vector2(0f, 40f);
            defenseRt.offsetMin = new Vector2(0f, 0f);
            defenseRt.offsetMax = new Vector2(-30f, 0f);

            GameObject closeBtn = CreateCloseButton(panel.transform, font);
            closeBtn.name = "CloseButton";
            return panel;
        }

        private static GameObject CreateInventoryPanel(Transform parent, Font font, GameObject itemPrefab)
        {
            GameObject panel = new GameObject("InventoryPanel");
            panel.transform.SetParent(parent, false);
            panel.SetActive(false);

            Image img = panel.AddComponent<Image>();
            img.color = new Color(0.2f, 0.18f, 0.15f, 0.96f);
            img.raycastTarget = true;

            RectTransform rt = panel.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.sizeDelta = new Vector2(800f, 700f);
            rt.anchoredPosition = Vector2.zero;

            GameObject titleObj = new GameObject("TitleText");
            titleObj.transform.SetParent(panel.transform, false);
            Text title = titleObj.AddComponent<Text>();
            title.text = "背包";
            title.font = font != null ? font : Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            title.fontSize = 40;
            title.alignment = TextAnchor.MiddleCenter;
            title.color = new Color(1f, 0.92f, 0.6f, 1f);
            title.raycastTarget = false;

            RectTransform titleRt = titleObj.GetComponent<RectTransform>();
            titleRt.anchorMin = new Vector2(0.5f, 1f);
            titleRt.anchorMax = new Vector2(0.5f, 1f);
            titleRt.pivot = new Vector2(0.5f, 1f);
            titleRt.sizeDelta = new Vector2(300f, 70f);
            titleRt.anchoredPosition = new Vector2(0f, -30f);

            CreateCloseButton(panel.transform, font);

            GameObject scrollViewObj = new GameObject("InventoryScrollView");
            scrollViewObj.transform.SetParent(panel.transform, false);

            Image svBg = scrollViewObj.AddComponent<Image>();
            svBg.color = new Color(0.1f, 0.08f, 0.05f, 0.8f);
            svBg.raycastTarget = true;

            ScrollRect scrollRect = scrollViewObj.AddComponent<ScrollRect>();
            scrollRect.horizontal = false;
            scrollRect.vertical = true;
            scrollRect.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHide;

            RectTransform svRt = scrollViewObj.GetComponent<RectTransform>();
            svRt.anchorMin = new Vector2(0.5f, 0.15f);
            svRt.anchorMax = new Vector2(0.5f, 0.9f);
            svRt.pivot = new Vector2(0.5f, 0.5f);
            svRt.sizeDelta = new Vector2(740f, 0f);

            GameObject viewportObj = new GameObject("Viewport");
            viewportObj.transform.SetParent(scrollViewObj.transform, false);
            Image vpImg = viewportObj.AddComponent<Image>();
            vpImg.color = Color.clear;
            vpImg.raycastTarget = true;
            Mask mask = viewportObj.AddComponent<Mask>();
            mask.showMaskGraphic = false;

            RectTransform vpRt = viewportObj.GetComponent<RectTransform>();
            vpRt.anchorMin = Vector2.zero;
            vpRt.anchorMax = Vector2.one;
            vpRt.offsetMin = new Vector2(10f, 10f);
            vpRt.offsetMax = new Vector2(-10f, -10f);

            GameObject contentObj = new GameObject("Content");
            contentObj.transform.SetParent(viewportObj.transform, false);

            GridLayoutGroup grid = contentObj.AddComponent<GridLayoutGroup>();
            grid.cellSize = new Vector2(170f, 170f);
            grid.spacing = new Vector2(10f, 10f);
            grid.startCorner = GridLayoutGroup.Corner.UpperLeft;
            grid.startAxis = GridLayoutGroup.Axis.Horizontal;
            grid.childAlignment = TextAnchor.UpperCenter;
            grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            grid.constraintCount = 4;

            ContentSizeFitter fitter = contentObj.AddComponent<ContentSizeFitter>();
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            RectTransform contentRt = contentObj.GetComponent<RectTransform>();
            contentRt.anchorMin = new Vector2(0.5f, 1f);
            contentRt.anchorMax = new Vector2(0.5f, 1f);
            contentRt.pivot = new Vector2(0.5f, 1f);
            contentRt.sizeDelta = new Vector2(720f, 0f);

            scrollRect.viewport = vpRt;
            scrollRect.content = contentRt;

            GameObject bottomBar = new GameObject("BottomBar");
            bottomBar.transform.SetParent(panel.transform, false);

            Image bottomImg = bottomBar.AddComponent<Image>();
            bottomImg.color = new Color(0.15f, 0.12f, 0.1f, 0.9f);
            bottomImg.raycastTarget = false;

            RectTransform bottomRt = bottomBar.GetComponent<RectTransform>();
            bottomRt.anchorMin = new Vector2(0f, 0f);
            bottomRt.anchorMax = new Vector2(1f, 0f);
            bottomRt.pivot = new Vector2(0.5f, 0f);
            bottomRt.sizeDelta = new Vector2(0f, 60f);
            bottomRt.offsetMin = Vector2.zero;
            bottomRt.offsetMax = new Vector2(0f, 60f);

            HorizontalLayoutGroup bottomHlg = bottomBar.AddComponent<HorizontalLayoutGroup>();
            bottomHlg.spacing = 20f;
            bottomHlg.childAlignment = TextAnchor.MiddleCenter;
            bottomHlg.childControlWidth = false;
            bottomHlg.childControlHeight = true;
            bottomHlg.childForceExpandWidth = true;
            bottomHlg.padding = new RectOffset(20, 20, 10, 10);

            CreateActionButton(bottomBar.transform, font, "出售", new Color(0.8f, 0.3f, 0.3f));
            CreateActionButton(bottomBar.transform, font, "整理", new Color(0.3f, 0.6f, 0.9f));

            return panel;
        }

        private static GameObject CreateSkillPanel(Transform parent, Font font, GameObject skillItemPrefab)
        {
            GameObject panel = new GameObject("SkillPanel");
            panel.transform.SetParent(parent, false);
            panel.SetActive(false);

            Image img = panel.AddComponent<Image>();
            img.color = new Color(0.18f, 0.15f, 0.25f, 0.96f);
            img.raycastTarget = true;

            RectTransform rt = panel.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.sizeDelta = new Vector2(800f, 700f);
            rt.anchoredPosition = Vector2.zero;

            GameObject titleObj = new GameObject("TitleText");
            titleObj.transform.SetParent(panel.transform, false);
            Text title = titleObj.AddComponent<Text>();
            title.text = "技能";
            title.font = font != null ? font : Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            title.fontSize = 40;
            title.alignment = TextAnchor.MiddleCenter;
            title.color = new Color(0.6f, 0.9f, 1f, 1f);
            title.raycastTarget = false;

            RectTransform titleRt = titleObj.GetComponent<RectTransform>();
            titleRt.anchorMin = new Vector2(0.5f, 1f);
            titleRt.anchorMax = new Vector2(0.5f, 1f);
            titleRt.pivot = new Vector2(0.5f, 1f);
            titleRt.sizeDelta = new Vector2(300f, 70f);
            titleRt.anchoredPosition = new Vector2(0f, -30f);

            CreateCloseButton(panel.transform, font);

            GameObject scrollViewObj = new GameObject("SkillScrollView");
            scrollViewObj.transform.SetParent(panel.transform, false);

            Image svBg = scrollViewObj.AddComponent<Image>();
            svBg.color = new Color(0.08f, 0.06f, 0.12f, 0.8f);
            svBg.raycastTarget = true;

            ScrollRect scrollRect = scrollViewObj.AddComponent<ScrollRect>();
            scrollRect.horizontal = false;
            scrollRect.vertical = true;
            scrollRect.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHide;

            RectTransform svRt = scrollViewObj.GetComponent<RectTransform>();
            svRt.anchorMin = new Vector2(0.5f, 0.15f);
            svRt.anchorMax = new Vector2(0.5f, 0.9f);
            svRt.pivot = new Vector2(0.5f, 0.5f);
            svRt.sizeDelta = new Vector2(740f, 0f);

            GameObject viewportObj = new GameObject("Viewport");
            viewportObj.transform.SetParent(scrollViewObj.transform, false);
            Image vpImg = viewportObj.AddComponent<Image>();
            vpImg.color = Color.clear;
            vpImg.raycastTarget = true;
            Mask mask = viewportObj.AddComponent<Mask>();
            mask.showMaskGraphic = false;

            RectTransform vpRt = viewportObj.GetComponent<RectTransform>();
            vpRt.anchorMin = Vector2.zero;
            vpRt.anchorMax = Vector2.one;
            vpRt.offsetMin = new Vector2(10f, 10f);
            vpRt.offsetMax = new Vector2(-10f, -10f);

            GameObject contentObj = new GameObject("Content");
            contentObj.transform.SetParent(viewportObj.transform, false);

            VerticalLayoutGroup vlg = contentObj.AddComponent<VerticalLayoutGroup>();
            vlg.spacing = 20f;
            vlg.childAlignment = TextAnchor.UpperCenter;
            vlg.childControlWidth = true;
            vlg.childControlHeight = true;
            vlg.childForceExpandWidth = true;
            vlg.childForceExpandHeight = false;

            ContentSizeFitter fitter = contentObj.AddComponent<ContentSizeFitter>();
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            RectTransform contentRt = contentObj.GetComponent<RectTransform>();
            contentRt.anchorMin = new Vector2(0.5f, 1f);
            contentRt.anchorMax = new Vector2(0.5f, 1f);
            contentRt.pivot = new Vector2(0.5f, 1f);
            contentRt.sizeDelta = new Vector2(720f, 0f);

            scrollRect.viewport = vpRt;
            scrollRect.content = contentRt;

            return panel;
        }

        private static GameObject CreateItemDetailPanel(Transform parent, Font font)
        {
            GameObject panel = new GameObject("ItemDetailPanel");
            panel.transform.SetParent(parent, false);
            panel.SetActive(false);

            Image img = panel.AddComponent<Image>();
            img.color = new Color(0.2f, 0.18f, 0.15f, 0.96f);
            img.raycastTarget = true;

            RectTransform rt = panel.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.sizeDelta = new Vector2(500f, 400f);
            rt.anchoredPosition = Vector2.zero;

            GameObject titleObj = new GameObject("TitleText");
            titleObj.transform.SetParent(panel.transform, false);
            Text title = titleObj.AddComponent<Text>();
            title.text = "物品详情";
            title.font = font != null ? font : Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            title.fontSize = 36;
            title.alignment = TextAnchor.MiddleCenter;
            title.color = new Color(1f, 0.92f, 0.6f, 1f);
            title.raycastTarget = false;

            RectTransform titleRt = titleObj.GetComponent<RectTransform>();
            titleRt.anchorMin = new Vector2(0.5f, 1f);
            titleRt.anchorMax = new Vector2(0.5f, 1f);
            titleRt.pivot = new Vector2(0.5f, 1f);
            titleRt.sizeDelta = new Vector2(300f, 60f);
            titleRt.anchoredPosition = new Vector2(0f, -30f);

            CreateCloseButton(panel.transform, font);

            GameObject nameObj = new GameObject("ItemNameText");
            nameObj.transform.SetParent(panel.transform, false);
            Text name = nameObj.AddComponent<Text>();
            name.text = "物品名称";
            name.font = font != null ? font : Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            name.fontSize = 32;
            name.alignment = TextAnchor.MiddleCenter;
            name.color = Color.white;
            name.raycastTarget = false;

            RectTransform nameRt = nameObj.GetComponent<RectTransform>();
            nameRt.anchorMin = new Vector2(0.5f, 0.75f);
            nameRt.anchorMax = new Vector2(0.5f, 0.75f);
            nameRt.pivot = new Vector2(0.5f, 0.5f);
            nameRt.sizeDelta = new Vector2(400f, 50f);

            GameObject descObj = new GameObject("ItemDescText");
            descObj.transform.SetParent(panel.transform, false);
            Text desc = descObj.AddComponent<Text>();
            desc.text = "物品描述信息...";
            desc.font = font != null ? font : Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            desc.fontSize = 24;
            desc.alignment = TextAnchor.MiddleCenter;
            desc.color = new Color(0.8f, 0.8f, 0.8f, 1f);
            desc.raycastTarget = false;
            desc.resizeTextForBestFit = true;
            desc.resizeTextMinSize = 18;

            RectTransform descRt = descObj.GetComponent<RectTransform>();
            descRt.anchorMin = new Vector2(0.05f, 0.25f);
            descRt.anchorMax = new Vector2(0.95f, 0.55f);
            descRt.pivot = new Vector2(0.5f, 0.5f);

            GameObject bottomBar = new GameObject("BottomBar");
            bottomBar.transform.SetParent(panel.transform, false);

            HorizontalLayoutGroup hlg = bottomBar.AddComponent<HorizontalLayoutGroup>();
            hlg.spacing = 30f;
            hlg.childAlignment = TextAnchor.MiddleCenter;
            hlg.childControlWidth = false;
            hlg.childControlHeight = true;

            RectTransform bottomRt = bottomBar.GetComponent<RectTransform>();
            bottomRt.anchorMin = new Vector2(0.5f, 0f);
            bottomRt.anchorMax = new Vector2(0.5f, 0f);
            bottomRt.pivot = new Vector2(0.5f, 0f);
            bottomRt.sizeDelta = new Vector2(400f, 70f);
            bottomRt.anchoredPosition = new Vector2(0f, 30f);

            CreateActionButton(bottomBar.transform, font, "使用", new Color(0.3f, 0.7f, 0.3f));
            CreateActionButton(bottomBar.transform, font, "批量使用", new Color(0.3f, 0.6f, 0.9f));

            return panel;
        }

        private static GameObject CreateSkillDetailPanel(Transform parent, Font font)
        {
            GameObject panel = new GameObject("SkillDetailPanel");
            panel.transform.SetParent(parent, false);
            panel.SetActive(false);

            Image img = panel.AddComponent<Image>();
            img.color = new Color(0.18f, 0.15f, 0.25f, 0.96f);
            img.raycastTarget = true;

            RectTransform rt = panel.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.sizeDelta = new Vector2(500f, 400f);
            rt.anchoredPosition = Vector2.zero;

            GameObject titleObj = new GameObject("TitleText");
            titleObj.transform.SetParent(panel.transform, false);
            Text title = titleObj.AddComponent<Text>();
            title.text = "技能信息";
            title.font = font != null ? font : Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            title.fontSize = 36;
            title.alignment = TextAnchor.MiddleCenter;
            title.color = new Color(0.6f, 0.9f, 1f, 1f);
            title.raycastTarget = false;

            RectTransform titleRt = titleObj.GetComponent<RectTransform>();
            titleRt.anchorMin = new Vector2(0.5f, 1f);
            titleRt.anchorMax = new Vector2(0.5f, 1f);
            titleRt.pivot = new Vector2(0.5f, 1f);
            titleRt.sizeDelta = new Vector2(300f, 60f);
            titleRt.anchoredPosition = new Vector2(0f, -30f);

            CreateCloseButton(panel.transform, font);

            GameObject nameObj = new GameObject("SkillNameText");
            nameObj.transform.SetParent(panel.transform, false);
            Text name = nameObj.AddComponent<Text>();
            name.text = "技能名称";
            name.font = font != null ? font : Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            name.fontSize = 32;
            name.alignment = TextAnchor.MiddleCenter;
            name.color = Color.white;
            name.raycastTarget = false;

            RectTransform nameRt = nameObj.GetComponent<RectTransform>();
            nameRt.anchorMin = new Vector2(0.5f, 0.75f);
            nameRt.anchorMax = new Vector2(0.5f, 0.75f);
            nameRt.pivot = new Vector2(0.5f, 0.5f);
            nameRt.sizeDelta = new Vector2(400f, 50f);

            GameObject descObj = new GameObject("SkillDescText");
            descObj.transform.SetParent(panel.transform, false);
            Text desc = descObj.AddComponent<Text>();
            desc.text = "技能描述信息...";
            desc.font = font != null ? font : Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            desc.fontSize = 24;
            desc.alignment = TextAnchor.MiddleCenter;
            desc.color = new Color(0.8f, 0.8f, 0.8f, 1f);
            desc.raycastTarget = false;
            desc.resizeTextForBestFit = true;

            RectTransform descRt = descObj.GetComponent<RectTransform>();
            descRt.anchorMin = new Vector2(0.05f, 0.25f);
            descRt.anchorMax = new Vector2(0.95f, 0.55f);
            descRt.pivot = new Vector2(0.5f, 0.5f);

            GameObject bottomBar = new GameObject("BottomBar");
            bottomBar.transform.SetParent(panel.transform, false);

            RectTransform bottomRt = bottomBar.GetComponent<RectTransform>();
            bottomRt.anchorMin = new Vector2(0.5f, 0f);
            bottomRt.anchorMax = new Vector2(0.5f, 0f);
            bottomRt.pivot = new Vector2(0.5f, 0f);
            bottomRt.sizeDelta = new Vector2(200f, 70f);
            bottomRt.anchoredPosition = new Vector2(0f, 30f);

            CreateActionButton(bottomBar.transform, font, "关闭", new Color(0.6f, 0.6f, 0.6f));

            return panel;
        }

        private static GameObject CreateCloseButton(Transform parent, Font font)
        {
            GameObject btnObj = new GameObject("CloseButton");
            btnObj.transform.SetParent(parent, false);

            Image img = btnObj.AddComponent<Image>();
            img.color = new Color(0.85f, 0.25f, 0.25f, 1f);
            img.raycastTarget = true;

            Button btn = btnObj.AddComponent<Button>();
            ColorBlock colors = btn.colors;
            colors.normalColor = new Color(0.85f, 0.25f, 0.25f, 1f);
            colors.highlightedColor = new Color(0.95f, 0.35f, 0.35f, 1f);
            colors.pressedColor = new Color(0.7f, 0.18f, 0.18f, 1f);
            btn.colors = colors;

            RectTransform rt = btnObj.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(1f, 1f);
            rt.anchorMax = new Vector2(1f, 1f);
            rt.pivot = new Vector2(1f, 1f);
            rt.sizeDelta = new Vector2(50f, 50f);
            rt.anchoredPosition = new Vector2(-15f, -15f);

            GameObject textObj = new GameObject("Text");
            textObj.transform.SetParent(btnObj.transform, false);
            Text text = textObj.AddComponent<Text>();
            text.text = "✕";
            text.font = font != null ? font : Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            text.fontSize = 30;
            text.alignment = TextAnchor.MiddleCenter;
            text.color = Color.white;
            text.raycastTarget = false;

            RectTransform textRt = textObj.GetComponent<RectTransform>();
            textRt.anchorMin = Vector2.zero;
            textRt.anchorMax = Vector2.one;
            textRt.offsetMin = Vector2.zero;
            textRt.offsetMax = Vector2.zero;

            return btnObj;
        }

        private static GameObject CreateActionButton(Transform parent, Font font, string text, Color color)
        {
            GameObject btnObj = new GameObject(text + "Button");
            btnObj.transform.SetParent(parent, false);

            Image img = btnObj.AddComponent<Image>();
            img.color = color;
            img.raycastTarget = true;

            Button btn = btnObj.AddComponent<Button>();
            ColorBlock colors = btn.colors;
            colors.normalColor = color;
            colors.highlightedColor = color * 1.2f;
            colors.pressedColor = color * 0.8f;
            btn.colors = colors;

            RectTransform rt = btnObj.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(150f, 50f);

            GameObject textObj = new GameObject("Text");
            textObj.transform.SetParent(btnObj.transform, false);
            Text btnText = textObj.AddComponent<Text>();
            btnText.text = text;
            btnText.font = font != null ? font : Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            btnText.fontSize = 26;
            btnText.alignment = TextAnchor.MiddleCenter;
            btnText.color = Color.white;
            btnText.raycastTarget = false;

            RectTransform textRt = textObj.GetComponent<RectTransform>();
            textRt.anchorMin = Vector2.zero;
            textRt.anchorMax = Vector2.one;
            textRt.offsetMin = Vector2.zero;
            textRt.offsetMax = Vector2.zero;

            return btnObj;
        }

        private static GameObject CreateInventoryItemPrefab(Font font)
        {
            GameObject itemObj = new GameObject("InventoryItem");

            Image bgImg = itemObj.AddComponent<Image>();
            bgImg.color = new Color(0.3f, 0.28f, 0.25f, 1f);
            bgImg.raycastTarget = true;

            Button btn = itemObj.AddComponent<Button>();
            ColorBlock colors = btn.colors;
            colors.normalColor = new Color(0.3f, 0.28f, 0.25f, 1f);
            colors.highlightedColor = new Color(0.45f, 0.42f, 0.38f, 1f);
            colors.pressedColor = new Color(0.2f, 0.18f, 0.15f, 1f);
            btn.colors = colors;

            RectTransform rt = itemObj.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(170f, 170f);
            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);

            GameObject iconObj = new GameObject("ItemIcon");
            iconObj.transform.SetParent(itemObj.transform, false);

            Image iconImg = iconObj.AddComponent<Image>();
            iconImg.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            iconImg.raycastTarget = false;

            RectTransform iconRt = iconObj.GetComponent<RectTransform>();
            iconRt.anchorMin = new Vector2(0.1f, 0.1f);
            iconRt.anchorMax = new Vector2(0.9f, 0.7f);

            GameObject nameObj = new GameObject("ItemNameText");
            nameObj.transform.SetParent(itemObj.transform, false);
            Text nameText = nameObj.AddComponent<Text>();
            nameText.text = "物品";
            nameText.font = font != null ? font : Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            nameText.fontSize = 20;
            nameText.alignment = TextAnchor.MiddleCenter;
            nameText.color = Color.white;
            nameText.raycastTarget = false;

            RectTransform nameRt = nameObj.GetComponent<RectTransform>();
            nameRt.anchorMin = new Vector2(0f, 0f);
            nameRt.anchorMax = new Vector2(1f, 0.3f);

            GameObject countObj = new GameObject("CountText");
            countObj.transform.SetParent(itemObj.transform, false);
            Text countText = countObj.AddComponent<Text>();
            countText.text = "1";
            countText.font = font != null ? font : Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            countText.fontSize = 18;
            countText.alignment = TextAnchor.MiddleRight;
            countText.color = new Color(1f, 0.9f, 0.5f, 1f);
            countText.raycastTarget = false;

            RectTransform countRt = countObj.GetComponent<RectTransform>();
            countRt.anchorMin = new Vector2(0.7f, 0.7f);
            countRt.anchorMax = new Vector2(0.95f, 0.95f);

            GameObject prefab = PrefabUtility.SaveAsPrefabAsset(itemObj, ItemPrefabPath);
            Object.DestroyImmediate(itemObj);
            return prefab;
        }

        private static GameObject CreateSkillItemPrefab(Font font)
        {
            GameObject itemObj = new GameObject("SkillItem");

            Image bgImg = itemObj.AddComponent<Image>();
            bgImg.color = new Color(0.25f, 0.22f, 0.32f, 1f);
            bgImg.raycastTarget = true;

            Button btn = itemObj.AddComponent<Button>();
            ColorBlock colors = btn.colors;
            colors.normalColor = new Color(0.25f, 0.22f, 0.32f, 1f);
            colors.highlightedColor = new Color(0.4f, 0.35f, 0.5f, 1f);
            colors.pressedColor = new Color(0.18f, 0.15f, 0.25f, 1f);
            btn.colors = colors;

            RectTransform rt = itemObj.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(720f, 150f);
            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);

            GameObject iconObj = new GameObject("SkillIcon");
            iconObj.transform.SetParent(itemObj.transform, false);

            Image iconImg = iconObj.AddComponent<Image>();
            iconImg.color = new Color(0.4f, 0.35f, 0.6f, 0.8f);
            iconImg.raycastTarget = false;

            RectTransform iconRt = iconObj.GetComponent<RectTransform>();
            iconRt.anchorMin = new Vector2(0.02f, 0.1f);
            iconRt.anchorMax = new Vector2(0.18f, 0.9f);

            GameObject infoObj = new GameObject("SkillInfo");
            infoObj.transform.SetParent(itemObj.transform, false);

            HorizontalLayoutGroup hlg = infoObj.AddComponent<HorizontalLayoutGroup>();
            hlg.spacing = 10f;
            hlg.childAlignment = TextAnchor.MiddleLeft;

            RectTransform infoRt = infoObj.GetComponent<RectTransform>();
            infoRt.anchorMin = new Vector2(0.2f, 0.15f);
            infoRt.anchorMax = new Vector2(0.98f, 0.85f);

            GameObject nameObj = new GameObject("SkillNameText");
            nameObj.transform.SetParent(infoObj.transform, false);
            Text nameText = nameObj.AddComponent<Text>();
            nameText.text = "技能名称";
            nameText.font = font != null ? font : Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            nameText.fontSize = 28;
            nameText.color = Color.white;
            nameText.raycastTarget = false;

            GameObject descObj = new GameObject("SkillDescText");
            descObj.transform.SetParent(infoObj.transform, false);
            Text descText = descObj.AddComponent<Text>();
            descText.text = "技能描述";
            descText.font = font != null ? font : Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            descText.fontSize = 20;
            descText.color = new Color(0.7f, 0.7f, 0.7f, 1f);
            descText.raycastTarget = false;

            GameObject cdObj = new GameObject("CDText");
            cdObj.transform.SetParent(infoObj.transform, false);
            Text cdText = cdObj.AddComponent<Text>();
            cdText.text = "CD: 5s";
            cdText.font = font != null ? font : Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            cdText.fontSize = 20;
            cdText.color = new Color(0.5f, 0.7f, 1f, 1f);
            cdText.raycastTarget = false;

            GameObject prefab = PrefabUtility.SaveAsPrefabAsset(itemObj, SkillItemPrefabPath);
            Object.DestroyImmediate(itemObj);
            return prefab;
        }

        private static void EnsureDirectory(string path)
        {
            string[] parts = path.Split('/');
            string current = "";
            for (int i = 0; i < parts.Length; i++)
            {
                current = (i == 0) ? parts[i] : current + "/" + parts[i];
                if (!AssetDatabase.IsValidFolder(current))
                {
                    string parent = i == 0 ? "Assets" : string.Join("/", parts, 0, i);
                    AssetDatabase.CreateFolder(parent, parts[i]);
                }
            }
        }

        private static void AddSceneToBuild(string scenePath)
        {
            EditorBuildSettingsScene[] buildScenes = EditorBuildSettings.scenes;
            bool exists = false;
            foreach (var s in buildScenes)
            {
                if (s.path == scenePath)
                {
                    exists = true;
                    break;
                }
            }
            if (!exists)
            {
                EditorBuildSettingsScene[] newScenes = new EditorBuildSettingsScene[buildScenes.Length + 1];
                buildScenes.CopyTo(newScenes, 0);
                newScenes[buildScenes.Length] = new EditorBuildSettingsScene(scenePath, true);
                EditorBuildSettings.scenes = newScenes;
            }
        }
    }
}
