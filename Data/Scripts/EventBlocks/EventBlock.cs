using System; 
using System.Collections.Generic; 
using VRage.Game.Components;
using Sandbox.ModAPI;
using VRage.ObjectBuilders;
using VRage.ModAPI;

namespace EventBlocks {

    public abstract class EventBlock<State> : MyGameLogicComponent {

        private State oldState;
        private IMyTerminalBlock block;
        private int remainingInitUpdates = 10;

        public override void Init(MyObjectBuilder_EntityBase objectBuilder) {
            NeedsUpdate |= MyEntityUpdateEnum.EACH_10TH_FRAME; // Must set this on MyGameLogicComponent to have update methods called

            block = Entity as IMyTerminalBlock;
        }

        protected abstract State GetState();

        protected abstract List<String> GetNewEvents(State state, State oldState);

        public override void UpdateBeforeSimulation10() {

            // Let everything settle for a few updates.
            if (remainingInitUpdates > 0) {
                remainingInitUpdates--;
                oldState = GetState();
                return;
            }

            // Get the current state of the block. If it's changed since last time, get any new events that have fired
            var state = GetState();
            List<String> newEvents = null;
            if (!EqualityComparer<State>.Default.Equals(state, oldState)) 
                newEvents = GetNewEvents(state, oldState);
            oldState = state;

            // For each event that fired during this update, look for a Timer Block with the appropriate name and trigger it.
            if (newEvents != null && newEvents.Count > 0) {
                var thisTerminal = MyAPIGateway.TerminalActionsHelper.GetTerminalSystemForGrid(block.CubeGrid);
                foreach (var evt in newEvents)
                {
                    var timer = thisTerminal.GetBlockWithName(block.CustomName + " (On" + evt + ")") as IMyTimerBlock;
                    if (timer != null) {
                        //MyAPIGateway.Utilities.ShowMessage(block.CustomName, evt);
                        timer.Trigger();
                    }
                }
            }
        }
    }
}