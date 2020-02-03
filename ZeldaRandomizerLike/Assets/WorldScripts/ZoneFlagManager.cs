using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneFlagManager : MonoBehaviour, IManageFlags, IServiceProvider
{
	public bool debugRefresh = false;

	[SerializeField]
	private List<InitialFlagData> initialSwitchData = new List<InitialFlagData>();

	private Dictionary<string, FlagData> switchData;

	private void Awake()
	{
		InitializeDictionary();
	}

	private void InitializeDictionary()
	{
		switchData = new Dictionary<string, FlagData>();

		foreach(InitialFlagData data in initialSwitchData)
		{
			switchData.Add(data.name, new FlagData(data.defaultValue));
		}
	}
	
	bool IManageFlags.GetSwitchBool(string switchName)
	{
		return switchData[switchName].bValue;
	}

	int IManageFlags.GetSwitchInt(string switchName)
	{
		return switchData[switchName].iValue;
	}

	void IManageFlags.RegisterNewLocalSwitch(ref int initialValue, string switchName, out bool valueAlreadySet)
	{
		valueAlreadySet = switchData.ContainsKey(switchName);

		if (valueAlreadySet)
		{
			initialValue = switchData[switchName].iValue;
		}
		else
			switchData.Add(switchName, new FlagData(initialValue));
	}

	void IManageFlags.SetSwitch(string switchName, bool newValue)
	{
		switchData[switchName].bValue = newValue;
	}

	void IManageFlags.SetSwitch(string switchName, int newValue)
	{
		switchData[switchName].iValue = newValue;
	}


	bool IManageFlags.SwitchExists(string switchName)
	{
		return switchData.ContainsKey(switchName);
	}

	private void LoadState()
	{
		//TODO: implement a save/load system. Likely through IOC and interfaces as well.
	}

	void Update()
	{
		if (debugRefresh)
		{
			debugRefresh = false;
			InitializeDictionary();
		}
	}


	void OnDestroy()
	{

	}

	private void SaveState()
	{

	}

	void IServiceProvider.RegisterServices()
	{
		//TODO: THIS SHOULD NOT BE HANDLED VIA IOC
		this.RegisterService<IManageFlags>();
	}
}

[System.Serializable]
public struct InitialFlagData
{
	public string name;
	public int defaultValue;
}

//Storing the information as a class right now, so then objects can just get a reference to their specific flag that they need to track
//and then use that reference without having to continuously look stuff up in the dictionary every frame.
public class FlagData
{
	private int actualvalue;
	public int iValue
	{
		get
		{
			return actualvalue;
		}
		set
		{
			actualvalue = value;
		}
	}
	public bool bValue
	{	get{
			return iValue > 0;
		}
		set {
			this.iValue = value ? 1 : 0;
		}
	}


	public FlagData()
	{
		actualvalue = 0;
	}

	public FlagData(int initialValue)
	{
		actualvalue = initialValue;
	}

	public FlagData(bool initialValue)
	{
		bValue = initialValue;
	}
}