using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using SupaCharge.Core.Converter;

namespace SupaCharge.Core.ConfigurationWrapper {
  public class ConfigurationWrapper {

    public bool Contains(string key) {
      return (ConfigurationSettings.AppSettings[key] != null);
    }

    public T Get<T>(string key) {
      return _Converter.Get<T>(ConfigurationSettings.AppSettings[key]);
    }

    public T Get<T>(string key, T defValue) {
      if (ConfigurationSettings.AppSettings[key] == null)
        return defValue;
      return _Converter.Get<T>(ConfigurationSettings.AppSettings[key]);
    }

    private static readonly ValueConverter _Converter = new ValueConverter();
  }
}
