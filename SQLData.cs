using System;
using UnityEngine;

public class SQLData
{
    private const string SQL_ip = "your host ip";
    private const string SQL_database_name = "your database name";
    private const string SQL_user_username = "your username";
    private const string SQL_user_password = "your password";
    private const int DATABASE_players_nicknameLimit = 20;
    private string CONNECTION_string = $"server={SQL_ip};user={SQL_user_username};database={SQL_database_name};password={SQL_user_password}";

    public string SQL_IP { get => SQL_ip; }
    public string SQL_DATABASE_NAME { get => SQL_database_name; }
    public string SQL_USER_USERNAME { get => SQL_user_username; }
    public string SQL_USER_PASSWORD { get => SQL_user_password; }
    public string DATABASE_PLAYERS_TABLENAME { get => DATABASE_players_tablename; }
    public int DATABASE_PLAYERS_NICKNAMELIMIT { get => DATABASE_players_nicknameLimit; }
    public string DATABASE_CONNECTION_STRING { get => CONNECTION_string; }
}

[Serializable]
public class SQLMessages
{
    public string _emailIncorrectError;
    public string _emailExistsError;
    public string _emailNotExistsError;
    public string _passwordError;
    public string _registerSuccess;
}

[Serializable]
public struct PlayerData
{
    public int ID;
    public string EMAIL;
    public string LOGIN;
    public string PASSWORD;
    public int LEVEL;
    public int EXP;
    public int NEXTLEVEL;

    public PlayerData(int id, string email, string login, string password, int level, int exp, int nextlevel)
    {
        ID = id;
        EMAIL = email;
        LOGIN = login;
        PASSWORD = password;
        LEVEL = level;
        EXP = exp;
        NEXTLEVEL = nextlevel;
    }
}
