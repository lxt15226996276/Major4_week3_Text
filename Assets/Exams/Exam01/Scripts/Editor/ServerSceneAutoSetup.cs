using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.EventSystems;

namespace Exam.Exam01.Editor
{
    /// <summary>Exam01 第 4 步：选服场景 + ServerUI 预制体实例</summary>
    public static class ServerSceneAutoSetup
    {
        private const string ScenePath = "Assets/Exams/Exam01/Scenes/Exam01_ServerScene.unity";
        private const string LoginScenePath = "Assets/Exams/Exam01/Scenes/Exam01_LoginScene.unity";
        private const string MainScenePath = "Assets/Exams/Exam01/Scenes/Exam01_MainScene.unity";
        private const string ServerUiPrefabPath = "Assets/Exams/Exam01/Prefabs/ServerUI.prefab";

        [MenuItem("Exam/Exam01/第4步 搭建 ServerScene")]
        public static void BuildServerScene()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(ServerUiPrefabPath);
            if (prefab == null)
            {
                Debug.LogError("[Exam01] 找不到 ServerUI.prefab：" + ServerUiPrefabPath);
                return;
            }

            var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);

            var light = GameObject.Find("Directional Light");
            if (light != null)
                light.SetActive(false);

            EnsureEventSystem();

            var instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            if (instance == null)
            {
                Debug.LogError("[Exam01] ServerUI 预制体实例化失败。");
                return;
            }

            instance.name = "ServerUI";

            EditorSceneManager.SaveScene(scene, ScenePath);
            UpdateBuildSettings();

            AssetDatabase.SaveAssets();
            Debug.Log("[Exam01] 第4步完成：已创建 " + ScenePath + "。Build：Login=0，Server=1。请 Play 从登录场景进入验证。");
        }

        private static void EnsureEventSystem()
        {
            if (Object.FindObjectOfType<EventSystem>() != null)
                return;

            var go = new GameObject("EventSystem");
            go.AddComponent<EventSystem>();
            go.AddComponent<StandaloneInputModule>();
        }

        private static void UpdateBuildSettings()
        {
            var scenes = new[]
            {
                new EditorBuildSettingsScene(LoginScenePath, true),
                new EditorBuildSettingsScene(ScenePath, true),
                new EditorBuildSettingsScene(MainScenePath, true),
            };
            EditorBuildSettings.scenes = scenes;
        }
    }
}
