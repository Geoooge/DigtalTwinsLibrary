using System.Collections;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class UsersManager : MonoBehaviour
{
    // ע��UI�͵�¼UI
    public GameObject RegisterUI;
    public GameObject LoginUI;
    public GameObject SceneUI;

    public GameObject MainUIVisualCamera;
    public GameObject SceneVisualCamera;
    

    // ��¼�û������������������
    public InputField userIdInputField;
    public InputField passwordInputfield;
    public Dropdown identityDropdown;
    //ע����������
    public InputField userIdRegisterInputField;
    public InputField usernameRegisterInputField;
    public InputField passwordRegisterInputfield;
    public InputField passwordConfirmInputfield;
    public Dropdown identityRegisterDropdown;

    // ע����Ϣ�͵�¼��Ϣ
    public Text registerMessage;
    public Text loginMessage;

    // DBConnector��ʵ����
    public DBConnector connector = new DBConnector("localhost", "gamedb", "root", "root");

    // User��ʵ����
    public User user;
    void Start()
    {
        // ��ʼ��UI״̬
        LoginUI.SetActive(true);
        RegisterUI.SetActive(false);

        //�������ݿ�
        user = new User(connector);
    }

    // ��������
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

    // ע���߼�
    public void OnRegister()
    {
        // ��������ȡ�û���������
        string userId = userIdRegisterInputField.text;
        string username = usernameRegisterInputField.text;
        //ʹ�ù�ϣ���м���
        string password = HashPassword(passwordRegisterInputfield.text);
        string confirmPassword = HashConfirmPassword(passwordConfirmInputfield.text);
        string identity = identityRegisterDropdown.options[identityDropdown.value].text;

        if (userId == ""||username == "" || password == ""|| identity == "")
        {
            registerMessage.text = "�˺Ż����벻��Ϊ��";
        }

        else
        {
            int code = user.Register(userId,username, password,identity,confirmPassword);
            if (code == 1)
            {
                Debug.Log("ע��ɹ�");
                registerMessage.text = "ע��ɹ�";
            }
            else if (code == 2)
            {
                Debug.Log("�û����Ѵ��ڣ���ѡ��ͬ���û�����");
                registerMessage.text = "�û����Ѵ��ڣ���ѡ��ͬ���û�����";
            }
            else if (code == 5) 
            {
                Debug.Log("�������벻һ�£�����������");
                Debug.Log(password+ confirmPassword);
                registerMessage.text = "�������벻һ�£�����������";
            }
            else
            {
                Debug.Log("ע��ʧ��");
                registerMessage.text = "ע��ʧ��";
            }
        }

        //��������
        userIdRegisterInputField.text = "";
        usernameRegisterInputField.text = "";
        passwordRegisterInputfield.text = "";
        passwordConfirmInputfield.text = "";
        //identityRegisterDropdown.options[identityDropdown.value].text = "";
    }

    // ��¼�߼�
   public void OnLogin()
{
    string userid = userIdInputField.text;
    string password = HashPassword(passwordInputfield.text);
    string identity = identityDropdown.options[identityDropdown.value].text;

    if (identity == "�ÿ�")
    {
        Debug.Log("�ÿ͵�¼�ɹ�");
        loginMessage.text = "�ÿ͵�¼�ɹ�";
        StartCoroutine(DelayedAction(2f)); // �ӳ� 2 ���ִ�в���
        return;
    }

    if (userid == "" || password == "" || identity == "")
    {
        loginMessage.text = "�˺Ż����벻��Ϊ��";
    }
    else
    {
        int code = user.Login(userid, password, identity);
        if (code == 1)
        {
            Debug.Log("��¼�ɹ�");
            loginMessage.text = "��¼�ɹ�";
            StartCoroutine(DelayedAction(2f)); // �ӳ� 2 ���ִ�в���
            
            }
        else if (code == 2)
        {
            Debug.Log("��¼ʧ�ܣ��������");
            loginMessage.text = "��¼ʧ�ܣ��������";
        }
        else if (code == 3)
        {
            Debug.Log("��¼ʧ�ܣ��û���������");
            loginMessage.text = "��¼ʧ�ܣ��û���������";
        }
        else if (code == 5)
        {
            Debug.Log("��¼ʧ��:����ѡ�����");
            loginMessage.text = "��¼ʧ�ܣ�����ѡ�����";
        }
        else
        {
            Debug.Log("��¼ʧ��");
            loginMessage.text = "��¼ʧ��";
        }
    }

    userIdInputField.text = "";
    passwordInputfield.text = "";
}


// Э�̷������ӳ�ָ��������ִ�в���
private IEnumerator DelayedAction(float delaySeconds)
{
    yield return new WaitForSeconds(delaySeconds);
    LoginUI.SetActive(false);
    MainUIVisualCamera.SetActive(false);
    SceneVisualCamera.SetActive(true);
    SceneUI.SetActive(true);

    }

}
