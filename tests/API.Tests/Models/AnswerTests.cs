using NUnit.Framework;
using API.Models;
using API.Enums;

namespace API.Tests.Models
{
	internal class AnswerTests
	{
		[Test]
		public void TestGenerateFiveStarRatingAnswer()
		{
			var question = new FiveStarRatingQuestion(
				Guid.NewGuid(),
				"This is a 5 star rating question"
			);
			var answer = new FiveStarRatingAnswer(question, 3);

			Assert.That(answer.QuestionId, Is.EqualTo(question.Id));
			Assert.That(answer.Type, Is.EqualTo(QuestionType.FiveStarRating));
			Assert.That(answer.Value, Is.EqualTo(3));

			answer.Value = 2;
			Assert.That(answer.Value, Is.EqualTo(2));

			answer.Value = 10;
			Assert.That(answer.Value, Is.EqualTo(5), "should default to max if higher");

			answer.Value = -2;
			Assert.That(answer.Value, Is.EqualTo(1), "should default to min if lower");
		}

		[Test]
		public void TestGenerateSingleSelectAnswer()
		{
			var question = new SingleSelectQuestion(
				Guid.NewGuid(),
				"This is a single select question",
				new List<string>() { "a", "b", "c", "2" }
			);
			var answer = new SingleSelectAnswer(question, "a");

			Assert.That(answer.QuestionId, Is.EqualTo(question.Id));
			Assert.That(answer.Type, Is.EqualTo(QuestionType.SingleSelect));
			Assert.That(answer.Value, Is.EqualTo(new List<string>() { "a" }));

			answer.Value = 2;
			Assert.That(answer.Value, Is.EqualTo(new List<string>() { "2" }), "Can set value to an int");

			
			Assert.Throws<ArgumentException>(() => 
			{
				answer.Value = new List<string>() { "3", "2" }; 
			}, "Should not be able to select multiple options");


			Assert.Throws<ArgumentException>(() =>
			{
				answer.Value = new List<string>() { "a" };
			}, "Should not be able to select options not in the list");
		}

		[Test]
		public void TestGenerateMultiSelectAnswer()
		{
			var question = new MultiSelectQuestion(
				Guid.NewGuid(),
				"This is a multi select question",
				new List<string>() { "a", "b", "c" }
			);
			var answer = new MultiSelectAnswer(question, new List<string>() { "a", "b"});

			Assert.That(answer.QuestionId, Is.EqualTo(question.Id));
			Assert.That(answer.Type, Is.EqualTo(QuestionType.MultiSelect));
			Assert.That(answer.Value, Is.EqualTo(new List<string>() { "a", "b" }));

			answer.Value = "a";
			Assert.That(answer.Value, Is.EqualTo(new List<string>() { "a" }), "Can set value to a string");

			Assert.Throws<ArgumentException>(() =>
			{
				answer.Value = new List<string>() { "3", "2" };
			}, "Should not be able to select options not in the list");
		}
	}
}
