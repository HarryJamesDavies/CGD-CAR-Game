using UnityEngine;
using System.Collections;

namespace HF
{
    public struct Twists
    {
        public enum Twist
        {
            NULL,
            tsunami,
            eruption
        }

        public bool allOff;
        public bool tsunamiOff;
        public bool eruptionOff;
    }

}