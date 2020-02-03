
public interface IManageFlags 
{
	void RegisterNewLocalSwitch(ref int initialValue, string switchName, out bool ValueAlreadySet);

	bool GetSwitchBool(string switchName);
	void SetSwitch(string switchName, bool newValue);

	int GetSwitchInt(string switchName);
	void SetSwitch(string switchName, int newValue);

	bool SwitchExists(string switchName);
}
