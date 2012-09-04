using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using SupaCharge.Core.Converter;

namespace SupaCharge.Core.ConfigurationWrapper {
  public class ConfigurationWrapper {
    public bool Contains(string key) {
      return ConfigurationManager.AppSettings[key] != null;
    }

    public T Get<T>(string key) {
      return _Converter.Get<T>(ConfigurationManager.AppSettings[key]);
    }

    public T Get<T>(string key, T defValue) {
      return ConfigurationManager.AppSettings[key] == null 
        ? defValue 
        : _Converter.Get<T>(ConfigurationManager.AppSettings[key]);
    }
    
    private static readonly ValueConverter _Converter = new ValueConverter();
  }
}
