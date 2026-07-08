using System.Text;
using UnityEditor;
using UnityEngine;

namespace Exam.Exam01.Editor
{
    public static class Exam01DeliveryCheck
    {
        private const string LoginScene = "Assets/Exams/Exam01/Scenes/Exam01_LoginScene.unity";
        private const string ServerScene = "Assets/Exams/Exam01/Scenes/Exam01_ServerScene.unity";

        [MenuItem("Exam/Exam01/第6步 断网自检清单")]
        public static void RunDeliveryCheck()
        {
            var report = new StringBuilder();
            report.AppendLine("=== Exam01 断网自检清单 ===");
            report.AppendLine();

            CheckBuildSettings(report);
            CheckScripts(report);
            CheckPrefabs(report);

            report.AppendLine();
            report.AppendLine("--- 请断网后 Play 手动勾选 ---");
            report.AppendLine("[ ] 正确账号密码 → Console「登陆成功」进选服");
            report.AppendLine("[ ] 错误密码 → Console「登录失败」不跳转");
            report.AppendLine("[ ] 选服场景恰好 10 条");
            report.AppendLine("[ ] 点击条目仅一条高亮");
            report.AppendLine("[ ] 关闭按钮后 ServerPanel 不可见");
            report.AppendLine("[ ] Console 无 Missing / NullReference");

            Debug.Log(report.ToString());
        }

        private static void CheckBuildSettings(StringBuilder report)
        {
            var scenes = EditorBuildSettings.scenes;
            if (scenes.Length < 2)
            {
                report.AppendLine("[X] Build Settings 场景不足 2 个");
                return;
            }

            bool loginOk = scenes[0].path == LoginScene && scenes[0].enabled;
            bool serverOk = scenes[1].path == ServerScene && scenes[1].enabled;

            report.AppendLine(loginOk ? "[√] Build 0 = LoginScene" : "[X] Build 0 应为 LoginScene");
            report.AppendLine(serverOk ? "[√] Build 1 = ServerScene" : "[X] Build 1 应为 ServerScene");

            if (scenes.Length > 2 && scenes[2].enabled)
                report.AppendLine("[i] Build 2 = MainScene（练习用，交卷可禁用）");
        }

        private static void CheckScripts(StringBuilder report)
        {
            string[] required =
            {
                "Assets/Exams/Exam01/Scripts/LoginController.cs",
                "Assets/Exams/Exam01/Scripts/ServerListController.cs",
                "Assets/Exams/Exam01/Scripts/ServerItemView.cs",
                "Assets/Exams/Exam01/Scripts/SceneNames.cs",
            };

            foreach (var path in required)
            {
                bool exists = System.IO.File.Exists(path);
                report.AppendLine(exists ? "[√] " + path : "[X] 缺少 " + path);
            }
        }

        private static void CheckPrefabs(StringBuilder report)
        {
            string[] required =
            {
                "Assets/Exams/Exam01/Prefabs/ServerItem.prefab",
                "Assets/Exams/Exam01/Prefabs/ServerUI.prefab",
            };

            foreach (var path in required)
            {
                bool exists = System.IO.File.Exists(path);
                report.AppendLine(exists ? "[√] " + path : "[X] 缺少 " + path);
            }
        }
    }
}
