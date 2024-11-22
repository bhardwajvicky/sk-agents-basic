using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.Chat;
using Microsoft.SemanticKernel.ChatCompletion;


#pragma warning disable SKEXP0001 // Using directive is not required for the current code file.
#pragma warning disable SKEXP0110 // Using directive is not required for the current code file.

namespace AgentDemo
{
    internal class Program
    {
        private const string FleetManagerName = "FleetManager";
        private const string FleetManagerInstructions =
            """
            You are a fleet manager responsible for expanding your company's vehicle fleet.
            The goal is to write and refine a detailed request for proposal (RFP) to send to vehicle suppliers.
            Make sure your RFP is clear, comprehensive, and specifies all necessary requirements.
            Use the following structure:
            - Project Overview: Brief description of the fleet expansion
            - Vehicle Specifications: Detailed requirements for each type of vehicle
            - Quantity: Number of units required for each vehicle type
            - Budget Constraints: Financial limitations and expectations
            - Delivery Timeline: Expected delivery dates
            Only provide one RFP per response.
            Stay focused on the specifics of the proposal.
            Avoid unnecessary information.
            Consider suggestions when refining your RFP.
            Implement only one suggested improvement at a time.
            """;

        private const string VehicleSupplierName = "VehicleSupplier";
        private const string VehicleSupplierInstructions =
            """
            You are a vehicle supplier specializing in commercial fleet sales.
            The goal is to evaluate the fleet manager's RFP and determine if you can meet the requirements.
            If the RFP is acceptable, respond with 'We can fulfill your requirements as specified.'
            If not, provide constructive feedback on how to improve the RFP without giving specific examples.
            Use the following guidelines when evaluating:
            - Are the vehicle specifications clear and detailed?
            - Is the quantity feasible within the proposed timeline?
            - Does the budget align with market prices?
            - Are there any logistical challenges or ambiguities?
            Only suggest one area of improvement at a time.
            """;

        private const string SoftwareDeveloperName = "SoftwareDeveloper";
        private const string SoftwareDeveloperInstructions =
             """
            You are a seasoned software developer eager to share your expertise at prominent tech conferences.
            Your goal is to draft and iteratively refine a comprehensive proposal for an upcoming international technology conference.
            Ensure your proposal is detailed, engaging, and adheres strictly to the conference's submission guidelines.
            Use the following structure:
            - **Title**: A compelling title that encapsulates your talk's essence.
            - **Abstract**: A succinct yet thorough summary of your talk (aim for 200-250 words).
            - **Outline**: A detailed breakdown of the key points and sections you will cover.
            - **Key Takeaways**: Specific insights or skills the audience will gain.
            - **Target Audience**: Define who will benefit most from your talk (e.g., beginners, intermediates, experts).
            - **Prerequisites**: Any prior knowledge or tools the audience should have.
            - **Engagement Strategies**: How you plan to interact with the audience (e.g., live demos, Q&A, interactive polls).
            Only provide one proposal per response.
            Focus on incorporating feedback meticulously.
            Avoid unnecessary chit-chat.
            Implement only one suggested improvement at a time.
            Ensure that the proposal fully complies with all conference guidelines.
            Do not proceed to the next step until the current feedback has been addressed.
            """;


        private const string ConferenceOrganizerName = "ConferenceOrganizer";
        private const string ConferenceOrganizerInstructions =
            """
            You are a member of the organizing committee for a prestigious international technology conference, responsible for evaluating session proposals.
            Your goal is to thoroughly assess each submission and provide detailed, constructive feedback to refine it until it meets all the criteria.
            Do not approve the proposal until it fully satisfies all requirements.
            Use the following guidelines when evaluating a proposal:
            - **Relevance**: Does the topic align with the conference themes and the interests of the anticipated audience?
            - **Clarity**: Is the proposal clearly written, well-structured, and free of ambiguities?
            - **Depth**: Does the content provide sufficient depth and insight appropriate for the target audience level?
            - **Engagement**: Are there clear strategies outlined to actively engage the audience?
            - **Originality**: Does the topic offer a unique perspective or cover emerging trends in technology?
            - **Compliance**: Does the proposal meet all submission guidelines (e.g., word count, formatting, required sections)?
            Provide specific, actionable feedback on one aspect at a time.
            Encourage the developer to refine their proposal based on your suggestions.
            Do not approve the proposal until it meets all the above criteria thoroughly.
            When the proposal meets all criteria, respond with: 'Your proposal has been accepted for the conference.'
            """;


        private const string ReviewerName = "ArtDirector";
            private const string ReviewerInstructions =
                """
                You are an art director who has opinions about copywriting born of a love for David Ogilvy.
                The goal is to determine if the given copy is acceptable to print.
                If so, state that it is approved.
                If not, provide insight on how to refine suggested copy without example.
                """;

        private const string CopyWriterName = "CopyWriter";
        private const string CopyWriterInstructions =
            """
            You are a copywriter with ten years of experience and are known for brevity and a dry humor.
            The goal is to refine and decide on the single best copy as an expert in the field.
            Only provide a single proposal per response.
            You're laser focused on the goal at hand.
            Don't waste time with chit chat.
            Consider suggestions when refining an idea.
            """;

