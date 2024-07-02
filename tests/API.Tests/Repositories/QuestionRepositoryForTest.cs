using API.Models;
using API.Repositories;
using System.Collections.Concurrent;

namespace API.Tests.Repositories
{
	public class QuestionRepositoryForTest : QuestionRepository
	{
		public QuestionRepositoryForTest()
		{
			this.QuestionsExposed = this._questions;
			this.AnswersExposed = this._answers;
		}

		public ConcurrentDictionary<Guid, Question> QuestionsExposed { get; }
		public ConcurrentDictionary<Guid, List<Answer>> AnswersExposed { get; }
	}
}
