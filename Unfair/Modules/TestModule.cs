using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unfair.Input;

namespace Unfair.Modules
{
    public class TestModule : Module
    {
        public TestModule(Cheat cheat, VirtualKeyShort bind) : base(cheat, bind, "Test")
        {
        }

        public override void Update()
        {
            // ... this is just a test module
        }
    }
}