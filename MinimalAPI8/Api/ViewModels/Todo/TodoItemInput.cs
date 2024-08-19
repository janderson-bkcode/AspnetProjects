namespace Api.ViewModels.Todo;

public class TodoItemInput
{
    public string? Title { get; set; }
    public bool IsCompleted { get; set; }

    public class TodoItemInputValidator : AbstractValidator<TodoItemInput>
    {
        private readonly TodoDbContext _todoDbContext;

        public TodoItemInputValidator(IDbContextFactory<TodoDbContext> dbContextFactory)
        {
            _todoDbContext = dbContextFactory.CreateDbContext();

            RuleFor(t => t.Title).NotEmpty().MaximumLength(100).MinimumLength(3)
                .Must(IsUnique).WithMessage("Title should be unique.");
        }

        private bool IsUnique(string title)
        {
            return !_todoDbContext.TodoItems.Any(t => t.Title == title);
        }
    }
}