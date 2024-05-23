using Cooking.Domain.Users;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Cooking.Api.Controllers;

public class CreateUserRequest
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    [JsonConverter(typeof(StringEnumConverter))]
    public Role Role { get; set; }
}