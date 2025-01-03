﻿using System.Collections.Generic;

namespace BattleSystem.Model
{
    public class Troop
    {
        public int Id { get; set; }
        public List<TroopMember> Members { get; set; }
        public List<PlayerSpot> PlayerSpots { get; set; }
        public int BattlefieldId { get; set; }

        public Troop(int id, List<TroopMember> members, List<PlayerSpot> playerSpots, int battlefieldId)
        {
            Id = id;
            Members = members;
            PlayerSpots = playerSpots;
            BattlefieldId = battlefieldId;
        }
    }
}
