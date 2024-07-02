using System.Text;
using API.DTOs;
using API.Enums;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace API.Tests.Controllers
{
	[TestFixture]
	public class QuestionControllerTests
	{
		private WebApplicationFactory<Startup> _factory;
		private HttpClient _client;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_factory = new WebApplicationFactory<Startup>();
		}

		[SetUp]
		public void SetUp()
		{
			_client = _factory.CreateClient();
		}

		[Test]
		public async Task TestApiExists()
		{
			var response = await _client.GetAsync("/api/question/");
			Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
			Assert.That(response.Content.ReadAsStringAsync().Result, Is.EqualTo("Hello, World!"));
		}

		[Test]
		public async Task TestController()
		{
			var questionId = Guid.NewGuid();
			var questionDto = new QuestionDTO
			{
				Title = "This is a 5 star rating question",
				Type = QuestionType.FiveStarRating,
				MinValue = 1,
				MaxValue = 5
			};
			var json = JsonConvert.SerializeObject(questionDto);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PutAsync($"/api/question/{questionId}", content);
			Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));

			response = await _client.GetAsync($"/api/question/{questionId}");
			response.EnsureSuccessStatusCode();
			Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

			var actual = response.Content.ReadAsStringAsync().Result;
			var expected = "{\"type\":0,\"minValue\":1,\"maxValue\":5,\"id\":\"" + questionId + "\",\"title\":\"This is a 5 star rating question\"}";
			Assert.That(actual, Is.EqualTo(expected));

			var answerDto = new AnswerDTO
			{
				RatingValue = 3
			};
			json = JsonConvert.SerializeObject(answerDto);
			content = new StringContent(json, Encoding.UTF8, "application/json");
			response = await _client.PostAsync($"/api/question/{questionId}", content);
			Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NoContent));

			response = await _client.GetAsync($"/api/question/{questionId}/answers");
			response.EnsureSuccessStatusCode();
			Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
			
			actual = response.Content.ReadAsStringAsync().Result;
			expected = "\"questionId\":\"" + questionId + "\",\"type\":0,\"value\":3,\"selectedOptions\":null,\"ratingValue\":3}]";
			Assert.That(actual.Contains(expected));
		}

		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			// Dispose resources if needed
			_client.Dispose();
			_factory.Dispose();
		}
	}
}
