using API.Models;

namespace API.Repositories
{
    public interface IQuestionRepository
	{
		Task<Question> GetQuestionByIdAsync(Guid id);
		Task CreateQuestionAsync(Question question);
		Task AnswerQuestionAsync(Answer answer);
		Task<List<Answer>> GetAllAnswersToQuestion(Guid questionId);
		Task<List<Question>> GetAllQuestions();
	}
}
