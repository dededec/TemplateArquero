using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    #region Fields

        private IAA_Player _controls;

	    #endregion
	  
	    #region Properties
	  
        public IAA_Player Controls
        {
            get
            {
                return _controls;
            }
    }
        
	#endregion
	
	#region LifeCycle
	
    private void Awake() 
    {
        _controls = new IAA_Player();
    }
    
    #endregion
}
