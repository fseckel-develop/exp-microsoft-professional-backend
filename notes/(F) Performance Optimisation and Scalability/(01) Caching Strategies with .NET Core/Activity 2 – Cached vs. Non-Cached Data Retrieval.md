## Overview

- Learners analyse real-world scenarios where frequent data requests reduce performance
- Focus on:
	- Identifying performance bottlenecks
	- Understanding how in-memory caching improves speed
	- Evaluating benefits and limitations
- Emphasis on planning and analysis, not implementation

---
## Example 1: E-Commerce Website

### Identified Problems

- Slow page load times for popular products
- Frequent database queries for:
	- Product catalog data
	- User shopping carts
	- Personalized recommendations
- Increased latency during high traffic

### How In-Memory Caching Helps

- Fast access
	- Product data stored in RAM for instant retrieval
- Reduced database load
	- Pages served from cache instead of querying the database
- Improved user experience
	- Shopping carts remain active across page navigation

### Cacheable Data

- Product catalog
	- Images
	- Descriptions
	- Prices
- User shopping cart data
- Homepage and product recommendations

---
## Example 2: Ride-Sharing App

### Identified Problems

- Delays caused by repeated database queries for driver locations
- Frequent recalculation of:
	- Nearby drivers
	- Ride estimates
- Performance degradation during peak traffic

### How In-Memory Caching Helps

- Faster updates
	- Recent driver locations stored in memory
- Improved ride estimates
	- Cached distance and fare calculations
- Better scalability
	- Handles high demand more efficiently

### Cacheable Data

- Recent driver locations
- Estimated fares and wait times
- User ride history

---
## Example 3: Social Media Platform

### Identified Problems

- Slow feed loading because timelines are fetched from the database on every app open
- High database load due to:
	- Repeated requests for user posts
	- Frequent profile data lookups
- Delays during peak usage periods

### How In-Memory Caching Helps

- Faster feed delivery
	- Timeline data served directly from memory
- Reduced database load
	- Fewer redundant queries for user and profile data
- Improved responsiveness
	- Trending data accessed instantly

### Cacheable Data

- User timeline posts
	- Recent posts from followed accounts
- Profile pictures and basic user information
- Trending topics and hashtag data

### Limitations of In-Memory Caching

- Cached data may not reflect real-time updates immediately
- Cache is cleared when the server restarts
- Large-scale platforms may require distributed caching instead

---
## Example 4: News Website

### Identified Problems

- Slow page loads during major news events
- Traffic spikes cause:
	- Increased database queries
	- Delayed content delivery
- Repeated requests for the same headlines and images

### How In-Memory Caching Helps

- Faster content delivery
	- Headlines and summaries served from memory
- Reduced server and database load
	- Popular content reused without regeneration
- Improved reliability
	- Site remains responsive during traffic surges

### Cacheable Data

- Latest headlines and article summaries
- Images reused across multiple articles
- Frequently accessed news categories
	- Breaking News
	- Sports

### Limitations of In-Memory Caching

- Cached content may lag behind real-time updates
- Cache loss on system restart
- Unsuitable for long-term or historical content storage