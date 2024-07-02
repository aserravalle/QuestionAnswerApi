using API.DTOs;
using API.Models;
using API.Repositories;
using API.Factories;

namespace API.Services
{
    public class QuestionService : IQuestionService
	{
		private readonly IQuestionRepository _questionRepository;
		private readonly IQuestionAnswerFactory _factory;

		public QuestionService(IQuestionRepository questionRepository, IQuestionAnswerFactory factory)
		{
			_questionRepository = questionRepository;
			_factory = factory;
		}

		public async Task<Question> GetQuestionByIdAsync(Guid id)
		{
			return await _questionRepository.GetQuestionByIdAsync(id);
		}

		public async Task<List<Question>> GetAllQuestionsAsync()
		{
			return await _questionRepository.GetAllQuestions();
		}

		public async Task CreateQuestionAsync(Guid questionId, QuestionDTO questionDTO)
		{
			Question question = _factory.CreateQuestion(questionId, questionDTO);
			await _questionRepository.CreateQuestionAsync(question);
		}

		public async Task AnswerQuestionAsync(Guid questionId, AnswerDTO answerDTO)
		{
			var question = await _questionRepository.GetQuestionByIdAsync(questionId);
			var answer = _factory.CreateAnswer(question, answerDTO);
			await _questionRepository.AnswerQuestionAsync(answer);
		}

		public async Task<List<Answer>> GetAllAnswersToQuestion(Guid questionId)
		{
			return await _questionRepository.GetAllAnswersToQuestion(questionId);
		}
	}

}
