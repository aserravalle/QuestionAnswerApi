using API.Enums;

namespace API.DTOs
{
	public class QuestionDTO
	{
		public string Title { get; set; }
		public QuestionType Type { get; set; }
		public List<string>? Options { get; set; }
		public int? MinValue { get; set; }
		public int? MaxValue { get; set; }
	}
}
