using Assembly = System.Reflection.Assembly;

namespace AdventOfCode2024
{
    public abstract class BaseProblem
    {
        protected virtual string ClassPrefix { get; } = "Problem";

        protected virtual string InputFileDirPath { get; } = "Inputs";

        protected virtual string InputFileExtension { get; } = ".txt";

  
        public virtual uint CalculateIndex()
        {
            string typeName = GetType().Name;

            return uint.TryParse(typeName[(typeName.IndexOf(ClassPrefix) + ClassPrefix.Length)..].TrimStart('_'), out uint index)
                ? index
                : default;
        }

        public virtual string InputFilePath()
        {
            string index = CalculateIndex().ToString("D2");
            string assemblyPath = Assembly.GetExecutingAssembly().Location;
            var projectRoot = Path.GetDirectoryName(assemblyPath);

            if (projectRoot is null)
            {
                throw new Exception("Unable to determine project root.");
            }

            return Path.Combine(projectRoot, InputFileDirPath, $"Day{index}", $"Day{index}.{InputFileExtension.TrimStart('.')}");
        } 

        public abstract ValueTask<int> Solve_1();

        public abstract ValueTask<int> Solve_2();
    }
}