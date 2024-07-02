using API.Enums;

namespace API.Models
{
	public abstract class Question : IQuestion
	{
		public Question(Guid id, string title, QuestionType type)
		{
			Id = id;
			Title = title;
			Type = type;
		}

		public new Guid Id { get; set; }
		public new string Title { get; set; }
		public new virtual QuestionType Type { get; set; }
	}

	public class FiveStarRatingQuestion : Question
	{
		public FiveStarRatingQuestion(Guid id, string title, int minValue = 1, int maxValue = 5) 
			: base(id, title, QuestionType.FiveStarRating)
		{
			if (minValue > maxValue)
			{
				var newMinValue = maxValue;
				maxValue = minValue;
				minValue = newMinValue;
			}
			this.MinValue = Math.Max(MinMinValue, minValue);
			this.MaxValue = Math.Min(MaxMaxValue, maxValue);
		}

		public override QuestionType Type => QuestionType.FiveStarRating;
		public new int MinValue { get; set; }
		public new int MaxValue { get; set; }

		public const int MaxMaxValue = 10;
		public const int MinMinValue = 1;
	}

	public class MultiSelectQuestion : Question
	{
		public MultiSelectQuestion(Guid id, string title, List<string> options)
			: base(id, title, QuestionType.MultiSelect)
		{
			this.Options = options;
		}

		public override QuestionType Type => QuestionType.MultiSelect;
		public new List<string> Options { get; set; }
	}

	public class SingleSelectQuestion : Question
	{
		public SingleSelectQuestion(Guid id, string title, List<string> options)
			: base(id, title, QuestionType.SingleSelect)
		{
			this.Options = options;
		}

		public override QuestionType Type => QuestionType.SingleSelect;
		public new List<string> Options { get; set; }
	}
}
