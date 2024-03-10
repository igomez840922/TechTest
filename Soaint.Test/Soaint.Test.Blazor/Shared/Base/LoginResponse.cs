namespace Soaint.Test.Blazor.Shared.Base;

public class LoginResponse
{
    public bool Succesful { get; set; }
    public string? Error { get; set; }
    public string? Token { get; set; }
}