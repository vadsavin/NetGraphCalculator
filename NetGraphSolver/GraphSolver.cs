namespace NetGraphSolver
{
    public class GraphSolver
    {
        public IList<GraphJob> Jobs { get; }

        public GraphSolver(List<GraphJob> jobs) 
        {
            Jobs = jobs;
        }

        public void CalculateAll()
        {
            CalculatePreviousCount();
            CalculateEarlyStartEnd();
            CalculateLateStartEnd();
            CalculateReserve();
        }

        public IEnumerable<GraphJob> GetCriticalRoute()
        {
            return Jobs.Where(j => j.Reserve == 0).OrderBy(j => j.StartNode);
        }

        public void CalculatePreviousCount()
        {
            foreach (var job in Jobs)
            {
                job.PreviousCount = Jobs.Where(j => j.EndNode == job.StartNode).Count();
            }
        }

        public void CalculateEarlyStartEnd()
        {
            // Инициализация
            foreach (var job in Jobs)
            {
                job.EarlyStart = 0;
                job.EarlyEnd = 0;
            }

            // Вычисление ранних сроков
            for (int i = 0; i < Jobs.Count; i++)
            {
                foreach (var job in Jobs)
                {
                    // Проверка предшественников
                    bool hasPredecessor = false;
                    foreach (var predecessor in Jobs)
                    {
                        if (predecessor.EndNode == job.StartNode)
                        {
                            hasPredecessor = true;
                            break;
                        }
                    }

                    // Раннее начало = Макс(Раннее окончание предшественников)
                    if (!hasPredecessor)
                    {
                        job.EarlyStart = 0;
                    }
                    else
                    {
                        job.EarlyStart = Math.Max(Jobs.Where(j => j.EndNode == job.StartNode).Select(j => j.EarlyEnd).Max(), job.EarlyStart);
                    }

                    // Раннее окончание = Раннее начало + Длительность
                    job.EarlyEnd = job.EarlyStart + job.Duration;
                }
            }
        }

        public int CalculateCriticalRoute()
        {
            return Jobs.MaxBy(j => j.EarlyEnd).EarlyEnd;
        }

        public void CalculateLateStartEnd()
        {
            // Инициализация
            foreach (var job in Jobs)
            {
                job.LateStart = int.MaxValue;
                job.LateEnd = int.MaxValue;
            }

            var criticalRoute = CalculateCriticalRoute();

            var graphEndNode = Jobs.MaxBy(j => j.EndNode).EndNode;
            foreach (var job in Jobs.Where(j => j.EndNode == graphEndNode))
            {
                job.LateEnd = criticalRoute;
                job.LateStart = job.LateEnd - job.Duration;
            }

            foreach (var job in Jobs.OrderByDescending(j => j.EndNode))
            {
                if (job.LateEnd != int.MaxValue)
                {
                    continue;
                }

                var successors = Jobs.Where(j => j.StartNode == job.EndNode);

                job.LateEnd = successors.MinBy(s => s.LateStart).LateStart;
                job.LateStart = job.LateEnd - job.Duration;
            }
        }

        public void CalculateReserve()
        {
            foreach (var job in Jobs)
            {
                job.Reserve = job.LateStart - job.EarlyStart;
            }
        }
    }
}