using System;

namespace SupaCharge.Core.SystemAbstractions {
  public class DotNetConsole : ISystemConsole {
    public void WriteLine(string format, params object[] args) {
      Console.WriteLine(format, args);
    }

    public void WriteLine(params string[] messages) {
      Array.ForEach(messages, Console.WriteLine);
    }

    public void WriteLineColored(ConsoleColor color, string format, params object[] args) {
      var currentColor = Console.ForegroundColor;
      Console.ForegroundColor = color;
      try {
        WriteLine(format, args);
      } finally {
        Console.ForegroundColor = currentColor;
      }
    }

    public void WriteLineColored(ConsoleColor color, params string[] messages) {
      var currentColor = Console.ForegroundColor;
      Console.ForegroundColor = color;
      try {
        WriteLine(messages);
      } finally {
        Console.ForegroundColor = currentColor;
      }
    }
  }
}