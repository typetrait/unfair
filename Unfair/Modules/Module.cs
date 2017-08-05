using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unfair.Input;

namespace Unfair.Modules
{
    /// <summary>
    /// Represents a toggleable cheat feature.
    /// </summary>
    public abstract class Module
    {
        public readonly Cheat Cheat;

        public VirtualKeyShort Bind { get; set; }
        public string Name { get; set; }
        public bool IsToggled { get; private set; }

        public Module(Cheat cheat, VirtualKeyShort bind, string name)
        {
            Cheat = cheat;
            Bind = bind;
            Name = name;
            IsToggled = false;
        }

        public void Toggle()
        {
            IsToggled = !IsToggled;
        }

        public abstract void Update();
    }
}