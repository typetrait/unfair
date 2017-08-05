using Unfair.Game;
using Unfair.Input;

namespace Unfair.Modules
{
    public class GlowModule : Module
    {
        private Player localPlayer;
        private int glowObjectArray;
        private int glowObjectCount;

        private Vector CounterTerroristColor;
        private Vector TerroristColor;

        public GlowModule(Cheat cheat, VirtualKeyShort bind) : base(cheat, bind, "Glow ESP")
        {
            CounterTerroristColor = new Vector(0.44f, 0.56f, 0.80f);
            TerroristColor = new Vector(0.99f, 0.92f, 0.48f);
        }

        public override void Update()
        {
            localPlayer = Cheat.LocalPlayer;

            glowObjectArray = Cheat.Memory.ReadInt32(Cheat.ClientModule.BaseAddress.ToInt32() + Offsets.dwGlowObjectManager);
            glowObjectCount = Cheat.Memory.ReadInt32(glowObjectArray + 0x4);

            for (int i = 0; i < Cheat.Players.Count; i++)
            {
                var currentPlayer = Cheat.Players[i];
                int currentPlayerGlowIndex = Cheat.Memory.ReadInt32(currentPlayer.Address + Offsets.m_iGlowIndex);

                if (currentPlayer.Team == (int)Team.None)
                    continue;

                int index = currentPlayerGlowIndex * 0x38;

                Vector color = new Vector();

                if (currentPlayer.Team == (int)Team.CounterTerrorist)
                    color = CounterTerroristColor;

                else if (currentPlayer.Team == (int)Team.Terrorist)
                    color = TerroristColor;

                Cheat.Memory.WriteFloat((glowObjectArray + (index + 0x4)), color.x);
                Cheat.Memory.WriteFloat((glowObjectArray + (index + 0x8)), color.y);
                Cheat.Memory.WriteFloat((glowObjectArray + (index + 0xC)), color.z);
                Cheat.Memory.WriteFloat((glowObjectArray + (index + 0x10)), 0.8f);
                Cheat.Memory.WriteBool((glowObjectArray + (index + 0x24)), true);
                Cheat.Memory.WriteBool((glowObjectArray + (index + 0x25)), false);
            }
        }
    }
}