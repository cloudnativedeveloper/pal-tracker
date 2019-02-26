using System;
using System.Collections.Generic;

namespace PalTracker
{
    public class OperationCounter<T> : IOperationCounter<T>
    {
        public IDictionary<TrackedOperation, int> GetCounts { get; }

        public string Name => $"{typeof(T).Name}Operations";

        public OperationCounter()
        {
            GetCounts = new Dictionary<TrackedOperation, int>();
            foreach (TrackedOperation operation in Enum.GetValues(typeof(TrackedOperation)))
            {
                GetCounts.Add(operation, 0);
            }
        }

        public void Increment(TrackedOperation operation)
        {
            GetCounts[operation]++;
        }
    }
}
