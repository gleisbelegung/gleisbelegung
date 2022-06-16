using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using Gleisbelegung.App.Common;
using Gleisbelegung.App.Data;
using Gleisbelegung.App.Events;
using Gleisbelegung.App.STSConnect.Messages;

namespace Gleisbelegung.App.STSConnect.MessageProcessors
{
    public class PlatformListMessageProcessor : IMessageProcessor, IEventListener<IncomingMessageEvent<PlatformListMessage>>
    {
        public PlatformListMessageProcessor()
        {
            this.RegisterSubscriptions();
        }

        public void ProcessEvent(IncomingMessageEvent<PlatformListMessage> eventData)
        {
            var platforms = Database.Instance.Platforms;

            // create platforms (if necessary)
            foreach (var platformData in eventData.Message.Bahnsteige)
            {
                if (platforms.ContainsKey(platformData.Name))
                    continue;

                var platform = new Platform
                {
                    Id = platforms.Count + 1,
                    Name = platformData.Name,
                };
                platforms.Add(platform.Name, platform);
            }

            // determine their neighbors
            foreach (var platformData in eventData.Message.Bahnsteige)
            {
                var platform = platforms[platformData.Name];

                platform.Neighbors = new List<Platform>();
                foreach (var neighborData in platformData.Nachbarn)
                {
                    var neighbor = platforms[neighborData.Name];
                    platform.Neighbors.Add(neighbor);
                }
            }
        }
    }
}