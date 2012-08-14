namespace SupaCharge.Core.Text.Extensions {
  public static class IsNullOrEmptyTrimExtension {
    public static bool IsNullOrEmptyTrim(this string item) {
      return item == null || item.Trim().Length == 0;
    }
  }
}