namespace SubInfrastructure.Api;

public interface IApiAccess
{
	Task<IEnumerable<T>> QueryMultiple<T>(string url, string token = "");
	Task<T?> QuerySingle<T>(string url, string token = "");
	Task<T?> PostData<T,U>(string url, U parameter, string token = "");
	Task<T?> UpdateData<T,U>(string url, U parameter, string token = "");
	Task<T?> DeleteData<T>(string url, string token = "");
}
