using System.Collections;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class UsersManager : MonoBehaviour
{
    // 注册UI和登录UI
    public GameObject RegisterUI;
    public GameObject LoginUI;
    public GameObject SceneUI;

    public GameObject MainUIVisualCamera;
    public GameObject SceneVisualCamera;
    

    // 登录用户名输入框和密码输入框
    public InputField userIdInputField;
    public InputField passwordInputfield;
    public Dropdown identityDropdown;
    //注册界面输入框
    public InputField userIdRegisterInputField;
    public InputField usernameRegisterInputField;
    public InputField passwordRegisterInputfield;
    public InputField passwordConfirmInputfield;
    public Dropdown identityRegisterDropdown;

    // 注册消息和登录消息
    public Text registerMessage;
    public Text loginMessage;

    // DBConnector类实例化
    public DBConnector connector = new DBConnector("localhost", "gamedb", "root", "root");

    // User类实例化
    public User user;
    void Start()
    {
        // 初始化UI状态
        LoginUI.SetActive(true);
        RegisterUI.SetActive(false);

        //连接数据库
        user = new User(connector);
    }

    // 加密密码
    private static string HashPassword(string password)
    {
        SHA256Managed crypt = new SHA256Managed();
        StringBuilder hash = new StringBuilder();
        byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password));
        foreach (byte theByte in crypto)
        {
            hash.Append(theByte.ToString("x2"));
        }
        return hash.ToString();
    }
    private static string HashConfirmPassword(string confirmpassword)
    {
        SHA256Managed crypt = new SHA256Managed();
        StringBuilder hash = new StringBuilder();
        byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(confirmpassword));
        foreach (byte theByte in crypto)
        {
            hash.Append(theByte.ToString("x2"));
        }
        return hash.ToString();
    }

    // 注册逻辑
    public void OnRegister()
    {
        // 从输入框获取用户名和密码
        string userId = userIdRegisterInputField.text;
        string username = usernameRegisterInputField.text;
        //使用哈希进行加密
        string password = HashPassword(passwordRegisterInputfield.text);
        string confirmPassword = HashConfirmPassword(passwordConfirmInputfield.text);
        string identity = identityRegisterDropdown.options[identityDropdown.value].text;

        if (userId == ""||username == "" || password == ""|| identity == "")
        {
            registerMessage.text = "账号或密码不能为空";
        }

        else
        {
            int code = user.Register(userId,username, password,identity,confirmPassword);
            if (code == 1)
            {
                Debug.Log("注册成功");
                registerMessage.text = "注册成功";
            }
            else if (code == 2)
            {
                Debug.Log("用户名已存在，请选择不同的用户名！");
                registerMessage.text = "用户名已存在，请选择不同的用户名！";
            }
            else if (code == 5) 
            {
                Debug.Log("两次密码不一致，请重新输入");
                Debug.Log(password+ confirmPassword);
                registerMessage.text = "两次密码不一致，请重新输入";
            }
            else
            {
                Debug.Log("注册失败");
                registerMessage.text = "注册失败";
            }
        }

        //清空输入框
        userIdRegisterInputField.text = "";
        usernameRegisterInputField.text = "";
        passwordRegisterInputfield.text = "";
        passwordConfirmInputfield.text = "";
        //identityRegisterDropdown.options[identityDropdown.value].text = "";
    }

    // 登录逻辑
   public void OnLogin()
{
    string userid = userIdInputField.text;
    string password = HashPassword(passwordInputfield.text);
    string identity = identityDropdown.options[identityDropdown.value].text;

    if (identity == "访客")
    {
        Debug.Log("访客登录成功");
        loginMessage.text = "访客登录成功";
        StartCoroutine(DelayedAction(2f)); // 延迟 2 秒后执行操作
        return;
    }

    if (userid == "" || password == "" || identity == "")
    {
        loginMessage.text = "账号或密码不能为空";
    }
    else
    {
        int code = user.Login(userid, password, identity);
        if (code == 1)
        {
            Debug.Log("登录成功");
            loginMessage.text = "登录成功";
            StartCoroutine(DelayedAction(2f)); // 延迟 2 秒后执行操作
            
            }
        else if (code == 2)
        {
            Debug.Log("登录失败：密码错误");
            loginMessage.text = "登录失败：密码错误";
        }
        else if (code == 3)
        {
            Debug.Log("登录失败：用户名不存在");
            loginMessage.text = "登录失败：用户名不存在";
        }
        else if (code == 5)
        {
            Debug.Log("登录失败:重新选择身份");
            loginMessage.text = "登录失败：重新选择身份";
        }
        else
        {
            Debug.Log("登录失败");
            loginMessage.text = "登录失败";
        }
    }

    userIdInputField.text = "";
    passwordInputfield.text = "";
}


// 协程方法，延迟指定秒数后执行操作
private IEnumerator DelayedAction(float delaySeconds)
{
    yield return new WaitForSeconds(delaySeconds);
    LoginUI.SetActive(false);
    MainUIVisualCamera.SetActive(false);
    SceneVisualCamera.SetActive(true);
    SceneUI.SetActive(true);

    }

}
