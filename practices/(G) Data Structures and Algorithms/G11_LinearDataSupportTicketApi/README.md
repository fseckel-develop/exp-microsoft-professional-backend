# (G11) LinearDataSupportTicketApi

An ASP.NET Core Web API demonstrating **linear data structures** through a support desk workflow. The project uses arrays, queues, stacks, and linked lists to model ticket lanes, pending work, undo operations, and event timelines.

---
## Course Context

**Course**: Data Structures and Algorithms  
**Section**: Linear Data Structures

This project demonstrates how core linear data structures can be applied in a small but practical API. Instead of showing isolated examples, it maps each structure to a support ticket workflow so their behaviour and trade-offs are easier to observe.

---
## Concepts Demonstrated

- Arrays for fixed collections
- Queues for FIFO ticket processing
- Stacks for undo behaviour
- Linked lists for ordered event timelines
- Sequential data structure trade-offs
- API-based demonstration of data structure behaviour
- Service-based workflow orchestration

---
## Project Structure

- **Controllers** – API endpoints for support lanes, tickets, and system health  
- **Contracts** – Request DTOs for ticket creation and status updates  
- **Models** – Ticket, lane, event, action, and status models  
- **Data** – In-memory ticket repository  
- **Services** – Workflow logic and data structure-specific operations  
- **Program.cs** – Application startup and dependency registration  
- **Requests.http** - HTTP requests to provided endpoints for testing

---
## How the Application Works

The API maps common support-desk behaviours to specific linear data structures:

- **Queue** – Incoming support tickets are processed FIFO.
- **Stack** – Ticket actions support undo operations.
- **Linked List** – Ticket events are stored in ordered timelines.
- **Array** – Fixed collections model ticket lanes.

API endpoints trigger these behaviours so their operational characteristics can be observed through a realistic workflow.