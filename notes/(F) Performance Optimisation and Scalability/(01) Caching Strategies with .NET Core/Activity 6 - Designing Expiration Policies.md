## Overview

- Learners analyse different datasets and select appropriate cache expiration strategies
- Decisions are justified based on data characteristics, performance impact, and consistency requirements
- Focus on balancing security, accuracy, and efficiency in caching

---
## Example 1: Expiration Policies for an Online Retail System

### Scenario

- An online retail platform caches multiple data types to improve performance
- Incorrect expiration can lead to:
	- Stale pricing
	- Expired promotions still visible
	- Inconsistent shopping cart behaviour
- Appropriate expiration strategies are required based on data volatility and importance

### Step 1: Data Types and Their Characteristics

- User Sessions
	- Users remain logged in across visits
	- Sessions should persist while active
	- Must expire after inactivity for security
- Product Catalog
	- Product details change infrequently
	- Updates must reflect immediately when changes occur
- Promotional Discounts
	- Highly time-sensitive
	- Must disappear immediately after the promotion ends

### Step 2: Choice of Expiration Strategy

- User Sessions
	- Best Strategy: Sliding Expiration
	- Why It Works:
		- Keeps sessions active while users interact
		- Automatically expires inactive sessions
- Product Catalog
	- Best Strategy: Dependent Expiration
	- Why It Works:
		- Cache updates automatically when product data changes
		- Prevents stale product information
- Promotional Discounts
	- Best Strategy: Absolute Expiration
	- Why It Works:
		- Automatically removes discounts at a fixed end time
		- Prevents expired promotions from being applied

### Step 3: Why These Expiration Policies Work

- Sliding Expiration ensures active users remain logged in without frequent reauthentication
- Dependent Expiration guarantees users always see the latest product information
- Absolute Expiration ensures expired promotions are removed immediately
- Overall benefits:
	- Prevents stale data
	- Reduces unnecessary database queries
	- Maintains performance without sacrificing accuracy

---
## Example 2: Expiration Policies for a Banking System

### Scenario

- A banking system caches sensitive and high-demand data
- Incorrect expiration could cause:
	- Security risks
	- Outdated financial information
	- Performance degradation
- Each dataset requires a strategy aligned with security, accuracy, and access patterns

### Step 1: Data Types and Their Characteristics

- User Authentication Tokens
	- Security-critical
	- Must not persist beyond a fixed validity period
- Transaction History
	- Frequently accessed by users
	- Older records change rarely
- Live Exchange Rates
	- Highly volatile
	- Must always reflect the latest market values

### Step 2: Choice of Expiration Strategy

- User Authentication Tokens
	- Best Strategy: Absolute Expiration
	- Justification:
		- Tokens expire after a fixed time
		- Prevents reuse and unauthorised access
- Transaction History
	- Best Strategy: Sliding Expiration
	- Justification:
		- Frequently accessed records remain cached
		- Infrequently accessed data expires naturally
- Live Exchange Rates
	- Best Strategy: Dependent Expiration
	- Justification:
		- Cache updates immediately when new rates are available
		- Prevents users from seeing outdated conversion values

### Step 3: Why These Expiration Policies Work

- Absolute Expiration enhances security for authentication tokens
- Sliding Expiration optimizes cache usage for frequently accessed transaction data
- Dependent Expiration ensures real-time accuracy for exchange rates
- Combined benefits:
	- Improved security and reliability
	- Reduced unnecessary cache refreshes
	- Accurate, up-to-date financial information with optimal performance