using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace func.brainfuck
{
    public class BrainfuckBasicCommands
    {
        public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
        {
            vm.RegisterCommand('.', b => { write((char)b.Memory[b.MemoryPointer]); });
            vm.RegisterCommand('+', b =>
            {
                if (b.Memory[b.MemoryPointer] < 255) b.Memory[b.MemoryPointer]++;
                else b.Memory[b.MemoryPointer] = 0;
            });
            vm.RegisterCommand('-', b =>
            {
                if (b.Memory[b.MemoryPointer] != 0) b.Memory[b.MemoryPointer]--;
                else b.Memory[b.MemoryPointer] = 255;
            });
            vm.RegisterCommand(',', b => { b.Memory[b.MemoryPointer] = (byte)read(); });
            vm.RegisterCommand('>', b =>
            {
                if (b.MemoryPointer < b.Memory.Length - 1) b.MemoryPointer++;
                else b.MemoryPointer = 0;
            });
            vm.RegisterCommand('<', b =>
            {
                if (b.MemoryPointer > 0) b.MemoryPointer--;
                else b.MemoryPointer = b.Memory.Length - 1;
            });
            RegisterCommandsASCII(vm);
        }

        private static void RegisterCommandsASCII(IVirtualMachine vm)
        {
            Action<char> registerCommandASCII = x => vm.RegisterCommand(x, b =>
            {
                var value = (byte)x;
                b.Memory[b.MemoryPointer] = value;
            });
            for (var i = 'a'; i <= 'z'; i++)
                registerCommandASCII(i);
            for (var i = 'A'; i <= 'Z'; i++)
                registerCommandASCII(i);
            for (var i = '0'; i <= '9'; i++)
                registerCommandASCII(i);
        }
    }
}