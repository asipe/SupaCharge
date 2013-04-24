namespace SupaCharge.Core.IOAbstractions {
  public class NoOpDotNetDns : IDns {
    public string GetIPAddress(string hostName) {
      return hostName;
    }
  }
}