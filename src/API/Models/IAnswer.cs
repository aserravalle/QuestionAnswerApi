using API.Enums;

namespace API.Models
{
    public interface IAnswer
	{
		public Guid AnswerId { get; }
		public Guid QuestionId { get; }
		QuestionType Type { get; }
		public object Value { get; set; }
	}
}
