using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using SupaCharge.Core.Converter;

namespace SupaCharge.Core.ConfigurationWrapper {
  public class ConfigurationWrapper {

    public bool Contains(string key) {
      if (ConfigurationSettings.AppSettings[key] != null) 
        return true;
      return false;
    }

    public T Get<T>(string key) {
      var c = new ValueConverter();
      return c.Get<T>(ConfigurationSettings.AppSettings[key]);
    }

    public T Get<T>(string key, T defValue) {
      var c = new ValueConverter();
      
      if (ConfigurationSettings.AppSettings[key] == null)
        return defValue;
      return c.Get<T>(ConfigurationSettings.AppSettings[key]);
    }
  }
}
