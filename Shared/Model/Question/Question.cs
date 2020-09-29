namespace BlazorApp.Shared.Model.Question
{
    public class Question : IQuestion
    {
        public int Order { get; set; }
        public string Text { get; set; }
        public string QuestionType { get; set; }
    }
}