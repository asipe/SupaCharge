namespace SupaCharge.Core.Net {
  public class NoOpDotNetDns : IDns {
    public string GetIPAddress(string hostName) {
      return hostName;
    }
  }
}