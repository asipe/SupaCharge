using System;
using System.IO;

namespace SupaCharge.Core.ConsoleAbstractions {
  public class DotNetConsole : IConsole {
    public TextWriter Error {
      get {return Console.Error;}
    }

    public TextReader In {
      get {return Console.In;}
    }

    public TextWriter Out {
      get {return Console.Out;}
    }

    public ConsoleColor BackgroundColor {
      get {return Console.BackgroundColor;}
      set {Console.BackgroundColor = value;}
    }

    public ConsoleColor ForegroundColor {
      get {return Console.ForegroundColor;}
      set {Console.ForegroundColor = value;}
    }

    public void WriteLine() {
      Console.WriteLine();
    }

    public void WriteLine(bool value) {
      Console.WriteLine(value);
    }

    public void WriteLine(char value) {
      Console.WriteLine(value);
    }

    public void WriteLine(char[] buffer) {
      Console.WriteLine(buffer);
    }

    public void WriteLine(decimal value) {
      Console.WriteLine(value);
    }

    public void WriteLine(double value) {
      Console.WriteLine(value);
    }

    public void WriteLine(int value) {
      Console.WriteLine(value);
    }

    public void WriteLine(long value) {
      Console.WriteLine(value);
    }

    public void WriteLine(object value) {
      Console.WriteLine(value);
    }

    public void WriteLine(float value) {
      Console.WriteLine(value);
    }

    public void WriteLine(string value) {
      Console.WriteLine(value);
    }

    public void WriteLine(uint value) {
      Console.WriteLine(value);
    }

    public void WriteLine(ulong value) {
      Console.WriteLine(value);
    }

    public void WriteLine(string format, object arg0) {
      Console.WriteLine(format, arg0);
    }

    public void WriteLine(string format, params object[] arg) {
      Console.WriteLine(format, arg);
    }

    public void WriteLine(char[] buffer, int index, int count) {
      Console.WriteLine(buffer, index, count);
    }

    public void WriteLine(string format, object arg0, object arg1) {
      Console.WriteLine(format, arg0, arg1);
    }

    public void WriteLine(string format, object arg0, object arg1, object arg2) {
      Console.WriteLine(format, arg0, arg1, arg2);
    }

    public void WriteLine(string format, object arg0, object arg1, object arg2, object arg3) {
      Console.WriteLine(format, arg0, arg1, arg3);
    }

    public void Write(bool value) {
      Console.Write(value);
    }

    public void Write(char value) {
      Console.Write(value);
    }

    public void Write(char[] buffer) {
      Console.Write(buffer);
    }

    public void Write(decimal value) {
      Console.Write(value);
    }

    public void Write(double value) {
      Console.Write(value);
    }

    public void Write(int value) {
      Console.Write(value);
    }

    public void Write(long value) {
      Console.Write(value);
    }

    public void Write(object value) {
      Console.Write(value);
    }

    public void Write(float value) {
      Console.Write(value);
    }

    public void Write(string value) {
      Console.Write(value);
    }

    public void Write(uint value) {
      Console.Write(value);
    }

    public void Write(ulong value) {
      Console.Write(value);
    }

    public void Write(string format, object arg0) {
      Console.Write(format, arg0);
    }

    public void Write(string format, params object[] arg) {
      Console.Write(format, arg);
    }

    public void Write(char[] buffer, int index, int count) {
      Console.Write(buffer, index, count);
    }

    public void Write(string format, object arg0, object arg1) {
      Console.Write(format, arg0, arg1);
    }

    public void Write(string format, object arg0, object arg1, object arg2) {
      Console.Write(format, arg0, arg1, arg2);
    }

    public void Write(string format, object arg0, object arg1, object arg2, object arg3) {
      Console.Write(format, arg0, arg1, arg2, arg3);
    }

    public string ReadLine() {
      return Console.ReadLine();
    }

    public ConsoleKeyInfo ReadKey() {
      return Console.ReadKey();
    }

    public ConsoleKeyInfo ReadKey(bool intercept) {
      return Console.ReadKey(intercept);
    }

    public void ResetColor() {
      Console.ResetColor();
    }
  }
}