using System;

namespace SupaCharge.Core.SystemAbstractions {
  public interface ISystemConsole {
    void WriteLine(string format, params object[] args);
    void WriteLine(params string[] messages);
    void WriteLineColored(ConsoleColor color, string format, params object[] args);
    void WriteLineColored(ConsoleColor color, params string[] messages);
  }
}