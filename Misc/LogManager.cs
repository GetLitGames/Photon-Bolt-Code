using UnityEngine;
using System.Collections;

public class LogManager : Singleton<LogManager>
{
	bool isQuitting;

    protected override void Awake()
    {
		base.Awake();

        Application.logMessageReceived += HandleLog;
	}

	void Start()
	{
		Log("Application Startup", string.Empty, LogType.Log);
	}

	void OnApplicationQuit()
	{
		isQuitting = true;
	}

    //Create a string to store log level in
    string level = "";

    //Capture debug.log output, send logs to Loggly
    public void HandleLog(string logString, string stackTrace, LogType type)
    {
		if (type != LogType.Error && type != LogType.Exception)
			return;

		Log(logString, stackTrace, type);
    }

	void Log(string logString, string stackTrace, LogType type)
	{
        try
        {
            //Initialize WWWForm and store log level as a string
            level = type.ToString();
            var loggingForm = new WWWForm();

            //Add log message to WWWForm
            loggingForm.AddField("Level", level);
            loggingForm.AddField("Message", logString);
            loggingForm.AddField("Device_Model", SystemInfo.deviceModel);
            if (level.ToLower() == "error")
                loggingForm.AddField("Stack_Trace", stackTrace);

            //Add any User, Game, or Device MetaData that would be useful to finding issues later
            StartCoroutine(SendData(loggingForm));
		}
        catch(System.Exception)
        {
			enabled = false;
        }
	}

    public IEnumerator SendData(WWWForm form)
    {
        //Send WWW Form to Loggly, replace TOKEN with your unique ID from Loggly
        WWW sendLog = new WWW("https://logs-01.loggly.com/inputs/{loggly-input-guid}/tag/http/", form);
        yield return sendLog;
    }
}