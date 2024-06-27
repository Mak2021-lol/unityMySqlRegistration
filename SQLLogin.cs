using MySql.Data.MySqlClient;
using UnityEngine;
using Flyen.Email;

public static class SQLLogin
{
    public static void Login(SQLData _sqlData, SQLMessages _sqlMessages, string _emailInput, string passwordInput, GameObject[] loggedInEnable, GameObject[] loggedInDisable)
    {
        using (MySqlConnection connection = new MySqlConnection(_sqlData.DATABASE_CONNECTION_STRING))
        {
            connection.Open();
            if (!EmailUtility.ValidateEmail(_emailInput))
            {
                SQLWebConnection.Instance.StartCoroutine(SQLWebConnection.Instance.CatchError(_sqlMessages._emailIncorrectError));
                return;
            }
            string query0 = $"SELECT id, email, login, password, exp, level FROM players WHERE email = \"{_emailInput}\";";
            MySqlCommand commandInsert = new MySqlCommand(query0, connection);
            using (MySqlDataReader reader = commandInsert.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    string email = reader.GetString("email");
                    string password = reader.GetString("password");
                    string login = reader.GetString("login");
                    int exp = reader.GetInt32("exp");
                    int level = reader.GetInt32("level");
                    int nextlevel = reader.GetInt32("nextLevel");
                    if (email == _emailInput && password == passwordInput)
                    {
                        SQLWebConnection._player = new PlayerData(id, email, login, password, exp, level, nextlevel);
                        bool loggedIn = LoggedIn(loggedInEnable, loggedInDisable);
                    }
                    else if (password != passwordInput)
                        SQLWebConnection.Instance.StartCoroutine(SQLWebConnection.Instance.CatchError(_sqlMessages._passwordError, reader));
                }
            }
        }
    }

    public static bool LoggedIn(GameObject[] _loggedInEnable, GameObject[] _loggedInDisable)
    {
        for (int i = 0; i < _loggedInEnable.Length; i++)
            _loggedInEnable[i].SetActive(true);
        for (int i = 0; i < _loggedInDisable.Length; i++)
            _loggedInDisable[i].SetActive(false);
        return true;
    }
}