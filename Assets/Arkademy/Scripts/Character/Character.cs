using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Arkademy.Pawns;
using Arkademy.Spells;

namespace Arkademy.Characters {

    [Serializable]
    public class Character {

        private readonly int _id;
        private readonly string _name;
        private float _mana;
        private float _health;
        private float _exp;
        private List<ISpell> _spells = new List<ISpell>();
        public int Id { get {return _id; }}
        public string Name { get {return _name; }}
        public float Mana { get {return _mana; }}
        public float Health { get {return _health; }}
        public float Exp { get {return _exp; }}
        public List<ISpell> Spells { get {return _spells; }}

        public Character(int id, string name, List<ISpell> defaultSpells){
            _id = id;
            _name = name;
            _spells.AddRange(defaultSpells);
        }        
    }

}