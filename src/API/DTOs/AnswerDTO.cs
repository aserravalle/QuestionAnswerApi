using API.Enums;

namespace API.DTOs
{
    public class AnswerDTO
	{
		public List<string>? SelectedOptions { get; set; }
		public int? RatingValue { get; set; }
	}
}
