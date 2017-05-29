namespace PoGoEmulator.Models
{
    public class CacheUserData
    {
        public bool IsAuthenticated { get; set; }
        public bool HasSignature { get; set; }
        public bool IsIOS { get; set; }

        public bool IsAndroid
        {
            get
            {
                return !IsIOS;
            }
        }

        public string Platform
        {
            get
            {
                return IsIOS ? "ios" : "android";
            }
        }
    }
}