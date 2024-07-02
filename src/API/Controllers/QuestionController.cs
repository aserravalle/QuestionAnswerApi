using API.DTOs;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class QuestionController : ControllerBase
	{
		private readonly IQuestionService _questionService;

		public QuestionController(IQuestionService questionService)
		{
			_questionService = questionService;
		}

		[HttpGet]
		public IActionResult Get()
		{
			return Ok("Hello, World!");
		}

		[HttpPut("{questionId:guid}")]
		public async Task<IActionResult> CreateQuestion(Guid questionId, [FromBody] QuestionDTO questionDto)
		{
			if (questionDto == null)
			{
				return BadRequest("Question data is required.");
			}

			if (string.IsNullOrEmpty(questionDto.Title))
			{
				return BadRequest("Question needs a title.");
			}

			await _questionService.CreateQuestionAsync(questionId, questionDto);
			return CreatedAtAction(nameof(GetQuestionById), new { questionId }, questionDto);
		}

		[HttpPost("{questionId:guid}")]
		public async Task<IActionResult> AnswerQuestion(Guid questionId, [FromBody] AnswerDTO answerDTO)
		{
			await _questionService.AnswerQuestionAsync(questionId, answerDTO);
			return NoContent();
		}

		[HttpGet("{questionId:guid}")]
		public async Task<IActionResult> GetQuestionById(Guid questionId)
		{
			var question = await _questionService.GetQuestionByIdAsync(questionId);
			if (question == null)
			{
				return NotFound();
			}
			return Ok(question);
		}

		[HttpGet("{questionId:guid}/answers")]
		public async Task<IActionResult> GetAllAnswersToQuestion(Guid questionId)
		{
			var answers = await _questionService.GetAllAnswersToQuestion(questionId);
			if (answers == null)
			{
				return NotFound();
			}
			return Ok(answers);
		}
	}

}
