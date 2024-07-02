using API.DTOs;
using API.Enums;
using API.Models;
using NUnit.Framework;

namespace API.Tests.Services
{
	internal class QuestionServiceTests
	{
		[Test]
		public async Task TestQuestionService()
		{
			var service = QuestionServiceForTest.New();

			var question1Id = Guid.NewGuid();
			await service.CreateQuestionAsync(question1Id,
				new QuestionDTO()
				{
					Title = "This is a 5 star rating question",
					Type = QuestionType.FiveStarRating,
					MinValue = 1,
					MaxValue = 5
				});
			var question2Id = Guid.NewGuid();
			await service.CreateQuestionAsync(question2Id,
				new QuestionDTO()
				{
					Title = "This is a single select question",
					Type = QuestionType.SingleSelect,
					Options = new List<string>() { "a", "b", "c" }
				});
			var question3Id = Guid.NewGuid();
			await service.CreateQuestionAsync(question3Id,
				new QuestionDTO()
				{
					Title = "This is a multi select question",
					Type = QuestionType.MultiSelect,
					Options = new List<string>() { "a", "b", "c" }
				});
			Assert.That(service.QuestionRepository.QuestionsExposed.Count, Is.EqualTo(3), "There should be 3 questions added to the repository");

			var allQuestions = service.GetAllQuestionsAsync().Result;
			Assert.That(allQuestions.Count, Is.EqualTo(3), "should have added 3 questions");

			var getQuestion1 = service.GetQuestionByIdAsync(question1Id).Result;
			Assert.IsInstanceOf<FiveStarRatingQuestion>(getQuestion1);

			await service.AnswerQuestionAsync(question1Id, 
				new AnswerDTO()
				{
					RatingValue = 3
				});
			await service.AnswerQuestionAsync(question1Id, 
				new AnswerDTO()
				{
					RatingValue = 3
				});
			Assert.That(service.QuestionRepository.AnswersExposed.ContainsKey(question1Id), "Q1 should be answered");
			Assert.That(service.QuestionRepository.AnswersExposed.Count, Is.EqualTo(1), "Only Q1 has been answered");

			var allAnswers = service.GetAllAnswersToQuestion(question1Id).Result;
			Assert.That(allAnswers.Count, Is.EqualTo(2), "There should be 2 answers to Q1");
			Assert.IsInstanceOf<FiveStarRatingAnswer>(allAnswers.First());

			var answerExistsInRepo = service.QuestionRepository.AnswersExposed[question1Id].Exists(a => a.AnswerId == allAnswers.First().AnswerId);
			Assert.That(answerExistsInRepo, "The first answer ID returned from the service should exist in the repo if Q1 has been answered ");
		}
	}
}
