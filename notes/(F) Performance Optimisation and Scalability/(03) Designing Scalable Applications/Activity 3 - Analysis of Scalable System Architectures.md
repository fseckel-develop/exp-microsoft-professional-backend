## Example 1: Architecture for an E-Commerce Application

### Scenario

- .NET-based e-commerce platform with high peak-hour traffic
- System requirements:
	- Dynamic scaling for traffic surges
	- Database optimisation using caching
	- Asynchronous processing for background tasks (e.g. emails)

### Architecture Diagram Components

- Load Balancing & Stateless Web Servers
	- Prevents single-server overload
	- Implementation:
		- Azure Load Balancer distributing requests
		- Stateless .NET Web APIs

- Caching for Performance Optimisation
	- Reduces frequent database access
	- Implementation:
		- Redis in-memory cache for product and cart data
		- Distributed caching for multi-server consistency

- Asynchronous Background Processing
	- Avoids blocking user requests
	- Implementation:
		- Azure Queue Storage for background jobs
		- Worker services for order confirmations and payments

### Design Decisions

- Load Balancer
	- Distributes incoming traffic
	- Implementation:
		- Azure Load Balancer / Nginx

- Stateless Web API
	- Ensures horizontal scalability
	- Implementation:
		- .NET Web API with JWT authentication

- Caching Layer
	- Improves response times and reduces DB load
	- Implementation:
		- Redis cache

- Message Queue
	- Handles asynchronous tasks
	- Implementation:
		- Azure Queue Storage / RabbitMQ

- Database
	- Stores transactional data
	- Implementation:
		- SQL Server with indexing

### Bottlenecks & Solutions

- Slow database queries
	- Cause:
		- High read/write volume
	- Solution:
		- Introduce caching and proper indexing

- Overloaded web servers
	- Cause:
		- Sudden traffic spikes
	- Solution:
		- Auto-scaling and load balancing

- Delayed background processing
	- Cause:
		- Queue overload
	- Solution:
		- Dynamic scaling of worker nodes

---
## Example 2: Architecture for Learning Management System (LMS)

### Scenario

- .NET-based LMS serving thousands of students
- System requirements:
	- Efficient content delivery
	- Real-time notifications
	- Scalability during exams

### Architecture Diagram Components

- Load Balancing for Scalability
	- Distributes API traffic evenly
	- Implementation:
		- Azure Load Balancer with Kubernetes-hosted .NET APIs

- Caching & Content Delivery
	- Minimises database access
	- Implementation:
		- Redis for course data
		- Azure CDN for video delivery

- Asynchronous Notifications & Grading
	- Prevents UI blocking
	- Implementation:
		- Azure Service Bus for grading
		- SignalR for real-time notifications

### Design Decisions

- Load Balancer
	- Ensures high availability
	- Implementation:
		- Azure Load Balancer / Kubernetes

- Stateless Web API
	- Supports horizontal scaling
	- Implementation:
		- .NET Web API with JWT authentication

- Caching Layer
	- Improves content access speed
	- Implementation:
		- Redis & Azure CDN

- Message Queue
	- Processes async grading and notifications
	- Implementation:
		- Azure Service Bus

- Database
	- Stores student and course data
	- Implementation:
		- SQL Server with indexing

### Bottlenecks & Solutions

- Slow page loads
	- Cause:
		- Heavy database reads
	- Solution:
		- Redis caching

- Delayed notifications
	- Cause:
		- Synchronous messaging
	- Solution:
		- Asynchronous messaging with SignalR

- Overloaded grading system
	- Cause:
		- High submission volume
	- Solution:
		- Batch grading with worker nodes

---
## Example 3: Social Media Platform Architecture

### Scenario

- .NET-based social media platform
- Core requirements:
	- User-generated content
	- Real-time messaging and notifications
	- High availability for viral traffic

### Architecture & Design Decisions

- Load Balancer
	- Distributes user traffic
	- Implementation:
		- Azure Load Balancer / Kubernetes

- Caching Layer
	- Speeds up content retrieval
	- Implementation:
		- Redis for profiles and recent posts

- Async Processing
	- Handles notifications and messaging
	- Implementation:
		- Azure Service Bus & SignalR

- Database
	- Stores posts and interactions
	- Implementation:
		- SQL Server with indexing

### Bottlenecks & Solutions

- Slow post loading
	- Cause:
		- High read traffic
	- Solution:
		- Redis caching

- Delayed notifications
	- Cause:
		- Blocking message delivery
	- Solution:
		- SignalR real-time updates

- Feed scalability issues
	- Cause:
		- Complex feed queries
	- Solution:
		- Precomputed and cached feeds

---
## Example 4: Financial Transactions API Architecture

### Scenario

- .NET-based banking API
- Core requirements:
	- Secure and fast transactions
	- High scalability during peak loads
	- Asynchronous fraud detection

### Architecture & Design Decisions

- Load Balancer
	- Ensures reliability and availability
	- Implementation:
		- Azure Load Balancer / Kubernetes

- Caching Layer
	- Reduces account lookup load
	- Implementation:
		- Redis cache

- Async Processing
	- Processes fraud detection and alerts
	- Implementation:
		- Azure Service Bus

- Database
	- Secure transaction storage
	- Implementation:
		- SQL Server with encryption and indexing

### Bottlenecks & Solutions

- Slow transaction processing
	- Cause:
		- Heavy write operations
	- Solution:
		- Database sharding

- Fraud detection delays
	- Cause:
		- Synchronous processing
	- Solution:
		- Async fraud analysis

- High API latency
	- Cause:
		- Large payload sizes
	- Solution:
		- Payload optimization and compression