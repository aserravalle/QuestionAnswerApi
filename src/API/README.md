

# Questionnaire Manager

## 1 Structure

We will need the following directories (ordered from user facing to data layer):
- Controllers: API endpoints for users to interact with when answering questions
- Services: Logic that acts as an intermediary between the controller and data layers
- Models: Will act as an intermediary between the DTO and the logic in the app layer
- Mapping: From Models to DTOs
- DTOs: To represent our data features as classes
- Repositories: In lieu of a DB, we can use these classes to store Qs and As

And mirror this for unit tests

## 2 Models

We require models for the Question and Answer objects which we will manipulate in this application.

We can use a strategy pattern to define the common and unique elements of each of the models 
Then create common functionality that is executed in a unique way for each type of question and answer.

All questions have the following in common:
- ID
- Title (e.g. "How sustainable do you rate yourself?")
- Question Type (FiveStarRating, MultiSelect, SingleSelect)

They differ only in their answer:
- Min and Max values between 1-10, defaulting to 1-5 (FiveStarRating)
- A list of options (MultiSelect, SingleSelect)
- An index (SingleSelect) or list of indexes (MultiSelect) for the chosen answer

## 3 Data layer

To simplify the data layer, we will use a Data Transfer Object, Mappings, and a Repository

We use the DTO pattern so that we can easily translate the models to a table. THey intentionally do not contain logic, only data.
We use the repository pattern to separate data retrieval and data storage logic making the code more maintainable
We use the singleton pattern to ensure there can only be one repository

## 4 Services 
The Services class allows us to define the main logic which the controller will use. 

It uses a factory pattern to generate the correct type of question based on the user information
We use the Adapter pattern to map DTOs to their models in the factory
Then executes the strategies implemented in the Model classes

## 5 Controller
Users will interact with the controller class.
User instructions are fed to a specific endpoint which is read by the controller and then passed to the appropriate server
The Controller accepts the question ID in the endpoint so that the request is always attached to some QuestionId

## 6 Integration Testing
Run the application and open Postman. Prepare to run the following API requests:

1. Get
GET https://localhost:62662/api/question 
RESULT "Hello, World!"

2. Create Question
PUT https://localhost:62662/api/question/123e4567-e89b-12d3-a456-426614174000 
BODY 
{
    "Title":"This is a 5 star rating question",
    "Type":0,
    "Options":null,
    "MinValue":1,
    "MaxValue":5
}

3. GetQuestionById
GET https://localhost:62662/api/question/123e4567-e89b-12d3-a456-426614174000
RESULT
{
    "type": 0,
    "minValue": 1,
    "maxValue": 5,
    "id": "123e4567-e89b-12d3-a456-426614174000",
    "title": "This is a 5 star rating question"
}

4. AnswerQuestion
POST https://localhost:62662/api/question/123e4567-e89b-12d3-a456-426614174000
BODY
{
    "RatingValue":3
}

5. GetAllAnswersToQuestion
GET https://localhost:62662/api/question/123e4567-e89b-12d3-a456-426614174000/answers
RESULT
[
    {
        "question": {
            "id": "123e4567-e89b-12d3-a456-426614174000",
            "title": "This is a 5 star rating question",
            "type": 0
        },
        "answerId": "73aa23da-7d44-4539-b473-e9ea1cbe0ea3",
        "questionId": "123e4567-e89b-12d3-a456-426614174000",
        "type": 0,
        "value": 3,
        "selectedOptions": null,
        "ratingValue": 3
    }
]