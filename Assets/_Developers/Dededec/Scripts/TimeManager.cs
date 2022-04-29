using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    #region Properties

        public System.DateTime LastConnection
        {
            get
            {
                return System.DateTime.Parse(SaveDataController.LastConnection);
            }

            set
            {
                SaveDataController.LastConnection = value.ToString();
            }
        }

        #endregion

        public System.TimeSpan TimeSinceLastConnection()
        {
            return System.DateTime.Now - LastConnection;
        }
}
