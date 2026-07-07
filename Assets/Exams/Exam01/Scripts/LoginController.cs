using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Exam.Exam01
{
    /// <summary>W3 Exam01 · 登录校验并跳转主界面</summary>
    public class LoginController : MonoBehaviour
    {
        // --- 卷面自定义账号密码（考试当天可改这两个 const） ---
        private const string ValidAccount = "lixiaotong";
        private const string ValidPassword = "123456";

        [Header("UI")]
        [SerializeField] private InputField _accountInput;
        [SerializeField] private InputField _passwordInput;
        [SerializeField] private Button _loginButton;
        [SerializeField] private Text _tipText;

        private void Start()
        {
            _loginButton.onClick.AddListener(OnLoginClick);

            // P-L1 可选：回车链
            _accountInput.onSubmit.AddListener(_ => _passwordInput.ActivateInputField());
            _passwordInput.onSubmit.AddListener(_ => OnLoginClick());
        }

        /// <summary>试卷：失败 Console · 成功跳主界面</summary>
        public void OnLoginClick()
        {
            string account = _accountInput.text.Trim();
            string password = _passwordInput.text.Trim();

            if (account == ValidAccount && password == ValidPassword)
            {
                Debug.Log("登录成功");
                SceneManager.LoadScene(SceneNames.Main);
            }
            else
            {
                Debug.Log("登录失败");
                if (_tipText != null)
                    _tipText.text = "账号或密码错误";
            }
        }

        private void OnDestroy()
        {
            if (_loginButton != null)
                _loginButton.onClick.RemoveListener(OnLoginClick);
        }
    }
}