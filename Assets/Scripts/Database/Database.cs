using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

public class Database : MonoBehaviour
{

	/*Inventory layout
		PK (String) - item name
		Int - Quantity
		Will start pre-populated with all items but quantities start with 0, meaning player doesn't have that item
	 */

	// Start is called before the first frame update
	void Start()
	{
		// Create database
		string connection = "URI=file:" + Application.persistentDataPath + "/" + "GameDB";

		// Open connection
		IDbConnection dbcon = new SqliteConnection(connection);
		dbcon.Open();

		// Create table inventory
		IDbCommand dbcmd;
		dbcmd = dbcon.CreateCommand();
		string q_createTable = "CREATE TABLE IF NOT EXISTS inventory (itemname STRING PRIMARY KEY, quantity INTEGER)";
		dbcmd.CommandText = q_createTable;
		dbcmd.ExecuteReader();

		try
		{
			string[] items = { "1", "2", "3", "4" };
			IDbCommand cmnd = dbcon.CreateCommand();
			foreach (string item in items)
			{
				cmnd.CommandText = "INSERT INTO inventory (itemname, quantity) VALUES (\"" + item + "\", 0)";
				cmnd.ExecuteNonQuery();
			}
		}
		catch { }

		/*
		// Create table skills
		q_createTable = "CREATE TABLE IF NOT EXISTS skills (skillname STRING PRIMARY KEY, level INTEGER)";
		dbcmd.CommandText = q_createTable;
		dbcmd.ExecuteReader();

		string[] skills = { "skill1", "skill2" };
		cmnd = dbcon.CreateCommand();
		foreach (string skill in skills)
		{
			cmnd.CommandText = "INSERT INTO skills (skillname, level) VALUES (" + skill + ", 0)";
			cmnd.ExecuteNonQuery();
		}*/

		dbcon.Close();
	}

	public List<string[]> getInventory(string saveName)
	{
		List<string[]> items = new List<string[]>();
		string connection = "URI=file:" + Application.persistentDataPath + "/" + "GameDB";
		IDbConnection dbcon = new SqliteConnection(connection);
		dbcon.Open();

		IDbCommand cmnd_read = dbcon.CreateCommand();
		IDataReader reader;
		string query = "SELECT itemname, quantity FROM inventory";
		cmnd_read.CommandText = query;
		reader = cmnd_read.ExecuteReader();

		while (reader.Read())
		{
			items.Add(new string[] {reader[0].ToString(), reader[1].ToString()});
			Debug.Log("item name: " + reader[0].ToString());
			Debug.Log("quantity: " + reader[1].ToString());
		}
		dbcon.Close();
		return items;
	}

	public void addItem(string saveName, string item, int amount)
	{
		string connection = "URI=file:" + Application.persistentDataPath + "/" + "GameDB";
		IDbConnection dbcon = new SqliteConnection(connection);
		dbcon.Open();
		IDbCommand cmnd = dbcon.CreateCommand();
		cmnd.CommandText = "UPDATE inventory SET quantity = (SELECT quantity FROM inventory WHERE itemname = " + item + ") + " + amount + " WHERE itemname = " + item;
		cmnd.ExecuteNonQuery();
	}

	/*
	public List<string[]> getSkills()
	{
		List<string[]> skills = new List<string[]>();
		string connection = "URI=file:" + Application.persistentDataPath + "/" + "GameDB";
		IDbConnection dbcon = new SqliteConnection(connection);
		dbcon.Open();

		IDbCommand cmnd_read = dbcon.CreateCommand();
		IDataReader reader;
		string query = "SELECT skillname, level FROM skills";
		cmnd_read.CommandText = query;
		reader = cmnd_read.ExecuteReader();

		while (reader.Read())
		{
			skills.Add(new string[] { reader[0].ToString(), reader[1].ToString() });
			Debug.Log("skill name: " + reader[0].ToString());
			Debug.Log("level: " + reader[1].ToString());
		}
		dbcon.Close();
		return skills;
	}

	public void addSkill(string skill, int amount)
	{
		string connection = "URI=file:" + Application.persistentDataPath + "/" + "GameDB";
		IDbConnection dbcon = new SqliteConnection(connection);
		dbcon.Open();
		IDbCommand cmnd = dbcon.CreateCommand();
		cmnd.CommandText = "UPDATE skills SET level = (SELECT level FROM skills WHERE skillname = " + skill + ") + " + amount + " WHERE skillname = " + skill;
		cmnd.ExecuteNonQuery();
	}*/

	public void insert(string table, string column, string value)
	{
		string connection = "URI=file:" + Application.persistentDataPath + "/" + "GameDB";
		IDbConnection dbcon = new SqliteConnection(connection);
		dbcon.Open();

		IDbCommand cmnd = dbcon.CreateCommand();
		cmnd.CommandText = "UPDATE " + table + " SET " + column + " = " + value;
		cmnd.ExecuteNonQuery();

		dbcon.Close();
	}

	public string selectValue(string table, string column)
	{
		string connection = "URI=file:" + Application.persistentDataPath + "/" + "GameDB";
		IDbConnection dbcon = new SqliteConnection(connection);
		dbcon.Open();

		IDbCommand cmnd_read = dbcon.CreateCommand();
		IDataReader reader;
		string query = "SELECT " + column + " FROM " + table;
		cmnd_read.CommandText = query;
		reader = cmnd_read.ExecuteReader();
		return reader[0].ToString();

	}
}
