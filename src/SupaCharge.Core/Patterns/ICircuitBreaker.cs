using System;

namespace SupaCharge.Core.Patterns {
  public interface ICircuitBreaker<T> {
    T Execute(Func<T> work);
  }
}