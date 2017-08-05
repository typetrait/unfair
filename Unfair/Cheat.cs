using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unfair.Core;
using Unfair.Game;
using Unfair.Input;
using Unfair.Modules;

namespace Unfair
{
    public class Cheat
    {
        public delegate void ClosedEventHandler(object sender, EventArgs e);
        public event ClosedEventHandler Closed;

        private const byte MaxPlayers = 64;
        private const VirtualKeyShort ExitKey = VirtualKeyShort.INSERT;

        public readonly ProcessMemory Memory;
        public readonly List<Module> Modules;
        private readonly Keyboard keyboard;

        public LocalPlayer LocalPlayer { get; private set; }
        public List<Player> Players { get; private set; }
        public bool IsRunning { get; private set; }
        public ProcessModule ClientModule { get; private set; }
        public ProcessModule EngineModule { get; private set; }

        public Cheat()
        {
            Memory = new ProcessMemory();
            Modules = new List<Module>()
            {
                new GlowModule(this, VirtualKeyShort.F1)
            };
            keyboard = new Keyboard();
        }

        public bool Hook(string processName)
        {
            if (!Memory.Attach(processName))
                return false;

            ClientModule = Memory.GetModule("client.dll");
            EngineModule = Memory.GetModule("engine.dll");

            return ClientModule != null;
        }

        public void Start()
        {
            IsRunning = true;
            while (IsRunning)
            {
                keyboard.Update();

                // Update local player
                int localPlayerAddress = Memory.ReadInt32(ClientModule.BaseAddress.ToInt32() + Offsets.dwLocalPlayer);
                LocalPlayer = new LocalPlayer(this, localPlayerAddress);
                LocalPlayer.Read();

                // Update other players
                Players = new List<Player>();
                for (byte i = 0; i < MaxPlayers; i++)
                {
                    int currentPlayerAddress = Memory.ReadInt32(ClientModule.BaseAddress.ToInt32() + Offsets.dwEntityList + i * 0x10);
                    Players.Add(new Player(this, currentPlayerAddress));
                    Players[i].Read(i);
                }

                foreach (var module in Modules)
                {
                    if (keyboard.KeyWentDown(module.Bind))
                        module.Toggle();

                    if (module.IsToggled)
                        module.Update();
                }

                if (keyboard.KeyWentDown(ExitKey))
                    IsRunning = false;

                Thread.Sleep(1);
            }

            OnClosed(this, EventArgs.Empty);
        }

        private void OnClosed(object sender, EventArgs e)
        {
            Closed?.Invoke(sender, e);
        }
    }
}