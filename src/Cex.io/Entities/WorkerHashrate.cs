using System;
using System.Linq;

namespace Nextmethod.Cex
{
    public class WorkerHashrate : Hashrate
    {

        public WorkerRejects Rejected { get; internal set; }

        public override string ToString()
        {
            return string.Format("{0}, Rejected: {1}", base.ToString(), Rejected);
        }


        new internal static WorkerHashrate FromDynamic(dynamic data)
        {
            var val = Hashrate.CreateFromDynamic(data, new WorkerHashrate());
            val.Rejected = WorkerRejects.FromDynamic(data["rejected"]);
            return val;
        }

    }

    public class WorkerRejects
    {

        public long Stale { get; internal set; }

        public long Duplicate { get; internal set; }

        public long LowDiff { get; internal set; }

        public override string ToString()
        {
            return string.Format("Stale: {0}, Duplicate: {1}, LowDiff: {2}", Stale, Duplicate, LowDiff);
        }

        internal static WorkerRejects FromDynamic(dynamic data)
        {
            return new WorkerRejects
            {
                Stale = Convert.ToInt64(data["stale"]),
                Duplicate = Convert.ToInt64(data["duplicate"]),
                LowDiff = Convert.ToInt64(data["lowdiff"])
            };
        }

    }
}
