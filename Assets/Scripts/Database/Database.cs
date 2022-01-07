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
			string[] items = { "Key", "Key1", "Skull Key"};
			IDbCommand cmnd = dbcon.CreateCommand();
			foreach (string item in items)
			{
				cmnd.CommandText = "INSERT INTO inventory (itemname, quantity) VALUES (\"" + item + "\", 0)";
				cmnd.ExecuteNonQuery();
			}
		}
		catch { }

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
		cmnd.CommandText = "UPDATE inventory SET quantity = (SELECT quantity FROM inventory WHERE itemname = \"" + item + "\") + " + amount + " WHERE itemname = \"" + item + "\"";
		cmnd.ExecuteNonQuery();
	}
}
