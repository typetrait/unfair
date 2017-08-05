using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unfair.Game
{
    /// <summary>
    /// Represents the local (client) player.
    /// </summary>
    public class LocalPlayer : Player
    {
        public Vector Origin { get; private set; }

        public LocalPlayer(Cheat cheat, int address) : base(cheat, address)
        {
        }

        public void Read()
        {
            Address = cheat.Memory.ReadInt32(cheat.ClientModule.BaseAddress.ToInt32() + Offsets.dwLocalPlayer);

            Health = cheat.Memory.ReadInt32(Address + Offsets.m_iHealth);
            Team = cheat.Memory.ReadInt32(Address + Offsets.m_iTeamNum);
            Origin = cheat.Memory.Read<Vector>(Address + Offsets.m_vecOrigin);
        }
    }
}