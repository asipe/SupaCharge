using System;
using System.IO;

namespace SupaCharge.Core.ConsoleAbstractions {
  public interface IConsole {
    TextWriter Error{get;}
    TextReader In{get;}
    TextWriter Out{get;}
    ConsoleColor BackgroundColor{get;set;}
    ConsoleColor ForegroundColor{get;set;}
    void WriteLine();
    void WriteLine(bool value);
    void WriteLine(char value);
    void WriteLine(char[] buffer);
    void WriteLine(decimal value);
    void WriteLine(double value);
    void WriteLine(int value);
    void WriteLine(long value);
    void WriteLine(object value);
    void WriteLine(float value);
    void WriteLine(string value);
    void WriteLine(uint value);
    void WriteLine(ulong value);
    void WriteLine(string format, object arg0);
    void WriteLine(string format, params object[] arg);
    void WriteLine(char[] buffer, int index, int count);
    void WriteLine(string format, object arg0, object arg1);
    void WriteLine(string format, object arg0, object arg1, object arg2);
    void WriteLine(string format, object arg0, object arg1, object arg2, object arg3);
    void Write(bool value);
    void Write(char value);
    void Write(char[] buffer);
    void Write(decimal value);
    void Write(double value);
    void Write(int value);
    void Write(long value);
    void Write(object value);
    void Write(float value);
    void Write(string value);
    void Write(uint value);
    void Write(ulong value);
    void Write(string format, object arg0);
    void Write(string format, params object[] arg);
    void Write(char[] buffer, int index, int count);
    void Write(string format, object arg0, object arg1);
    void Write(string format, object arg0, object arg1, object arg2);
    void Write(string format, object arg0, object arg1, object arg2, object arg3);
    string ReadLine();
    ConsoleKeyInfo ReadKey();
    ConsoleKeyInfo ReadKey(bool intercept);
    void ResetColor();
  }
}