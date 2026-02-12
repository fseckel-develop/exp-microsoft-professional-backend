## Example 1: E-Commerce Platform

### Scenario

-	High traffic spikes during flash sales
-	Five backend application servers
-	Key requirements:
	-	Even traffic distribution
	-	Automatic exclusion of unhealthy servers
	-	Consistent user sessions during checkout

---
### Load Balancing Plan

-	Load balancing algorithm:
	-	Round Robin
		-	Evenly distributes requests across all servers
		-	Best suited for equal-capacity, stateless servers
-	Health checks:
	-	Continuously monitor server availability
	-	Automatically remove failed servers from rotation
	-	Prevent downtime during peak traffic
-	Sticky sessions:
	-	Required
		-	Maintains user session continuity during checkout
		-	Preserves shopping cart and user state without repeated retrieval

---
### Summary

-	Even traffic distribution:
	-	Round Robin ensures balanced server utilization
-	Reliability:
	-	Health checks redirect traffic to healthy servers
-	User experience:
	-	Sticky sessions preserve checkout state



---
---
## Example 2: Learning Management System (LMS)

### Scenario

-	Thousands of students accessing materials simultaneously
-	Four backend application servers
-	Key requirements:
	-	Route traffic to least busy server
	-	Prevent overload during exams
	-	Minimise disruption to live quizzes

---
### Load Balancing Plan

-	Load balancing algorithm:
	-	Least Connections
		-	Routes requests to the server with the fewest active connections
		-	Ideal for variable workloads (exams, quizzes)
-	Health checks:
	-	Continuously verify server health
	-	Remove failing servers during live sessions
	-	Reduce risk of quiz disruptions
-	Sticky sessions:
	-	Not required
		-	LMS interactions are mostly stateless
		-	Avoids restricting traffic distribution flexibility

---
### Summary

-	Request routing:
	-	Least Connections optimises performance under uneven load
-	System reliability:
	-	Health checks ensure continuous availability
-	Scalability:
	-	No sticky sessions allow flexible load distribution



---
---
## Example 3: Video Streaming Service

### Scenario

-	High traffic during peak viewing hours
-	Eight backend servers
-	Key requirements:
	-	High availability
	-	Minimal buffering
	-	Uninterrupted streaming experience

---
### Recommended Load Balancing Plan

-	Load balancing algorithm:
	-	Least Connections
		-	Accounts for long-lived streaming connections
		-	Reduces buffering by avoiding overloaded servers
-	Health checks:
	-	Detect and remove failed streaming servers
	-	Ensure traffic is routed only to healthy instances
-	Sticky sessions:
	-	Not required
		-	Streaming is stateless
		-	Improves flexibility and fault tolerance

---
### Summary

-	Performance optimization:
	-	Least Connections minimizes buffering
-	Reliability:
	-	Health checks prevent routing to failed servers
-	Scalability:
	-	No sticky sessions maintain dynamic balancing



---
---
## Example 4: Social Media Platform

### Scenario

-	Millions of requests per second
-	Ten backend servers
-	Key requirements:
	-	Even request distribution
	-	High availability during failures
	-	Smooth media upload and browsing experience

---
### Recommended Load Balancing Plan

-	Load balancing algorithm:
	-	Round Robin
		-	Simple and effective for equal-capacity servers
		-	Ensures uniform traffic distribution
-	Health checks:
	-	Continuously monitor server status
	-	Automatically exclude failed servers
	-	Prevent outages during high traffic
-	Sticky sessions:
	-	Not required
		-	Most interactions are stateless
		-	Improves load balancer efficiency

---
### Summary

-	Traffic distribution:
	-	Round Robin balances requests evenly
-	Fault tolerance:
	-	Health checks ensure high availability
-	User experience:
	-	Stateless routing supports scalability and responsiveness