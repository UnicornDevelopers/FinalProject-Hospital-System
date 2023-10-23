using System;
namespace Hospital_System.Models.Interfaces
{
    public interface IQuestionService
    {
        Task<List<Question>> GetQuestionsAsync();
        Task<Question> GetQuestionByIdAsync(int id);
        Task AddQuestionAsync(Question question, string userId);
        Task UpdateQuestionAsync(Question question);
        Task DeleteQuestionAsync(int id);
        Task AddAnswerAsync(Answer answer, string userId);
        Task<List<Answer>> GetAnswersForQuestionAsync(int questionId);

        Task<List<Question>> GetQuestionsWithAnswerCount();
        Task<int> GetAnswerCountForQuestion(int questionId);
    }
}

