namespace Soaint.Test.Blazor.Shared.Base;

public class RegisterResponse
{
    public bool Succesful { get; set; }
    public IEnumerable<string> Errors { get; set; }
}