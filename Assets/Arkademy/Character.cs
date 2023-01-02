using System;
using System.Collections.Generic;
using Arkademy.Dialogue;

namespace Arkademy
{
    [Serializable]
    public class Character
    {
        public string id;
        public string givenName;


        public int strength;
        public int dexterity;
        public int constitution;
        public int intelligence;
        public int wisdom;
        public int charisma;
        
        [Serializable]
        public struct Relationship
        {
            public string charId;
            public int familiarness;
            public int affinity;
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
            public int happiness;
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

        [Serializable]
        public struct Communication
        {
            public List<DialogueCommand> commands;
            public List<DialogueCommand> responds;
        }

        public Personality personality;
        public Mood mood;
        public Social social;
        public Communication communication;
    }
}