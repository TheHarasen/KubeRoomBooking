namespace SubApplication.Options;

public class CorsOptions
{
	public const string CorsOptionsKey = "CorsOptions";

	public string[] AllowedOrigins { get; set; }
}
