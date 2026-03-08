using HashTableDemo.Models;

namespace HashTableDemo.Data;

public static class ApiRequestFactory
{
    public static IReadOnlyList<ApiRequest> CreateInitialRequests()
    {
        return
        [
            new ApiRequest("key_live_alpine", "/v1/reports"),
            new ApiRequest("key_live_beacon", "/v1/orders"),
            new ApiRequest("key_invalid_unknown", "/v1/customers")
        ];
    }
}