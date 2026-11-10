using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AllLoggers
{
    static DataLogging firstLogger = new DataLogging();

    public static void registerPlayer(playerController a) { firstLogger.setPlayerInfo(a); }
    public static DataLogging getFirstLogger() { return firstLogger; }
}
