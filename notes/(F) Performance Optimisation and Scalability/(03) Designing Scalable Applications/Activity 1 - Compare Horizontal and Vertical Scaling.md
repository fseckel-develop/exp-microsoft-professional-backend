## Example 1: Scaling an E-Commerce Website

### Scenario

- Online retail store preparing for a major sale event  
- Normal traffic: ~10,000 daily visitors  
- Peak traffic during promotions: 100,000+ daily visitors  
- Hosted on a single powerful server handling:
	- Product searches  
	- Checkout processes  
	- User accounts  
- Issues during peak load:
	- Slow load times  
	- Occasional crashes  

### Step 1: Evaluating the Scaling Options

- **Horizontal Scaling**
	- Adds multiple servers to distribute traffic  
	- Pros:
		- Handles high traffic efficiently  
		- Improves redundancy  
		- Eliminates single point of failure  
	- Cons:
		- Requires load balancing  
		- Needs synchronization between servers  

- **Vertical Scaling**
	- Upgrades CPU, RAM, and storage of one server  
	- Pros:
		- Simple to implement  
		- No architectural changes required  
	- Cons:
		- Limited by hardware capacity  
		- Expensive upgrades  
		- Risk of downtime  

### Step 2: Selecting the Best Approach

- Horizontal scaling chosen as the better solution  
- Reasons:
	- Traffic spikes occur only during sale events  
	- Supports many concurrent users  
	- Prevents server overload  
	- Load balancer distributes requests evenly  
	- Improves performance and reliability  

---
## Example 2: Scaling a Video Streaming Platform

### Scenario

- Video streaming service experiencing rapid growth  
- User base increased from thousands to over one million viewers  
- Common issues reported:
	- Buffering  
	- Slow video loading  
	- Playback interruptions during peak hours  
- Current setup:
	- Single high-performance server  
	- Handles video storage, user requests, and streaming  

### Step 1: Comparing the Scaling Options

- **Horizontal Scaling**
	- Pros:
		- Handles many concurrent users  
		- Improves reliability  
		- Distributes load across multiple servers  
	- Cons:
		- Requires load balancing  
		- Increases system complexity  

- **Vertical Scaling**
	- Pros:
		- Simple to implement  
		- No architectural changes needed  
	- Cons:
		- Limited by hardware capacity  
		- Cannot scale indefinitely  

### Step 2: Best Scaling Approach

- Horizontal scaling is the better choice for the streaming platform  
- Justification:
	- Distributes user requests to avoid server overload  
	- Reduces buffering and playback interruptions  
	- Supports continuous growth without hardware limits  
	- Enables long-term scalability  
	- Can be enhanced further by integrating a CDN for optimized video delivery  