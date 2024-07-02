using API.Models;
using System.Collections.Concurrent;

namespace API.Repositories
{
    public class QuestionRepository : IQuestionRepository
	{
		protected readonly ConcurrentDictionary<Guid, Question> _questions = new();
		protected readonly ConcurrentDictionary<Guid, List<Answer>> _answers = new();

		public Task<Question> GetQuestionByIdAsync(Guid id)
		{
			_questions.TryGetValue(id, out var question);
			if (question == null)
			{
				throw new Exception($"Question not found with GUID '{id}'");
			}
			return Task.FromResult(question);
		}

		public Task<List<Question>> GetAllQuestions()
		{
			var allQuestions = _questions.Values.ToList();
			return Task.FromResult(allQuestions);
		}

		public Task CreateQuestionAsync(Question question)
		{
			_questions[question.Id] = question;
			return Task.CompletedTask;
		}

		public Task AnswerQuestionAsync(Answer answer)
		{
			if (!_answers.ContainsKey(answer.QuestionId))
			{
				_answers[answer.QuestionId] = new List<Answer>();
			}
			_answers[answer.QuestionId].Add(answer);
			return Task.CompletedTask;
		}

		public Task<List<Answer>> GetAllAnswersToQuestion(Guid questionId)
		{
			_answers.TryGetValue(questionId, out var answers);
			return Task.FromResult(answers);
		}
	}
}
