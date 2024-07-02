using API.DTOs;
using API.Models;

namespace API.Factories
{
	public interface IQuestionAnswerFactory
   {
      Question CreateQuestion(Guid questionId, QuestionDTO questionDTO);
      Answer CreateAnswer(Question question, AnswerDTO answerDTO);
   }
}