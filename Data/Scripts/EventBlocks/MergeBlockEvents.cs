using System; 
using System.Collections.Generic; 
using VRage.Game.Components;
using Sandbox.Common.ObjectBuilders;
using VRageMath;


namespace EventBlocks {

    [MyEntityComponentDescriptor(typeof(MyObjectBuilder_MergeBlock), false)]
    public class MergeBlockEvents : EventBlock<bool> {

        protected override bool GetState() {
            var block = Entity as SpaceEngineers.Game.ModAPI.IMyShipMergeBlock;
            return block.IsConnected 
                && block.CubeGrid.CubeExists(block.Position + ((Vector3I)block.LocalMatrix.Right));
        }

        protected override List<String> GetNewEvents(bool state, bool oldState) {
            return new List<String>() { state ? "Merge" : "Unmerge"};
        }
    }
}