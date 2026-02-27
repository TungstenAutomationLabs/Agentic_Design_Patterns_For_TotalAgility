# Agentic_Design_Patterns_For_TotalAgility
This repo contains sample packages for use with Tungsten TotalAgility 2026.1+. The packages illustrate how to implement different agentic design patterns using TotalAgility's low code process orchestration.

These packages illustrate some of the concepts described in this blog: [How to Build Enterprise Grade AI Agents with Agentic Design Patterns](https://www.tungstenautomation.com/learn/blog/build-enterprise-grade-ai-agents-agentic-design-patterns)

The package contains 2 examples:
 - [The Agentic AI Reflection Pattern](https://www.tungstenautomation.com/learn/blog/the-agentic-ai-reflection-pattern)
 - [The Agentic AI Tool-Use Pattern](https://www.tungstenautomation.com/learn/blog/the-agentic-ai-tool-use-pattern)

Packages for additional patterns are available by request:
 - [The Agentic AI Multi-Agent Pattern](https://www.tungstenautomation.com/learn/blog/the-agentic-ai-multi-agent-pattern)
 - [The AI Planning Pattern](https://www.tungstenautomation.com/learn/blog/the-agentic-ai-planning-pattern)

Also see: 
 - [TotalAgility Example Agent using an Agentic Loop](https://github.com/TungstenAutomationLabs/TotalAgilityExampleAgent ) 

## Approach:
Key to the Tool Use pattern is agents, workflows or cases that are called by the agent have the same "agent" signature. This makes them universally pluggable, so the agent can invoke any worker agent or tool without needing a specific step. If structured data (e.g. json) is needed, this can be passed in using the "INPUT_PROMPT", simply describe the expected input format in the registry and the agent will do the rest.
![Agent IO Signature](./images/Agent_IO_Signature.png)

## What's in the Package:
The TotalAgility package contains:
 - Category: Agentic Design Pattern Examples - contains the agent examples		
    - Example - Async Agent	- template for calling an async workflow (note: this workflow should only use automatic steps, for "Human in the Loop" processes use a "Case")	
    - Example - Sync Agent - template for calling a sychronous agent		
    - Example - Case Agent - template for launching a long running case
    - Reflection - Report Formatting Agent - illustrates the "Reflection" pattern	
    - Tool Use - Chat Agent - illustrates the "Tool Use" pattern
    - Global Variable: Agent Tool Registry - holds the registry of tools or worker agents available to a Tool Use agent
 - Category: Agent Services - contains supporting services for the agents
    - Service - Analyse Document - sync	- used to provide meta data about a file to the calling agent
    - Service - Base64 to TA Document - used to convert Base64 into a TotalAgility Document (avoids needing to pass large blocks of Base64 data as variables in the agent process)		
 - Custom service group: Agent Services	
    - Custom service: Get Session - utility service to grab the user's session id for use when invoking tools (ensures tools are invoked with the same permissions and identity as the calling user)
 - TotalAgility Agent Documents	Extraction group - documents processed by agents are classified using this extraction group. Alls retention and knowledge base settings to be independently configured for agent documetns.

**Note:** the examples only illustrate the patterns, and are not fully functioning agents. To give the agents power, create tools for them to use and include them in the registry variable (alternatives to using a static variable include a proxy service to provide tools, semantic intent routing using the knowledge base or using a Model Context Protocol server).

## What's in the postman collection:
Import this file into [Postman](https://www.postman.com/) to see examples REST calls to invoke the agents, with or without documents attached. 

Create an "environment" for this file to provide:
 - *host*: the host name (DNS) of your TotalAgility enviroment 
 - *SPP_SYSTEM_SESSION_ID*: the session ID of your TotalAgility enviroment 
 - *user_name*: the user name of the account that will invoke the services

The web services can be used to call agents on TotalAgility, for example from [Microsoft Teams](https://github.com/TungstenAutomationLabs/TotalAgility-MSTeams-Agent)
	
