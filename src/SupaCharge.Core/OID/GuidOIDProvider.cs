using System;

namespace SupaCharge.Core.OID {
  public class GuidOIDProvider : IOIDProvider {
    public string GetID() {
      return Guid.NewGuid().ToString("N");
    }
  }
}