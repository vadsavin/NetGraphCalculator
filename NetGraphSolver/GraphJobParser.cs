namespace NetGraphSolver
{
    public class GraphJobParser
    {
        public static List<GraphJob> ParseGraphJobs(string input)
        {
            var jobs = new List<GraphJob>();

            // Разбиваем строку на отдельные задачи
            var jobStrings = input.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var jobString in jobStrings)
            {
                // Разбиваем строку задачи на части
                var parts = jobString.Trim().Split(new[] { '-', ',', ' ' });

                if (parts.Length != 3)
                {
                    throw new ArgumentException($"Неверный формат задачи: {jobString}");
                }

                // Парсим значения
                var startNode = int.Parse(parts[0]);
                var endNode = int.Parse(parts[1]);
                var duration = int.Parse(parts[2]);

                // Создаем и добавляем объект задачи
                jobs.Add(new GraphJob
                {
                    StartNode = startNode,
                    EndNode = endNode,
                    Duration = duration,
                });
            }

            return jobs;
        }
    }
}
