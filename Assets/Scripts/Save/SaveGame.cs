using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;
using Mono.Data.Sqlite;

public static class SaveGame
{

    /// <summary>
    /// Inserst the player's save data into the database
    /// </summary>
    /// <param name="player">Formatted player data</param>
    /// <param name="saveName">Name of the save</param>
    public static void SavePlayer (PlayerData player, string saveName)
    {
        //creates a connection to the database and sets up database interaction
        string connection = "URI=file:" + Application.persistentDataPath + "/" + "GameDB";

        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        //creates the table if it does not exist that contains player information
        IDbCommand dbcmd;
        dbcmd = dbcon.CreateCommand();
        string q_createTable = "CREATE TABLE IF NOT EXISTS saveplayer (savename STRING PRIMARY KEY, posX REAL, posY REAL, posZ REAL, rotX REAL, rotY REAL, rotZ REAL, rotW REAL, Health REAL, Stamina REAL, Mana REAL)";
        dbcmd.CommandText = q_createTable;
        dbcmd.ExecuteReader();

        //inserts a save or replaces it if it already exists
        IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = "INSERT OR REPLACE INTO saveplayer (savename, posX, posY, posZ, rotX, rotY, rotZ, rotW, Health, Stamina, Mana) VALUES (\"" + saveName + "\", " + player.position[0] + ", " + player.position[1] + ", " + player.position[2] + ", " + player.orientation[0] + ", " + player.orientation[1] + ", " + player.orientation[2] + ", " + player.orientation[3] + ", " + player.health + ", " + player.stamina + ", " + player.mana + ")";
        cmnd.ExecuteNonQuery();
        dbcon.Close();
    }

    /// <summary>
    /// Loads the player from the database
    /// </summary>
    /// <param name="saveName">Name of the save</param>
    /// <returns>Raw player data</returns>
    public static PlayerData LoadPlayer (string saveName)
    {
        string connection = "URI=file:" + Application.persistentDataPath + "/" + "GameDB";
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        //gets the save with the requested save name
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT posX, posY, posZ, rotX, rotY, rotZ, rotW, Health, Stamina, Mana FROM saveplayer WHERE savename = \"" + saveName + "\"";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

        //if the save exists, create a PlayerData object from the save
        if (reader.Read())
        {
            float[] position = { float.Parse(reader[0].ToString()), float.Parse(reader[1].ToString()), float.Parse(reader[2].ToString()) };
            float[] rotation = { float.Parse(reader[3].ToString()), float.Parse(reader[4].ToString()), float.Parse(reader[5].ToString()), float.Parse(reader[6].ToString()) };
            PlayerData pd = new PlayerData(position, rotation, float.Parse(reader[7].ToString()), float.Parse(reader[8].ToString()), float.Parse(reader[9].ToString()));
            dbcon.Close();
            return pd;
        }
        else
        {
            dbcon.Close();
            return null;
        }
    }

    /// <summary>
    /// Saves enemy data
    /// </summary>
    /// <param name="enemies">A list of all the enemies needed to be saved</param>
    /// <param name="saveName">Name of the save</param>
    public static void SaveEnemies (EnemyClass[] enemies, string saveName)
    {
        string connection = "URI=file:" + Application.persistentDataPath + "/" + "GameDB";

        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        //creates the enemy table if it does not exist
        IDbCommand dbcmd;
        dbcmd = dbcon.CreateCommand();
        string q_createTable = "CREATE TABLE IF NOT EXISTS saveenemy (ID INTEGER PRIMARY KEY, savename STRING, posX REAL, posY REAL, posZ REAL, rotX REAL, rotY REAL, rotZ REAL, rotW REAL, Health REAL, Type REAL)";
        dbcmd.CommandText = q_createTable;
        dbcmd.ExecuteReader();

        try
        {
            //deletes old save
            IDbCommand cmnd = dbcon.CreateCommand();
            cmnd.CommandText = "DELETE FROM saveenemy WHERE savename = \"" + saveName + "\"";
            cmnd.ExecuteNonQuery();
        }
        catch {
            
        }

        List<float[]> enemyData = new List<float[]>();
        /*Enemy Data takes the following form
         * position x|y|z| rotation x|y|z|w| health| type|
         *          0|1|2|          3|4|5|6|      7|    8|
         */
        for (int i = 0; i < enemies.Length; i++)
        {
            //creates new save
            IDbCommand cmnd = dbcon.CreateCommand();
            Transform enemyTransform = enemies[i].GetComponent<Transform>();
            cmnd.CommandText = "INSERT INTO saveenemy (savename, posX, posY, posZ, rotX, rotY, rotZ, rotW, Health, Type) VALUES (\"" + saveName + "\", " + enemyTransform.position[0] + ", " + enemyTransform.position[1] + ", " + enemyTransform.position[2] + ", " + enemyTransform.rotation[0] + ", " + enemyTransform.rotation[1] + ", " + enemyTransform.rotation[2] + ", " + enemyTransform.rotation[3] + ", " + enemies[i].getHealth() + ", " + enemies[i].returnType() + ")";
            cmnd.ExecuteNonQuery();
        }
        dbcon.Close();
    }

    /// <summary>
    /// Loads all the enemies
    /// </summary>
    /// <param name="saveName">Name of the save</param>
    /// <returns>A list of raw enemy data</returns>
    public static List<float[]> LoadEnemies(string saveName)
    {
        string connection = "URI=file:" + Application.persistentDataPath + "/" + "GameDB";
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;

        //selects all saves with the following name
        List<float[]> enemyData = new List<float[]>();
        string query = "SELECT posX, posY, posZ, rotX, rotY, rotZ, rotW, Health, Type FROM saveenemy WHERE savename = \"" + saveName + "\"";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

        while (reader.Read())
        {
            //iterates through the results and puts them in a list
            float[] arrayData = { float.Parse(reader[0].ToString()), float.Parse(reader[1].ToString()), float.Parse(reader[2].ToString()), float.Parse(reader[3].ToString()), float.Parse(reader[4].ToString()), float.Parse(reader[5].ToString()), float.Parse(reader[6].ToString()), float.Parse(reader[7].ToString()), float.Parse(reader[8].ToString()) };
            enemyData.Add(arrayData);
        }
        dbcon.Close();
        return enemyData;
    }
    
    /// <summary>
    /// Lists all the saves available
    /// </summary>
    /// <returns>A list of save names</returns>
    public static List<string> getSaves()
    {
        List<string> fileNames = new List<string>();
        string connection = "URI=file:" + Application.persistentDataPath + "/" + "GameDB";
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT savename FROM saveplayer";
        cmnd_read.CommandText = query;

        try
        {
            reader = cmnd_read.ExecuteReader();

            while (reader.Read())
            {
                fileNames.Add(reader[0].ToString());
            }
            dbcon.Close();
            return fileNames;
        }
        catch
        {
            return fileNames;
        }
    }

    /// <summary>
    /// Deletes specified save
    /// </summary>
    /// <param name="saveName">Name of the save</param>
    public static void deleteSave()//string saveName)
    {
        string connection = "URI=file:" + Application.persistentDataPath + "/" + "GameDB";
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();
        IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = "DELETE FROM saveenemy";// WHERE savename = \"" + saveName + "\"";
        cmnd.ExecuteNonQuery();
        cmnd.CommandText = "DELETE FROM saveplayer";// WHERE savename = \"" + saveName + "\"";
        cmnd.ExecuteNonQuery();
        cmnd.CommandText = "DELETE FROM inventory";// WHERE savename = \"" + saveName + "\"";
        cmnd.ExecuteNonQuery();
        dbcon.Close();
    }
}
