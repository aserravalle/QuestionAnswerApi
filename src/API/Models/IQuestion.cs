using API.Enums;

namespace API.Models
{
    public interface IQuestion
	{
		Guid Id { get; set; }
		string Title { get; set; }
		QuestionType Type { get; set; }
	}
}