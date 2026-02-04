## Example 1: Challenges in a Multi-Server E-Commerce Platform

### Scenario

- An e-commerce platform operates across multiple servers
	- Redis is used as a distributed cache
	- Cached data includes product details, user sessions, and shopping carts
- Identified problems
	- Stale data shown to users
	- Cache inconsistency between servers
	- Synchronisation delays across regions
- Goal
	- Ensure real-time synchronisation
	- Minimise performance overhead

---
### Step 1: Identify the Synchronisation Challenges

- Stale Data
	- Why it happens
		- Cache entries are not updated immediately after database changes
	- Example scenario
		- Users see outdated product prices
- Cache Inconsistency
	- Why it happens
		- Different servers update caches at different times
	- Example scenario
		- Shopping cart updates visible on one server but not another
- Synchronization Delays
	- Why it happens
		- Network latency and load balancing delays cache updates
	- Example scenario
		- Stock availability updates arrive later in some regions

---
### Step 2: Solutions to Ensure Data Consistency

- Cache Expiration & Invalidation
	- How it works
		- Sets TTLs and clears cache when data changes
	- Use case
		- Product prices expire every 10 minutes or on update
- Publish-Subscribe (Pub/Sub) Mechanism
	- How it works
		- Redis broadcasts data changes to all servers
	- Use case
		- Stock level changes synced instantly
- Read-Through Caching
	- How it works
		- Cache refreshes automatically on access
	- Use case
		- Product data updated on cache miss
- Write-Through Caching
	- How it works
		- Writes update both cache and database simultaneously
	- Use case
		- Shopping cart updates stay consistent
- Geographically Distributed Caching
	- How it works
		- Uses regional cache nodes
	- Use case
		- Faster synchronisation across regions



---
---
## Example 2: Data Synchronisation Challenges in a News Platform

### Scenario

- A global news website serves millions of users
	- Distributed across multiple servers
	- Uses caching for headlines, articles, trending topics, and comments
- Identified problems
	- Outdated articles shown to users
	- Inconsistent trending topics across regions
	- Delayed appearance of user comments
- Goal
	- Maintain accurate, synchronised data
	- Preserve real-time user experience

---
### Step 1: Identify the Synchronisation Challenges

- Stale Data
	- Why it happens
		- Cached headlines are not refreshed after editorial updates
	- Example scenario
		- Breaking news articles display outdated information
- Cache Inconsistency
	- Why it happens
		- Servers synchronize trending topics at different times
	- Example scenario
		- Users in different regions see different trending stories
- Synchronization Delays
	- Why it happens
		- Slow propagation of comment updates across cache nodes
	- Example scenario
		- Newly posted comments appear minutes later for other users

---
### Step 2: Solutions to Ensure Data Consistency

- Cache Expiration & Invalidation
	- How it works
		- Automatically clears outdated cache entries
	- Use case
		- Headlines refresh every 5 minutes or on editor updates
- Publish-Subscribe (Pub/Sub) Mechanism
	- How it works
		- Redis pushes content updates to all servers in real time
	- Use case
		- Breaking news updates synced instantly
- Read-Through Caching
	- How it works
		- Fetches fresh data when cached content is missing or outdated
	- Use case
		- Updated articles loaded automatically on access
- Write-Through Caching
	- How it works
		- Writes comments to cache and database simultaneously
	- Use case
		- Ensures comment consistency across servers
- Geographically Distributed Caching
	- How it works
		- Regional cache nodes reduce latency and sync delays
	- Use case
		- Trending topics update faster worldwide

---
### Step 3: Impact of Poor Synchronization

- Causes inconsistent user experiences
	- Different users see different versions of content
- Leads to misinformation
	- Outdated headlines reduce trust in the platform
- Breaks real-time interaction
	- Delayed comments harm engagement
- Affects platform reliability
	- Users lose confidence in content accuracy
- Highlights the need for synchronization
	- Real-time cache updates ensure trust, performance, and scalability