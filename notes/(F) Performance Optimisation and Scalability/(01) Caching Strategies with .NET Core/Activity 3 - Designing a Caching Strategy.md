## Example 1: Caching Strategy for a News Website

- Scenario
	- High-traffic news website with millions of daily visitors
	- Frequent access to headlines, articles, comments, and trending topics
	- Performance issues caused by repeated database queries
	- Goal: improve performance while keeping content reasonably fresh

---
### What to Cache and Why

- Latest news headlines
	- Requested extremely frequently
	- Change often but not every second
- Full articles
	- Rarely change after publication
	- Ideal candidates for caching
- User comments
	- Must load quickly
	- Still need to reflect new interactions
- Trending topics
	- Displayed on most pages
	- Expensive to recalculate repeatedly

---
### Caching Strategy

- Latest headlines
	- Performance benefit
		- Reduces repeated database queries
		- Speeds up homepage loads
	- Expiration strategy
		- Refresh every 5 minutes
- Full articles
	- Performance benefit
		- Faster content delivery
		- Fewer database hits
	- Expiration strategy
		- Refresh every 1 hour or on article update
- User comments
	- Performance benefit
		- Fast comment loading
		- Reduced read-time latency
	- Expiration strategy
		- Store for 15 minutes
		- Refresh when new comments are posted
- Trending topics
	- Performance benefit
		- Avoids repeated keyword searches
		- Faster page rendering
	- Expiration strategy
		- Refresh every 30 minutes

---
### Why This Caching Strategy Works

- Balances freshness and performance
- Prevents server overload from constant database access
- Ensures frequently accessed content loads instantly
- Allows timely updates without unnecessary recomputation

---
### Performance Impact

- Reduced server load
	- Thousands of database queries avoided per second
- Faster page loads
	- Users receive content almost instantly
- Improved scalability
	- Handles traffic spikes more reliably


---
---
## Example 2: Caching Strategy for an E-Commerce Store

### Scenario

- Online store selling electronics, clothing, and home goods
- Frequent browsing, searching, and cart interactions
- Performance issues due to heavy database usage
- Goal: optimise speed and reduce server load using caching

---
### What to Cache an Why

- Product catalog
	- Performance benefit
		- Avoids repeated database queries
		- Improves page load speed
	- Expiration strategy
		- Refresh every 30 minutes or when product details change
- User shopping cart
	- Performance benefit
		- Fast access to cart items
		- Prevents unnecessary database hits
	- Expiration strategy
		- Store for 1 hour
		- Refresh when items are added or removed
- Search results
	- Performance benefit
		- Faster responses for common searches
		- Reduced database load
	- Expiration strategy
		- Store for 10 minutes
		- Refresh when new products are added
- Category listings
	- Performance benefit
		- Faster navigation between product categories
	- Expiration strategy
		- Refresh every 1 hour
- Homepage recommendations
	- Performance benefit
		- Faster delivery of personalised content
	- Expiration strategy
		- Refresh daily or on user login

---
### How Caching Improves Performance

- In-memory caching reduces response times by serving frequently accessed data instantly
- Product pages load faster because data is retrieved from memory instead of the database
- Shopping cart caching ensures a smooth and uninterrupted checkout experience
- Cached search results return popular queries quickly, improving navigation
- Overall server load is reduced, lowering latency and improving scalability