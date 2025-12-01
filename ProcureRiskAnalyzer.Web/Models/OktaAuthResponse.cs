using System.Text.Json.Serialization;

namespace ProcureRiskAnalyzer.Web.Models;

public class OktaAuthResponse
{
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("sessionToken")]
    public string? SessionToken { get; set; }

    [JsonPropertyName("expiresAt")]
    public DateTime? ExpiresAt { get; set; }

    [JsonPropertyName("_embedded")]
    public OktaAuthEmbedded? Embedded { get; set; }
}

public class OktaAuthEmbedded
{
    [JsonPropertyName("user")]
    public OktaUser? User { get; set; }
}

public class OktaUser
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("profile")]
    public OktaUserProfile? Profile { get; set; }
}

public class OktaUserProfile
{
    [JsonPropertyName("login")]
    public string Login { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string? Email { get; set; }
}

