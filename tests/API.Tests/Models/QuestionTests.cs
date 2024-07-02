using NUnit.Framework;
using API.Models;
using API.Enums;

namespace API.Tests.Models
{
	internal class QuestionTests
	{
		[Test]
		public void TestGenerateFiveStarRatingQuestion()
		{
			var question = new FiveStarRatingQuestion(
				Guid.NewGuid(),
				"This is a 5 star rating question"
			);

			Assert.That(question.Title, Is.EqualTo("This is a 5 star rating question"));
			Assert.That(question.Type, Is.EqualTo(QuestionType.FiveStarRating));
			Assert.That(question.MinValue, Is.EqualTo(1));
			Assert.That(question.MaxValue, Is.EqualTo(5));
		}

		[Test]
		public void TestFiveStarRatingQuestion_SetMinMaxValues()
		{
			var question = new FiveStarRatingQuestion(
				Guid.NewGuid(),
				"This is a 5 star rating question",
				2,
				10
			);

			Assert.That(question.MinValue, Is.EqualTo(2));
			Assert.That(question.MaxValue, Is.EqualTo(10));

			question = new FiveStarRatingQuestion(
				Guid.NewGuid(),
				"This is a 5 star rating question",
				10,
				2
			);

			Assert.That(question.MinValue, Is.EqualTo(2));
			Assert.That(question.MaxValue, Is.EqualTo(10));

			question = new FiveStarRatingQuestion(
				Guid.NewGuid(),
				"This is a 5 star rating question",
				-1,
				20
			);

			Assert.That(question.MinValue, Is.EqualTo(1));
			Assert.That(question.MaxValue, Is.EqualTo(10));
		}

		[Test]
		public void TestGenerateSingleSelectQuestion()
		{
			var question = new SingleSelectQuestion(
				Guid.NewGuid(),
				"This is a single select question",
				new List<string>() { "a", "b", "c" }
			);

			Assert.That(question.Title, Is.EqualTo("This is a single select question"));
			Assert.That(question.Type, Is.EqualTo(QuestionType.SingleSelect));
			Assert.That(question.Options, Is.EqualTo(new List<string>() { "a", "b", "c" }));
		}

		[Test]
		public void TestGenerateMultiSelectQuestion()
		{
			var question = new MultiSelectQuestion(
				Guid.NewGuid(),
				"This is a multi select question",
				new List<string>() { "a", "b", "c"}
			);

			Assert.That(question.Title, Is.EqualTo("This is a multi select question"));
			Assert.That(question.Type, Is.EqualTo(QuestionType.MultiSelect));
			Assert.That(question.Options, Is.EqualTo(new List<string>() { "a", "b", "c" }));
		}
	}
}
