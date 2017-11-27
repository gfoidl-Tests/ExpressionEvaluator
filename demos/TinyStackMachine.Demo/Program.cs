using System.IO;

namespace TinyStackMachine.Demo
{
    static class Program
    {
        static void Main(string[] args)
        {
            string tsmFile0 = Path.ChangeExtension(Path.Combine(args[0], args[1]), "tsm");
            string tsmFile1 = Path.ChangeExtension(Path.Combine(args[0], args[2]), "tsm");

            var vm = new VirtualMachine(args: new double[] { 2.1, 1.3 });

            vm.Execute(tsmFile0);
            vm.Execute(tsmFile1);
        }
    }
}