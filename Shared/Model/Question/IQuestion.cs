namespace BlazorApp.Shared.Model.Question
{
    public interface IQuestion
    {
         int Order { get; }
         string Text { get; }
         string QuestionType { get; }
    }
}