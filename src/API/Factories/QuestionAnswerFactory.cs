using API.DTOs;
using API.Enums;
using API.Models;


namespace API.Factories
{
	public class QuestionAnswerFactory : IQuestionAnswerFactory
	{
		public Question CreateQuestion(Guid questionId, QuestionDTO questionDTO)
		{
			Question question = questionDTO.Type switch
			{
				QuestionType.FiveStarRating => new FiveStarRatingQuestion(questionId, questionDTO.Title, (int)questionDTO.MinValue, (int)questionDTO.MaxValue),
				QuestionType.MultiSelect => new MultiSelectQuestion(questionId, questionDTO.Title, questionDTO.Options),
				QuestionType.SingleSelect => new SingleSelectQuestion(questionId, questionDTO.Title, questionDTO.Options),
				_ => throw new ArgumentException("Invalid question type")
			};
			return question;
		}

		public Answer CreateAnswer(Question question, AnswerDTO answerDTO)
		{
			Answer answer = question.Type switch
			{
				QuestionType.FiveStarRating => new FiveStarRatingAnswer((FiveStarRatingQuestion)question, (int)answerDTO.RatingValue),
				QuestionType.MultiSelect => new MultiSelectAnswer((MultiSelectQuestion)question, answerDTO.SelectedOptions),
				QuestionType.SingleSelect => new SingleSelectAnswer((SingleSelectQuestion)question, answerDTO.SelectedOptions.First()),
				_ => throw new ArgumentException("Invalid answer type")	
			};
			return answer;
		}
	}
}