        static async Task Main(string[] args)
        {

            ChatCompletionAgent agentFleetManager = new()
            {
                Instructions = FleetManagerInstructions,
                Name = FleetManagerName,
                Kernel = CreateKernelWithChatCompletion(),
            };

            ChatCompletionAgent agentVehicleSupplier = new()
            {
                Instructions = VehicleSupplierInstructions,
                Name = VehicleSupplierName,
                Kernel = CreateKernelWithChatCompletion(),
            };


            ChatCompletionAgent agentSoftwareDeveloper =
                new()
                {
                    Instructions = SoftwareDeveloperInstructions,
                    Name = SoftwareDeveloperName,
                    Kernel = CreateKernelWithChatCompletion(),
                };

            ChatCompletionAgent agentConferenceOrganizer = new()
            {
                Instructions = ConferenceOrganizerInstructions,
                Name = ConferenceOrganizerName,
                Kernel = CreateKernelWithChatCompletion(),
            };

                    ChatCompletionAgent agentReviewer =
            new()
            {
                Instructions = ReviewerInstructions,
                Name = ReviewerName,
                Kernel = CreateKernelWithChatCompletion(),
            };

        ChatCompletionAgent agentWriter =
            new()
            {
                Instructions = CopyWriterInstructions,
                Name = CopyWriterName,
                Kernel = CreateKernelWithChatCompletion(),
            };



            // Flag to switch between the two scenarios
            // 0: Software Developer and Conference Organizer
            // 1: Reviewer and Copy Writer
            // 2: Fleet Manager and Vehicle Supplier
            int i =0;
            AgentGroupChat chat;
            ChatMessageContent input = new ChatMessageContent();
            switch (i)
            {
                case 0:
                    chat =
                        new(agentSoftwareDeveloper, agentConferenceOrganizer)
                        {
                            ExecutionSettings =
                            new()
                            {
                                TerminationStrategy =
                                new LooksGoodTerminationStrategy()
                                {
                                    Agents = [agentConferenceOrganizer],
                                    MaximumIterations = 10,
                                }
                            }
                        };
                    input = new(AuthorRole.User, "Begin drafting a comprehensive proposal for a talk titled 'Developing AI Agents with Semantic Kernel' for an upcoming international technology conference.");
                    chat.AddChatMessage(input);
                        break;
                case 2:
                    chat = new(agentFleetManager, agentVehicleSupplier)
                    {
                        ExecutionSettings =
                        new()
                        {
                            TerminationStrategy =
                            new CanFulfillTerminationStrategy()
                            {
                                Agents = [agentVehicleSupplier],
                                MaximumIterations = 10,
                            }
                        }
                    };
                    input = new(AuthorRole.User, "Prepare an RFP for acquiring 50 electric delivery vans.");
                    chat.AddChatMessage(input);
                    break;
                case 1:
                default:
                    chat = new(agentReviewer, agentWriter)
                    {
                        ExecutionSettings =
                        new()
                        {
                            TerminationStrategy =
                            new ApprovalTerminationStrategy()
                            {
                                Agents = [agentReviewer],
                                MaximumIterations = 10,
                            }
                        }
                    };
                    input = new(AuthorRole.User, "concept: maps made out of egg cartons.");
                    chat.AddChatMessage(input); 
                    break;

            }
        

            Console.WriteLine($"###{AuthorRole.User}:");
            Console.WriteLine($"'{input}'###");

            await foreach (var content in chat.InvokeAsync())
            {
                Console.WriteLine($"\n\n\n### {content.AuthorName.ToUpper() ?? "*"}:");
                Console.WriteLine($"'{content.Content}'");
                 
            }
        }

        protected static Kernel CreateKernelWithChatCompletion()
        {
            var config = new ConfigurationBuilder()
                       .AddJsonFile("appsettings.json")
                       .Build();

            var model = config["OPEN_AI_MODEL"];
            var key = config["OPEN_AI_KEY"];
            var orgId = config["OPEN_AI_ORG_ID"];

            var builder = Kernel.CreateBuilder();
            builder.AddOpenAIChatCompletion(model, key, orgId);
            return builder.Build();
        }
    }

    class ApprovalTerminationStrategy : TerminationStrategy
    {
        protected override Task<bool> ShouldAgentTerminateAsync(Agent agent, IReadOnlyList<ChatMessageContent> history, CancellationToken cancellationToken)
            => Task.FromResult(history[history.Count - 1].Content?.Contains("approve", StringComparison.OrdinalIgnoreCase) ?? false);
    }
    class LooksGoodTerminationStrategy: TerminationStrategy
    {
    protected override Task<bool> ShouldAgentTerminateAsync(Agent agent, IReadOnlyList<ChatMessageContent> history, CancellationToken cancellationToken)
            => Task.FromResult(history[history.Count - 1].Content?.Contains("Your proposal has been accepted for the conference.", StringComparison.OrdinalIgnoreCase) ?? false);
    }

    class CanFulfillTerminationStrategy : TerminationStrategy
    {
        protected override Task<bool> ShouldAgentTerminateAsync(Agent agent, IReadOnlyList<ChatMessageContent> history, CancellationToken cancellationToken)
            => Task.FromResult(history[history.Count - 1].Content?.Contains("We can fulfill your requirements as specified.", StringComparison.OrdinalIgnoreCase) ?? false);
    }
}

#pragma warning restore SKEXP0001 // Using directive is not required for the current code file.
#pragma warning restore SKEXP0110 // Using directive is not required for the current code file.