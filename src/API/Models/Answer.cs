using API.Enums;
using System.Collections;

namespace API.Models
{
	public abstract class Answer : IAnswer
	{
		public Answer(Question question)
		{
			this.AnswerId = Guid.NewGuid();
			this.Question = question;
		}

		public Question Question { get; set; }
		public new Guid AnswerId { get; set; }
		public new Guid QuestionId => Question.Id;
		public QuestionType Type => Question.Type;

		public object _value;
		public object Value
		{ 
			get
			{ 
				return this._value;
			}
			set
			{ 
				this._value = ApplyConstraintsTo(value);
			}
		}

		public abstract object ApplyConstraintsTo(object newValue);

		public new abstract List<string> SelectedOptions { get; }
		public new abstract int? RatingValue { get; }

	}

	public class FiveStarRatingAnswer : Answer
	{
		public FiveStarRatingAnswer(FiveStarRatingQuestion question, int ratingValue)
			: base(question)
		{
			this.MinValue = question.MinValue;
			this.MaxValue = question.MaxValue;
			this.Value = ratingValue;
		}

		public new FiveStarRatingQuestion Question { get; set; }
		public int MinValue { get; }
		public int MaxValue { get; }

		public override List<string> SelectedOptions { get => null; }

		public override object ApplyConstraintsTo(object newValue)
		{
			var intValue = (int)newValue;
			if (intValue < MinValue)
			{
				intValue = MinValue;
			}
			else if (intValue > MaxValue)
			{
				intValue = MaxValue;
			}

			return intValue;
		}
	
		public override int? RatingValue => (int)Value;
	}

	public class MultiSelectAnswer : Answer
	{
		public MultiSelectAnswer(MultiSelectQuestion question, List<string> selectedOptions)
			: base(question)
		{
			this.SelectableOptions = question.Options;
			this.Value = selectedOptions;
		}

		public new MultiSelectQuestion Question { get; set; }
		public List<string> SelectableOptions { get; }

		public override object ApplyConstraintsTo(object newValue)
		{
			if (newValue is string || newValue is int)
			{
				newValue = new List<string> { newValue.ToString() };
			}
			if (newValue is IEnumerable enumerable)
			{
				var result = new List<string>();
				foreach (var item in enumerable)
				{
					if (SelectableOptions.Contains(item))
					{
						result.Add(item.ToString());
					}
					else
					{
						throw new ArgumentException($"Contains invalid option {item}");
					}
				}
				return result;
			}
			else
			{
				throw new ArgumentException("The supplied value was invalid");
			}
		}

		public override List<string> SelectedOptions => (List<string>)this.Value;

		public override int? RatingValue => null;
	}

	public class SingleSelectAnswer : Answer
	{
		public SingleSelectAnswer(SingleSelectQuestion question, int selectedOption)
			: base(question)
		{
			this.SelectableOptions = question.Options;
			this.Value = selectedOption;
		}

		public SingleSelectAnswer(SingleSelectQuestion question, string selectedOption)
			: base(question)
		{
			this.SelectableOptions = question.Options;
			this.Value = selectedOption;
		}

		public new SingleSelectQuestion Question { get; set; }
		public List<string> SelectableOptions { get; }

		public override object ApplyConstraintsTo(object newValue)
		{
			if (newValue is string || newValue is int)
			{
				string strValue = newValue.ToString();
				if (SelectableOptions.Contains(strValue))
				{
					return new List<string> { strValue };
				}
				else
				{
					throw new ArgumentException($"Contains invalid option {strValue}");
				}
			}
			else if (newValue is IEnumerable)
			{
				throw new ArgumentException("The supplied value was enumerable but SingleSelectAnswer accepts only one answer");
			}
			else
			{
				throw new ArgumentException("The supplied value was invalid");
			}
		}

		public override List<string> SelectedOptions => (List<string>)this.Value;

		public override int? RatingValue => null;
	}
}
