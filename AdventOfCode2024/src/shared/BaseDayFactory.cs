namespace AdventOfCode2024
{
    public interface IBaseDayFactory<TBaseDay> where TBaseDay : BaseDay
    {
        TBaseDay CreateInstance(string[]? inputLines = null);
    }

    public class BaseDayFactory<TBaseDay> : IBaseDayFactory<TBaseDay>
        where TBaseDay : BaseDay
    {
        private readonly Func<string[]?, TBaseDay> _createInstance;

        public BaseDayFactory(Func<string[]?, TBaseDay> createInstance)
        {
            _createInstance = createInstance;
        }

        public TBaseDay CreateInstance(string[]? inputLines = null)
        {
            return _createInstance(inputLines);
        }
    }
}