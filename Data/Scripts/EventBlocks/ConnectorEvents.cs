using System; 
using System.Collections.Generic; 
using VRage.Game.Components;
using Sandbox.Common.ObjectBuilders;
using Sandbox.ModAPI.Ingame;

namespace EventBlocks {

    [MyEntityComponentDescriptor(typeof(MyObjectBuilder_ShipConnector), false)] // False means we use new update system and don't get switched off when a block stops updating.
    public class ConnectorEvents : EventBlock<MyShipConnectorStatus> {


        protected override MyShipConnectorStatus GetState() {
            return (Entity as Sandbox.ModAPI.IMyShipConnector).Status;
        }

        protected override List<String> GetNewEvents(MyShipConnectorStatus state, MyShipConnectorStatus oldState) {
            switch(state) {
                case MyShipConnectorStatus.Connected:
                    return new List<String>() { "Connected" };
                case MyShipConnectorStatus.Unconnected:
                    return new List<String>() { "Unconnected" };
                case MyShipConnectorStatus.Connectable:
                    if (oldState == MyShipConnectorStatus.Unconnected)
                        return new List<String>() { "Connectable" };
                    else
                        return null;
                default:
                    return null;
            }
        }
    }
}