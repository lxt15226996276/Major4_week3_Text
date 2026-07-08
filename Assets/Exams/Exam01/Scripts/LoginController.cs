using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Exam.Exam01
{
    /// <summary>Exam01 · 登录校验（作者 lixiaotong）</summary>
    public class LoginController : MonoBehaviour
    {
        // 试卷密码 888888；账号 = 姓名全拼
        private const string ValidAccount = "lixiaotong";
        private const string ValidPassword = "888888";

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

        /// <summary>试卷：成功 Console「登陆成功」并跳转选服；失败 Console「登录失败」</summary>
        public void OnLoginClick()
        {
            string account = _accountInput.text.Trim();
            string password = _passwordInput.text.Trim();

            if (account == ValidAccount && password == ValidPassword)
            {
                Debug.Log("登陆成功");
                SceneManager.LoadScene(SceneNames.Server);
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