namespace JWT_BASIC_TOKEN_DOT_NET_CORE_8
{
    public class Jwtsettings
    {
        public string Secretkey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string ExpiryMinutes { get; set; }
    }
}
