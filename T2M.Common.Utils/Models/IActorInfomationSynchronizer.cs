using System;

namespace T2M.Common.Utils.Models
{
    public interface IActorInfomationSynchronizer
    {

        ActorInfomationSynchronizer GetActorInfomationSynchronizer();
    }

    public class ActorInfomationSynchronizer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }
    }
}