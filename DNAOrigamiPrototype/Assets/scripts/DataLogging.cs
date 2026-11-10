using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System;

public class DataLogging
{
    string pID;
    string separator = ",";
    string curTime;
    string filepath;

    StringBuilder output = new StringBuilder();

    playerController playerInfo;

    public DataLogging() {
        curTime = cleanTimestamp(DateTime.Now.ToString());
        string eventType = "GAME_START";

        String[] headers = { "TIMESTAMP", "[EVENT_TYPE]", "[EVENT_INFO]", "[X POS]", "[Y POS]", "[Z POS]", "[MAINCAM X ROT]", "[FPP Y ROT]"}; //POS = position, ROT = rotation
        String[] startLog = { curTime, eventType, "NULL" , "0", "0", "0", "0", "0"};
        output.AppendLine(string.Join(separator, headers));
        output.AppendLine(string.Join(separator, startLog));
    }

    public DataLogging(string pID)
    {
        this.pID = pID;

        //EXPERIMENT START TIME
        curTime = cleanTimestamp(DateTime.Now.ToString());
        string eventType = "GAME_START";

        String[] headers = {"TIMESTAMP", "[EVENT_TYPE]", "[EVENT_INFO]", "[X POS]", "[Y POS]", "[Z POS]", "[MAINCAM X ROT]", "[FPP Y ROT]"};
        String[] startLog = { curTime, eventType, "NULL", "0", "0", "0", "0", "0"};
        output.AppendLine(string.Join(separator, headers));
        output.AppendLine(string.Join(separator, startLog));

    }

    public void updatePID(string s)
    { 
        pID = s;
    }

    //adding new event to logfile
    public void addLine(string eventType, string eventInformation)
    {
        curTime = cleanTimestamp(DateTime.Now.ToString());

        Vector3 playerPosition = playerInfo.transform.position;
        float xRot = playerInfo.mouseCam.getxRotation();

        String[] newLine = {curTime, 
                            eventType, 
                            eventInformation, 
                            playerPosition.x.ToString(), 
                            playerPosition.y.ToString(), 
                            playerPosition.z.ToString(),
                            xRot.ToString(),
                            playerInfo.transform.localRotation.eulerAngles.y.ToString()};

        output.AppendLine(string.Join(separator, newLine));

    }   

    public string cleanTimestamp(string timestamp)
    {
        timestamp = timestamp.Replace(" ", "_"); //avoiding spaces in filename
        timestamp = timestamp.Replace("/", ""); //removing / from mmddyyyy
        timestamp = timestamp.Replace(":", ""); //removing : from hh:mm:ss

        return timestamp;
    }
          
        public void dumpLogFile()
    {
        //CREATE FILENAME HERE
        curTime = cleanTimestamp(DateTime.Now.ToString());        
        filepath = Application.dataPath + @"/LOGGED_DATA/" + pID + "_" + curTime + ".csv";
        Directory.CreateDirectory(Path.GetDirectoryName(filepath));

        UnityEngine.Debug.Log(filepath);

        try { File.AppendAllText(filepath, output.ToString()); }
        catch (Exception e){
            Console.WriteLine(e.Message);
            Console.WriteLine("DATA COULD NOT BE WRITTEN TO THE CSV FILE.");
            return;
        }
    }

    public void setPlayerInfo(playerController playerInfo)
    {
        this.playerInfo = playerInfo;
    }
}
