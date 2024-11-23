using Assembly = System.Reflection.Assembly;

namespace AdventOfCode2024;

public abstract class BaseProblem
{
    protected virtual string ClassPrefix { get; } = "Problem";

    protected virtual string InputFileDirPath { get; } = "Inputs";
 
    protected virtual string InputFileExtension { get; } = ".txt";

  
    public virtual uint CalculateIndex()
    {
        var typeName = GetType().Name;

        return uint.TryParse(typeName[(typeName.IndexOf(ClassPrefix) + ClassPrefix.Length)..].TrimStart('_'), out var index)
            ? index
            : default;
    }

    public virtual string InputFilePath(int part)
    {
        var index = CalculateIndex().ToString("D2");
        var partSuffix = $"_Part{part}";
        string assemblyPath = Assembly.GetExecutingAssembly().Location;
        string projectRoot = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(assemblyPath), "..", ".."));
        return Path.Combine(projectRoot, InputFileDirPath, $"Day{index}", $"Day{index}{partSuffix}.{InputFileExtension.TrimStart('.')}");
    }

    public abstract ValueTask<string> Solve_1();

    public abstract ValueTask<string> Solve_2();
}