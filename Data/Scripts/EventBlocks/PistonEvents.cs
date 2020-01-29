using System; 
using System.Collections.Generic; 
using VRage.Game.Components;
using Sandbox.Common.ObjectBuilders;
using Sandbox.ModAPI.Ingame;

namespace EventBlocks {

    [MyEntityComponentDescriptor(typeof(MyObjectBuilder_ExtendedPistonBase), false)] // False means we use new update system and don't get switched off when a block stops updating.
    public class PistonBaseEvents : EventBlock<PistonStatus> {


        protected override PistonStatus GetState() {
            return (Entity as Sandbox.ModAPI.IMyExtendedPistonBase).Status;
        }

        protected override List<String> GetNewEvents(PistonStatus state, PistonStatus oldState) {
            switch(state) {
                case PistonStatus.Extended:
                    return new List<String>() { "Extended" };
                case PistonStatus.Retracted:
                    return new List<String>() { "Retracted" };
                default:
                    return null;
            }
        }
    }
}