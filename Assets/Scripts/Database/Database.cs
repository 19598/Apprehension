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

	/// <summary>
	/// Sets up the database
	/// </summary>
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

		//Sets up the table. Throws an error if the table has already been set up, so this catches that error
		try
		{
			string[] items = { "Key1", "Key2", "Key3", "Key4" };//this is a list of every item in the game
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

	/// <summary>
	/// Lists everything in the player's inventory
	/// </summary>
	/// <param name="saveName">Name of the save</param>
	/// <returns>List of items and their quantity</returns>
	public List<string[]> getInventory(string saveName)
	{
		List<string[]> items = new List<string[]>();
		string connection = "URI=file:" + Application.persistentDataPath + "/" + "GameDB";
		IDbConnection dbcon = new SqliteConnection(connection);
		dbcon.Open();

		//Uses SQL to get everything from the inventory table
		IDbCommand cmnd_read = dbcon.CreateCommand();
		IDataReader reader;
		string query = "SELECT itemname, quantity FROM inventory";
		cmnd_read.CommandText = query;
		reader = cmnd_read.ExecuteReader();

		//Adds the results to a list and returns them
		while (reader.Read())
		{
			items.Add(new string[] { reader[0].ToString(), reader[1].ToString() });
		}
		dbcon.Close();
		return items;
	}
	
	/// <summary>
	/// Adds item to player inventory
	/// </summary>
	/// <param name="saveName">Name of the save</param>
	/// <param name="item">Name of the desired item</param>
	/// <param name="amount">Amount of the item to add to the inventory</param>
	public void addItem(string saveName, string item, int amount)
	{
		string connection = "URI=file:" + Application.persistentDataPath + "/" + "GameDB";
		IDbConnection dbcon = new SqliteConnection(connection);
		dbcon.Open();
		IDbCommand cmnd = dbcon.CreateCommand();
		cmnd.CommandText = "UPDATE inventory SET quantity = (SELECT quantity FROM inventory WHERE itemname = \"" + item + "\") + " + amount + " WHERE itemname = \"" + item + "\"";
		cmnd.ExecuteNonQuery();
	}

	/// <summary>
	/// Sets all items to zero
	/// </summary>
	/// <param name="saveName">Name of the save</param>
	public void resetDB(string saveName)
	{
		string connection = "URI=file:" + Application.persistentDataPath + "/" + "GameDB";
		IDbConnection dbcon = new SqliteConnection(connection);
		dbcon.Open();
		IDbCommand cmnd = dbcon.CreateCommand();
		cmnd.CommandText = "UPDATE inventory SET quantity = 0";
		cmnd.ExecuteNonQuery();
	}
}
