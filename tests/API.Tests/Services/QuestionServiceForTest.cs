using API.DTOs;
using API.Factories;
using API.Models;
using API.Services;
using API.Tests.Repositories;


namespace API.Tests.Services
{
    internal class QuestionServiceForTest : QuestionService
	{
		public readonly QuestionRepositoryForTest QuestionRepository;

		QuestionServiceForTest(QuestionRepositoryForTest questionRepository, IQuestionAnswerFactory factory) 
			: base(questionRepository, factory)
		{
			this.QuestionRepository = questionRepository;
		}

		public static QuestionServiceForTest New()
		{
			var questionRepository = new QuestionRepositoryForTest();
			var factory = new QuestionAnswerFactory();
			return new QuestionServiceForTest(questionRepository, factory);
		}
	}
}
