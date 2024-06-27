using MySql.Data.MySqlClient;
using Flyen.Email;

public static class SQLRegister
{
    public static void Register(SQLData _sqlData, SQLMessages _sqlMessages, string _emailInput, string _passwordInput, string _nicknameInput)
    {
        using (MySqlConnection connection = new MySqlConnection(_sqlData.DATABASE_CONNECTION_STRING))
        {
            connection.Open();
            if (!EmailUtility.ValidateEmail(_emailInput))
            {
                SQLWebConnection.Instance.StartCoroutine(SQLWebConnection.Instance.CatchError(_sqlMessages._emailIncorrectError));
                return;
            }
            string query0 = $"SELECT email FROM players WHERE email = \"{_emailInput}\";";
            string query1 = $"INSERT INTO players(id, email, login, password, exp, level, nextLevel) VALUES (0, \"{_emailInput}\", \"{_nicknameInput}\", \"{_passwordInput}\", 0, 1, 250);";
            MySqlCommand commandInsert1 = new MySqlCommand(query1, connection);
            MySqlCommand commandInsert = new MySqlCommand(query0, connection);
            using (MySqlDataReader reader = commandInsert.ExecuteReader())
            {
                if (!reader.Read())
                {
                    reader.Close();
                    commandInsert1.ExecuteScalar();
                }
                else
                    SQLWebConnection.Instance.StartCoroutine(SQLWebConnection.Instance.CatchError(_sqlMessages._emailExistsError, reader));
            }
        }
    }
}