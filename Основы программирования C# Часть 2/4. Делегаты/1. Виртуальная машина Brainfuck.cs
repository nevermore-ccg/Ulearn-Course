using System;
using System.Collections.Generic;

namespace func.brainfuck
{
    public class VirtualMachine : IVirtualMachine
    {
        public string Instructions { get; }
        public int InstructionPointer { get; set; }
        public byte[] Memory { get; }
        public int MemoryPointer { get; set; }
        public Dictionary<char, Action<IVirtualMachine>> Commands { get; }

        public VirtualMachine(string program, int memorySize)
        {
            Instructions = program;
            Memory = new byte[memorySize];
            Commands = new Dictionary<char, Action<IVirtualMachine>>();
        }

        public void RegisterCommand(char symbol, Action<IVirtualMachine> execute) => Commands[symbol] = execute;

        public void Run()
        {
            while (InstructionPointer < Instructions.Length)
            {
                var commandSymbol = Instructions[InstructionPointer];
                if (Commands.ContainsKey(commandSymbol))
                    Commands[commandSymbol](this);
                InstructionPointer++;
            }
        }
    }
}