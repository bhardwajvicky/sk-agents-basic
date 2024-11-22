# AgentDemo

AgentDemo is a C# application that demonstrates the use of Microsoft Semantic Kernel to create and manage chat agents with specific instructions and termination strategies.

## Prerequisites

- Visual Studio 2022
- .NET 8 SDK

## Setup

1. Clone the repository.
2. Open the solution in Visual Studio 2022.
3. Rename `appsettings.json.template` to `appsettings.json`.
4. Update the `appsettings.json` file with your OpenAI credentials.

## Running the Application

1. Build the solution.
2. Run the application.

The application will create chat agents with specific instructions and execute them based on the provided scenarios.

## Agents

- **FleetManager**: Writes and refines a detailed request for proposal (RFP) for vehicle suppliers.
- **VehicleSupplier**: Evaluates the fleet manager's RFP and provides feedback.
- **SoftwareDeveloper**: Writes and refines proposals for developer conferences.
- **ConferenceOrganizer**: Evaluates proposals for developer conferences.
- **ArtDirector**: Reviews copy for print.
- **CopyWriter**: Refines and decides on the best copy.

## Termination Strategies

- **ApprovalTerminationStrategy**: Terminates when the content contains "approve".
- **LooksGoodTerminationStrategy**: Terminates when the content contains "Your proposal has been accepted for the conference.".
- **CanFulfillTerminationStrategy**: Terminates when the content contains "We can fulfill your requirements as specified.".

## Use Cases

### Use Case 1: Proposal Writing and Evaluation

In this scenario, the `SoftwareDeveloper` and `ConferenceOrganizer` agents are used. The `SoftwareDeveloper` agent writes and refines a proposal for an upcoming international technology conference, while the `ConferenceOrganizer` agent evaluates the proposal based on specific guidelines.

#### Instructions for SoftwareDeveloper

- Write and refine a proposal for an upcoming international technology conference.
- Use the following structure:
  - Title: A compelling title that encapsulates your talk's essence.
  - Abstract: A succinct yet thorough summary of your talk (aim for 200-250 words).
  - Outline: A detailed breakdown of the key points and sections you will cover.
  - Key Takeaways: Specific insights or skills the audience will gain.
  - Target Audience: Define who will benefit most from your talk (e.g., beginners, intermediates, experts).
  - Prerequisites: Any prior knowledge or tools the audience should have.
  - Engagement Strategies: How you plan to interact with the audience (e.g., live demos, Q&A, interactive polls).
- Only provide one proposal per response.
- Focus on incorporating feedback meticulously.
- Avoid unnecessary chit-chat.
- Implement only one suggested improvement at a time.
- Ensure that the proposal fully complies with all conference guidelines.

#### Instructions for ConferenceOrganizer

- Evaluate the proposal to determine if it qualifies for the conference.
- Use the following guidelines:
  - Relevance: Does the topic align with the conference themes and the interests of the anticipated audience?
  - Clarity: Is the proposal clearly written, well-structured, and free of ambiguities?
  - Depth: Does the content provide sufficient depth and insight appropriate for the target audience level?
  - Engagement: Are there clear strategies outlined to actively engage the audience?
  - Originality: Does the topic offer a unique perspective or cover emerging trends in technology?
  - Compliance: Does the proposal meet all submission guidelines (e.g., word count, formatting, required sections)?
- Provide specific, actionable feedback on one aspect at a time.
- Encourage the developer to refine their proposal based on your suggestions.
- Do not approve the proposal until it meets all the above criteria thoroughly.
- When the proposal meets all criteria, respond with: 'Your proposal has been accepted for the conference.'.

### Use Case 2: Copywriting and Review

In this scenario, the `ArtDirector` and `CopyWriter` agents are used. The `CopyWriter` agent refines and decides on the best copy, while the `ArtDirector` agent reviews the copy for print.

#### Instructions for ArtDirector

- Determine if the given copy is acceptable to print.
- If so, state that it is approved.
- If not, provide insight on how to refine the suggested copy without example.

#### Instructions for CopyWriter

- Refine and decide on the single best copy as an expert in the field.
- Only provide a single proposal per response.
- Focus on the goal at hand and avoid chit chat.
- Consider suggestions when refining an idea.

### Use Case 3: Fleet Management and Vehicle Supply

In this scenario, the `FleetManager` and `VehicleSupplier` agents are used. The `FleetManager` agent writes and refines a detailed request for proposal (RFP) for vehicle suppliers, while the `VehicleSupplier` agent evaluates the RFP and provides feedback.

#### Instructions for FleetManager

- Write and refine a detailed request for proposal (RFP) to send to vehicle suppliers.
- Use the following structure:
  - Project Overview: Brief description of the fleet expansion.
  - Vehicle Specifications: Detailed requirements for each type of vehicle.
  - Quantity: Number of units required for each vehicle type.
  - Budget Constraints: Financial limitations and expectations.
  - Delivery Timeline: Expected delivery dates.
- Only provide one RFP per response.
- Stay focused on the specifics of the proposal.
- Avoid unnecessary information.
- Consider suggestions when refining your RFP.
- Implement only one suggested improvement at a time.

#### Instructions for VehicleSupplier

- Evaluate the fleet manager's RFP and determine if you can meet the requirements.
- If the RFP is acceptable, respond with 'We can fulfill your requirements as specified.'.
- If not, provide constructive feedback on how to improve the RFP without giving specific examples.
- Use the following guidelines when evaluating:
  - Are the vehicle specifications clear and detailed?
  - Is the quantity feasible within the proposed timeline?
  - Does the budget align with market prices?
  - Are there any logistical challenges or ambiguities?
- Only suggest one area of improvement at a time.

## License
This project is licensed under the MIT License.