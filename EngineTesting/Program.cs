using StrategyEngine;
using StrategyEngine.Interfaces;

namespace StrategyEngine
{
    public class EngineTester
    {
        private IUserContext _context;
        private List<IAction> _actions;

        public EngineTester()
        {
            _context = ContextFactory.Create(new string[] { "gold" });
            UpdateActions();
        }

        public void WriteActions()
        {
            Console.WriteLine("Actions:");
            Console.WriteLine(
                string.Join(
                    "\n", 
                    Enumerable.Range(0, _actions.Count)
                        .Select(_ => $"{_}. {_actions[_].ActionName}")
                )
            );
            Console.WriteLine();
        }

        public void ExecuteAction(int index)
        {
            _actions[index].Execute();
            UpdateActions();
        }

        public void WriteConstructions()
        {
            Console.WriteLine("Constructions:");
            Console.WriteLine(string.Join("\n", _context.Constructions.GetConstructions().Select(_ => _.GetType().Name)));
            Console.WriteLine();
        }

        public void WriteResources()
        {
            Console.WriteLine($"Resources:\ngold: {_context.Resources.GetAmount("gold")}\n");
        }

        private void UpdateActions()
        {
            _actions = _context.GetActions().ToList();
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var tester = new EngineTester();
            while (true)
            {
                tester.WriteResources();
                tester.WriteConstructions();
                tester.WriteActions();
                var actionIndex = int.Parse(Console.ReadLine());
                tester.ExecuteAction(actionIndex);
            }
        }
    }
}