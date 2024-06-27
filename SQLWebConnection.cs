using MySql.Data.MySqlClient;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class SQLWebConnection : MonoBehaviour
{
    public static SQLWebConnection Instance;
    [Header("REFERENCES")]
    [SerializeField] private GameObject[] _loggedInEnable;
    [SerializeField] private GameObject[] _loggedInDisable;
    [SerializeField] private Transform _messagesParent;
    [Header("REGISTER")]
    [SerializeField] private TMP_InputField _register_emailInput;
    [SerializeField] private TMP_InputField _register_nicknameInput;
    [SerializeField] private TMP_InputField _register_passwordInput;
    [Header("LOGIN")]
    [SerializeField] private TMP_InputField _login_emailInput;
    [SerializeField] private TMP_InputField _login_passwordInput;
    [Header("MESSAGES")]
    [SerializeField] private GameObject _messagePrefab;
    [SerializeField] private SQLMessages _messages;
    [SerializeField] private float _messagesTime;

    public static PlayerData _player;
    public static SQLData _data => new SQLData();

    private void Start()
    {
        if (Instance == null)
            Instance = this;
    }

    public void TryRegister() => SQLRegister.Register(_data, _messages, _register_emailInput.text, _register_passwordInput.text, _register_nicknameInput.text);
    public void TryLogin() => SQLLogin.Login(_data, _messages, _login_emailInput.text, _login_passwordInput.text, _loggedInEnable, _loggedInDisable);

    GameObject INST;

    public IEnumerator CatchError(string _error)
    {
        if(INST != null) Destroy(INST);
        try {
            INST = Instantiate(_messagePrefab, _messagesParent);
            INST.GetComponent<TextMeshProUGUI>().text = _error;
        }
        catch (Exception e) {
            Debug.LogError(e);
        }
        yield return new WaitForSeconds(_messagesTime);
        Destroy(INST);
        yield return null;
    }

    public IEnumerator CatchError(string _error, MySqlDataReader _reader)
    {
        _reader.Close();
        if (INST != null) Destroy(INST);
        try
        {
            INST = Instantiate(_messagePrefab, _messagesParent);
            INST.GetComponent<TextMeshProUGUI>().text = _error;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        yield return new WaitForSeconds(_messagesTime);
        Destroy(INST);
        yield return null;
    }
}
