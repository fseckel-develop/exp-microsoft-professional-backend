## Objective

- Design a Redis-based caching strategy for a high-traffic platform
	- Focus on performance, scalability, and synchronisation
	- Decide what data should be cached and why
	- Define expiration and synchronisation strategies
- Emphasis on architectural planning rather than implementation


---
---
## Example 1: Redis Caching Strategy for Video Streaming Platform

### Scenario

- A video streaming platform serves millions of users
	- Users search for content, watch videos, and save watch history
- Identified problems
	- Every request queries the database
	- High database load causes slow response times
- Goal
	- Integrate Redis caching
	- Reduce database queries
	- Improve performance and scalability

---
### Step 1: Identify What to Cache and Why

- User Sessions
	- Enables seamless logins across multiple devices
- Recently Watched Videos
	- Allows users to resume playback quickly
- Popular Video Metadata
	- Titles and descriptions requested frequently
- Search Results
	- Trending searches queried repeatedly

---
### Step 2: Define the Redis Caching Strategy

- User Sessions
	- Performance benefits
		- Reduces repeated authentication requests
	- Expiration & synchronisation
		- Stored for 24 hours
		- Refreshed on activity
- Recently Watched Videos
	- Performance benefits
		- Fast access to playback history
	- Expiration & synchronisation
		- Stored for 7 days
		- Updated when new videos are watched
- Popular Video Metadata
	- Performance benefits
		- Prevents excessive database queries
	- Expiration & synchronisation
		- Cached for 1 hour
		- Refreshed periodically
- Search Results
	- Performance benefits
		- Faster responses for trending searches
	- Expiration & synchronization
		- Cached for 10 minutes
		- Updated when new content is available

---
### Step 3: Why This Redis Caching Strategy Works

- User sessions
	- Prevent frequent logins
	- Support seamless device switching
- Recently watched videos
	- Enable quick resume without database access
- Video metadata
	- Cached due to infrequent changes
- Search results
	- Short cache duration balances speed and freshness

---
### How Redis Improves Performance

- Reduces database load by serving data from memory
- Improves response times significantly
- Enables scalability by distributing cache across servers


---
---
## Example 2: Redis Caching Strategy for E-Commerce Platform

### Scenario

- An e-commerce platform serves millions of daily users
	- Browsing products
	- Searching for deals
	- Completing purchases
- Identified problems
	- Slow product page loads due to frequent database queries
	- High server load during peak traffic
	- Long checkout times impacting user experience
- Goal
	- Design a Redis caching strategy
	- Improve performance and scalability

---
### Step 1: Identify What to Cache and Why

- Product Listings
	- Frequently accessed product details
- User Sessions
	- Authentication data used across devices
- Shopping Carts
	- Active cart data accessed repeatedly
- Frequently Searched Items
	- Common search queries requested by many users
- Promotional Discounts
	- Time-sensitive sales data displayed site-wide

---
### Step 2: Define the Redis Caching Strategy

- Product Listings
	- Performance benefits
		- Faster product page loads
		- Reduced database queries
	- Expiration & synchronization
		- Refresh every 30 minutes
		- Update when product details change
- User Sessions
	- Performance benefits
		- Faster authentication
		- Seamless multi-device access
	- Expiration & synchronization
		- Stored for 24 hours
		- Refreshed on user activity
- Shopping Carts
	- Performance benefits
		- Prevents cart loss
		- Faster checkout process
	- Expiration & synchronization
		- Stored for 1 hour
		- Updated when items are added or removed
- Frequently Searched Items
	- Performance benefits
		- Faster responses for popular searches
	- Expiration & synchronization
		- Cached for 10 minutes
		- Refreshed when new products are added
- Promotional Discounts
	- Performance benefits
		- Immediate access to sales information
	- Expiration & synchronization
		- Cached for 1 day
		- Updated when promotions change

---
### Step 3: Why This Redis Caching Strategy Works

- Product listings
	- Cached to reduce database pressure during heavy traffic
- User sessions
	- Stored centrally to support scalability
- Shopping carts
	- Maintained in memory for a smooth checkout experience
- Search data and promotions
	- Cached briefly to balance freshness and speed

---
### How Redis Improves Performance

- Reduces database load by serving frequently accessed data from memory
- Improves response times for browsing, search, and checkout
- Distributes cache across servers for high scalability
- Ensures consistent performance during peak traffic