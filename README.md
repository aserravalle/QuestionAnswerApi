# Technical Challenge
 
## Objective
 
Design and implement a system to manage questionnaires and collect responses.
 
## Task Overview
 
You, as part of the development team, have been tasked with creating an application capable of facilitating the creation of questionnaires and the collection of responses.
 
### Question Types
 
The application must support three types of questions, with functionalities for both creation and answering:
 
- **5-Star Rating Questions**: 
  - **Creation**:
    - Title to be displayed.
    - Answer value range, customizable within 1 to 10. Defaults to 1-5 if not specified.
  - **Answering**:
    - Submit only the selected value.
 
- **Multi-Select Question**: Allows selection of multiple answers.
  - **Creation**:
    - Title to be displayed.
    - Multiple answer options (at least 2).
  - **Answering**:
    - Submit one or more selected values.
 
- **Single-Select Question**: Similar to the multi-select, but limited to a single choice.
  - **Creation**:
    - Title to be displayed.
    - Multiple answer options.
  - **Answering**:
    - Submit only the selected value.
 
## API Specification
 
- **PUT** `/question/{id:uuid}`: Create a new question with the details as specified above.
- **POST** `/question/{id:uuid}`: Answer a question given its ID.
 
Both endpoints should support a flexible request schema suitable for the requirements of each question type.
 
## Project Structure
 
Your solution should utilize `C#` with `.NET 8.0`. The provided project template includes:
 
- A `Makefile` for automating CI/CD processes.
- A .NET 8.0 solution (`Questions.sln`), which should include all projects.
- A template WebAPI project (`src/API`) with a Dockerfile to aid in building and packaging.
- `.dockerignore` and `.gitignore` files to optimize storage and build processes.
 
You are encouraged to modify these components to develop an optimal solution.
 
## Tooling
 
Since we expect you to offer the more suitable and complete solution to the challenge, no additional tooling has been provided. Add as much tooling as you want/consider based on the requirements listed below. Key considerations include:
 
- Use of Docker for system building.
- Embracing automation for efficiency.
 
## Requirements
 
- **Performance**: The system should meet basic throughput and response time needs, though specific metrics are not defined. Aim for efficiency and reliability.
- **Collaboration**: Design the system to support multiple contributors, emphasizing testability and extensibility.
- **Documentation**: Clearly document your design decisions and operation methodologies. This documentation is crucial for us to understand your approach to system design and daily workflow.
