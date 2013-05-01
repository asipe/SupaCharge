using System.Net;

namespace SupaCharge.Core.IOAbstractions {
  public class DotNetDns : IDns {
    public string GetIPAddress(string hostName) {
      return Dns.GetHostAddresses(hostName)[0].ToString();
    }
  }
}