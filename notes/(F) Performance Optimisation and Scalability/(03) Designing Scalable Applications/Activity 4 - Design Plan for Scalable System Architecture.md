## Stage 1: Preparation and Core Concepts

### Stateless Services

- Definition and behaviour:
	- Each request is processed independently
	- No session state stored on application servers
- Scalability impact:
	- Enables horizontal scaling
	- Simplifies fault tolerance
- State management (if required):
	- Use distributed caching (e.g., Redis)

### Load Balancing

- Purpose:
	- Distributes incoming traffic evenly
	- Prevents server overload
- Common strategies:
	- Round-robin for equal distribution
	- Least connections for dynamic load handling
	- IP-hash when session affinity is required

### Caching Mechanisms

- Caching types:
	- In-memory caching for fast, local access
	- Distributed caching for shared state across servers
- Typical use cases:
	- Frequently accessed records
	- Session-related or read-heavy data
- Technologies:
	- Redis
	- Memcached
	- .NET built-in caching

### Asynchronous Processing

- Why asynchronous execution:
	- Prevents blocking user requests
	- Improves responsiveness under load
- Common async tasks:
	- Email notifications
	- Report generation
	- Background data processing
- Supporting technologies:
	- Message queues (Azure Queue Storage, RabbitMQ)
	- Event-driven architectures

---
## Stage 2: Scalable Architecture Design

### User Request Flow

- Request path:
	- User → Load Balancer → Multiple Stateless Web Servers
- Key characteristics:
	- No dependency on server-local state
	- Supports dynamic scaling

### Caching Layer Placement

- Integration point:
	- Between application servers and database
- Cached data:
	- Frequently accessed records
	- Session or computed data (if needed)
- Benefits:
	- Reduced database load
	- Faster response times

### Asynchronous Processing Flow

- Background execution model:
	- Application servers enqueue tasks
	- Message queue buffers workload
	- Worker services process tasks independently
- Typical examples:
	- Email sending
	- Report generation
	- Long-running computations

### Database Layer Integration

- Application interaction:
	- Stateless servers communicate with database
- Scalability techniques:
	- Read replicas for read-heavy workloads
	- Primary database for write operations
	- Optional sharding or partitioning

---
## Stage 3: Documented Design Decisions

### Load Balancing

- Design choice:
	- Load balancer used as entry point
- Strategy:
	- Round-robin selected for simplicity and fairness
- Result:
	- Improved availability and reliability

### Stateless Services

- Rationale:
	- Simplifies horizontal scaling
	- Reduces dependency between requests
- State handling:
	- Externalized to distributed caching when necessary

### Caching Layer

- Data selection:
	- Read-heavy and frequently requested data
- Caching approach:
	- Redis for shared distributed caching
	- Optional in-memory caching per instance
- Outcome:
	- Lower latency
	- Reduced database contention

### Asynchronous Processing

- Justification:
	- Decouples long-running tasks from request lifecycle
- Execution model:
	- Message queue + worker services
- Key benefits:
	- Smoother traffic handling
	- Improved user experience

### Database Scaling

- Techniques applied:
	- Read replicas for scalability
	- Single primary for consistency
- Performance impact:
	- Reduced read pressure
	- Improved throughput

---
## Stage 4: Bottlenecks and Optimisation Strategies

### Identified Bottlenecks

- Database load:
	- High volume of read operations
- Cache saturation:
	- Frequent evictions under heavy usage
- Message queue delays:
	- Backlog during peak traffic
- Load balancer limits:
	- Single entry point becoming a bottleneck

### Optimisation Solutions

- Database optimisations:
	- Introduce read replicas
	- Apply indexing and query optimisation
- Caching improvements:
	- Increase cache capacity
	- Use tiered caching strategies
- Asynchronous scaling:
	- Scale worker services horizontally
	- Adopt event-driven processing
- Traffic management:
	- Deploy multiple load balancers
	- Use cloud-native load balancing solutions

### Future Growth Considerations

- Evolution strategies:
	- Auto-scaling groups
	- Serverless components for burst workloads
	- Microservices architecture for independent scaling