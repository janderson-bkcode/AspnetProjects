namespace Api.ViewModels.Todo;

public class UserInput
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    
    public class UserInputValidator : AbstractValidator<UserInput>
    {
        public UserInputValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}