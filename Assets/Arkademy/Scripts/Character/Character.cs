using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Arkademy.Pawns;
using Arkademy.Spells;

namespace Arkademy.Characters {

    public class Character {
        private readonly int _id;
        private readonly string _name;
        private float _mana;
        private float _health;
        private float _exp;
        private List<ISpell> _spells;
    }

}