namespace NetGraphSolver
{
    public class GraphJob
    {
        public int StartNode { get; init; }

        public int EndNode { get; init; }

        public int Duration { get; init; }

        public int EarlyStart { get; set; }

        public int EarlyEnd { get; set; }

        public int LateStart { get; set; }

        public int LateEnd { get; set; }

        public int Reserve { get; set; }

        public int PreviousCount { get; set; }

        public override string ToString()
        {
            return $"({StartNode}-{EndNode}:{Duration})";
        }
    }
}
