

using System;
using UnityEngine;

/// <summary>
/// centeralised data manager to allow user pages have access to common data like app data without excess reloading
/// </summary>
public class DataManager
{
	/// <value>
	/// singleton instance for data manager itself
	/// </value>
	static DataManager instance;
	/// <value>
	/// state to indicate whether data  manager is loading data
	/// </value>
	bool loading;
	/// <value>
	/// data reference ketp to present excess reloading from backend
	/// </value>
	AppData data;

	/// <summary>
	/// constructor for initilising data manager 
	/// </summary>
	private DataManager()
    {
		// loading user data from file
		// check if data from file and firebase user is different
		// if true, remove file, re-load data from firebase
		// else assume data from file is valid for uses
    }

	/// <summary>
	/// getter method to retrieve singlton instance of firebase manager
	/// </summary>
	/// <returns>firebase manager instance</returns>
	static public DataManager getInstance()
    {
        if (instance == null)
        {
            instance = new DataManager();
        }
        return instance;
    }

	/// <summary>
	/// an interface for all pages to access user's app data
	/// </summary>
	/// <param name="callback">callback to notify caller when task is finished (optional</param>
	/// <param name="forceReload">to indicate force reload is needed</param>
	public void getGameData( Action<AppData, string> callback = null, bool forceReload = false)
	{
		if (data != null && !forceReload)
		{
			if (callback!=null)
			{
				callback(data, "load from memory");
			}
			return;
		}

		// call firebase manager to load data
		data = null;
		loading = true;
		var fbMgr = FirebaseManager.getInstance();
		fbMgr.getGameData((newData, msg) =>
		{
			// firebase return data, update game data
			loading = false;
			if (newData!=null)
			{
				this.data = newData;				
			}
			// if newData == null => it means error has happended

			// relay data back to requester via callback
			if (callback != null)
			{
				callback(this.data, msg);
			}
		});
	}

	/// <summary>
	/// saving changes to app data to firebase database 
	/// </summary>
	/// <param name="newAppData">app data (optional)</param>
	/// <param name="callback">callback to notify caller when task is finished (optional)</param>
	public void updateGameData(AppData newAppData = null, Action<bool, string> callback = null)
	{
		// save default to firebase when avatar is null
		if (newAppData != null)
		{
			this.data = newAppData;
		}
		if (data == null)
		{
			if (callback != null)
			{
				callback(false, "no default data to be saved");
			}
			return;
		}

		var fbMgr = FirebaseManager.getInstance();
		fbMgr.saveGameData(this.data, (result, msg) =>
		{
			Debug.Log("dataMgr.saveAvatarData: " + result + ", " + msg);
			if (callback != null)
			{
				callback(result, msg);
			}
		});
	}



	// should be call when log user out
	/// <summary>
	/// clearing of user's data when user is logout from system
	/// </summary>
	public void clearData(){
		data = null;
		// remove saved file from system
	}



}