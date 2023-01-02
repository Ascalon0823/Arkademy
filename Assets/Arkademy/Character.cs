using System;
using System.Collections.Generic;

namespace Arkademy
{
    [Serializable]
    public class Character
    {
        public string id;
        public string givenName;
        

        [Serializable]
        public struct Relationship
        {
            public string charId;
            public float familiarness;
            // public float respect;
            // public float faith;
            // public float friendliness;
            // public float romance;
        }

        [Serializable]
        public struct Personality
        {
            // public float courage;
            // public float resilience;
            // public float outgoingness;
            // public float discipline;
            // public float humor;
            // public float confidence;
        }

        [Serializable]
        public struct Mood
        {
            // public float relaxness;
            // public float romanticness;
            public float happiness;
            // public float peacefulness;
            // public float excitement;
        }

        [Serializable]
        public struct Social
        {
            // [Serializable]
            // public struct Need
            // {
            //     public float interest;
            //     public float hygiene;
            //     public float liveliness;
            // }

            public List<Relationship> relationships;
        }

        public Personality personality;
        public Mood mood;
        public Social social;
    }
}