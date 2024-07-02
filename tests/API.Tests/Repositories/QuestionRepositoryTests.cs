using API.Models;
using NUnit.Framework;

namespace API.Tests.Repositories
{
	internal class QuestionRepositoryTests
	{
		[Test]
		public void TestQuestionRepository()
		{
			var repo = new QuestionRepositoryForTest();

			var question1 = new FiveStarRatingQuestion(Guid.NewGuid(), "This is a 5 star rating question");
			repo.CreateQuestionAsync(question1);
			var question2 = new SingleSelectQuestion(Guid.NewGuid(), "This is a single select question", new List<string>() { "a", "b", "c" });
			repo.CreateQuestionAsync(question2);
			var question3 = new MultiSelectQuestion(Guid.NewGuid(), "This is a multi select question", new List<string>() { "a", "b", "c" });
			repo.CreateQuestionAsync(question3);
			Assert.That(repo.QuestionsExposed.Count, Is.EqualTo(3), "There should be 3 questions added to the repository");

			var getQuestion = repo.GetQuestionByIdAsync(question1.Id).Result;
			Assert.That(getQuestion.Id, Is.EqualTo(question1.Id), "Should have gotten Q1 from the DB");

			var answer1 = new FiveStarRatingAnswer(question1, 3);
			repo.AnswerQuestionAsync(answer1);
			var answer2 = new FiveStarRatingAnswer(question1, 3);
			repo.AnswerQuestionAsync(answer2);
			Assert.That(repo.AnswersExposed.ContainsKey(question1.Id), "Q1 should be answered");
			Assert.That(repo.AnswersExposed.Count, Is.EqualTo(1), "Only Q1 has been answered");

			var answersFromRepo = repo.GetAllAnswersToQuestion(question1.Id).Result;
			Assert.That(answersFromRepo.Count, Is.EqualTo(2), "There should be 2 answers to Q1");
			Assert.That(answersFromRepo.First().AnswerId, Is.EqualTo(answer1.AnswerId));
		}
	}
}
