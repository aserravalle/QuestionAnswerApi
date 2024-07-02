using API.DTOs;
using API.Models;

namespace API.Services
{
	public interface IQuestionService
	{
		Task<Question> GetQuestionByIdAsync(Guid id);
		Task CreateQuestionAsync(Guid questionId, QuestionDTO questionDTO);
		Task AnswerQuestionAsync(Guid questionId, AnswerDTO answerDTO);
		Task<List<Question>> GetAllQuestionsAsync();
		Task<List<Answer>> GetAllAnswersToQuestion(Guid questionId);
	}
}
