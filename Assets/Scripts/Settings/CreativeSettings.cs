using NaughtyAttributes;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "CreativeSettings", menuName = "Settings/CreativeSettings")]
    public class CreativeSettings : ScriptableObject
    {
        [BoxGroup("Creative Settings")] public string playableLink =
            "https://apps.apple.com/us/app/cat-escape/id1536578421";
        
        [BoxGroup("Creative Settings")] public bool useTimer =true;
        [BoxGroup("Creative Settings")] [EnableIf("useTimer")] public bool showTimerUI =true;
        [BoxGroup("Creative Settings")] [EnableIf("useTimer")] public int timeToCTA = 5;
        
        [BoxGroup("UI Settings")] public bool showJoystick = true;

        [BoxGroup("Tutorial Settings")] public bool showTutorial = true;
        [BoxGroup("Tutorial Settings")] public bool showHint = true;
        [BoxGroup("Tutorial Settings")] [EnableIf("showHint")]
        public float hintTime = 2;

        [BoxGroup("Player Settings")] public float playerMoveSpeed = 3f;
        [BoxGroup("Enemy Settings")] public float enemyMoveSpeed = 3f;

    }
}