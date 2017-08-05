using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unfair.Core;

namespace Unfair.Game
{
    /// <summary>
    /// Represents a player entity.
    /// </summary>
    public class Player
    {
        public int Address { get; protected set; }
        public int Index { get; protected set; }
        public int Health { get; protected set; }
        public int Team { get; protected set; }

        protected readonly Cheat cheat;

        public Player(Cheat cheat, int address)
        {
            this.cheat = cheat;
        }

        public void Read(int index)
        {
            Index = index;

            Address = cheat.Memory.ReadInt32(cheat.ClientModule.BaseAddress.ToInt32() + Offsets.dwEntityList + Index * 0x10);

            Health = cheat.Memory.ReadInt32(Address + Offsets.m_iHealth);
            Team = cheat.Memory.ReadInt32(Address + Offsets.m_iTeamNum);
        }
    }
}