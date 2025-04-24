using System.Collections.Generic;
using System.Net.Http.Headers;

namespace func.brainfuck
{
    public class BrainfuckLoopCommands
    {
        public static void RegisterTo(IVirtualMachine vm)
        {
            var positions = new Dictionary<int, int>();
            var stack = new Stack<int>();
            for (int i = 0; i < vm.Instructions.Length; i++)
            {
                if (vm.Instructions[i] == '[') stack.Push(i);
                else if (vm.Instructions[i] == ']')
                {
                    positions[i] = stack.Peek();
                    positions[stack.Pop()] = i;
                }
            }
            vm.RegisterCommand('[', b =>
            {
                if (b.Memory[b.MemoryPointer] == 0)
                    b.InstructionPointer = positions[b.InstructionPointer];
            });
            vm.RegisterCommand(']', b =>
            {
                if (b.Memory[b.MemoryPointer] != 0)
                    b.InstructionPointer = positions[b.InstructionPointer];
            });
        }
    }
}