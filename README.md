# Architecture
![azure_architecture(1)](https://github.com/user-attachments/assets/7d934948-2a5e-4f23-8644-1a01bc8bb25f)

# Description
Small Chat/Messaging Application using [Azure Dev Frontend](https://github.com/Daniel-Pfeffer/azuredev-chat-frontend) as its frontend and [Azure Dev Backend](https://github.com/Daniel-Pfeffer/azuredev-chat-backend) as its backend.
Completly hosted on Azure.

### Used Azure Resources
* SignalR: to communicate to clients when a new message is available for a certain chat
* Azure Database for Postgresql: to store messages and chats
* Static Web App: to host angular frontend
* App Service: to host the .NET backend
* Virtual Network: to isolate database from any other networks
* Key Vault: to store necessary secrets (SignalR + DB connection strings)

### Scrapped Ideas
* Authorization/Authentication - Scrapped due to time limit and complexity

### Learnings
* Best practices require way more resources and time commitment for seemingly no benefit
  * Obvs. security is a major benefit
* Log streams where incredibly unreliable
* Limitations of the student subscription limited useability and development experience quite drastically
* Some wrong clicks can be **extremly** expensive
* Personally I like Spring ecosystem more than .NET, but .NET is way easier to get a functional web api without any knowledge
  * Probably due to the incredible [documentation](learn.microsoft.com)
