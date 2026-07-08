using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

namespace Exam.Exam01.Editor
{
    public static class ServerListWireSetup
    {
        private const string ScenePath = "Assets/Exams/Exam01/Scenes/Exam01_ServerScene.unity";
        private const string ServerItemPrefabPath = "Assets/Exams/Exam01/Prefabs/ServerItem.prefab";

        [MenuItem("Exam/Exam01/第5步 接线 ServerList")]
        public static void WireServerList()
        {
            if (EditorSceneManager.GetActiveScene().path != ScenePath)
            {
                if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    return;
                EditorSceneManager.OpenScene(ScenePath);
            }

            var serverUi = GameObject.Find("ServerUI");
            if (serverUi == null)
            {
                Debug.LogError("[Exam01] 场景中找不到 ServerUI，请先执行第4步。");
                return;
            }

            var managerGo = GameObject.Find("ServerManager");
            if (managerGo == null)
                managerGo = new GameObject("ServerManager");

            GameObjectUtility.RemoveMonoBehavioursWithMissingScript(managerGo);

            var controller = managerGo.GetComponent<ServerListController>();
            if (controller == null)
                controller = managerGo.AddComponent<ServerListController>();

            var root = serverUi.transform;
            var panel = root.Find("Background/ServerPanel")?.gameObject;
            var content = root.Find("Background/ServerPanel/ServerScrollView/Viewport/Content");
            var closeBtn = root.Find("Background/ServerPanel/CloseButton")?.GetComponent<Button>();
            var selectedLabel = root.Find("Background/ServerPanel/SelectedLabel")?.GetComponent<Text>();
            var itemPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(ServerItemPrefabPath);

            var so = new SerializedObject(controller);
            SetRef(so, "serverItemPrefab", itemPrefab);
            SetRef(so, "contentRoot", content);
            SetRef(so, "serverPanel", panel);
            SetRef(so, "closeButton", closeBtn);
            SetRef(so, "selectedLabel", selectedLabel);
            so.ApplyModifiedPropertiesWithoutUndo();

            EditorUtility.SetDirty(controller);
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());

            Debug.Log("[Exam01] 第5步完成：ServerListController 已接线。Play 测试 10 条区服 + 单选 + 关闭。");
        }

        private static void SetRef(SerializedObject so, string propName, Object value)
        {
            var prop = so.FindProperty(propName);
            if (prop != null)
                prop.objectReferenceValue = value;
        }
    }
}
